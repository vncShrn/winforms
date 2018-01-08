using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using vxc152130_RebateForm;

namespace Asg2_vxc152130
{
    /**
     * FormRebate class for Rebate form add, modify, delete operations
     * @author: Vincy Shrine
     */
    public partial class FormRebate : Form
    {
        // dictionary map to load and store records in memory
        private Dictionary<string, Record> recordMap = new Dictionary<string, Record>();

        // start time of ADD new record
        private string startTime;

        // end time of ADD new record
        private string endTime;

        // determines modes: ADD new record or MODIFY existing record
        private Record selectedRecord = null;

        // constant file name
        static private string fileName = @"CS6326Asg2.txt";

        public FormRebate()
        {
            InitializeComponent();
            // Make column width constant
            // this.listView_Records.ColumnWidthChanging += new ColumnWidthChangingEventHandler(listView_Records_ColumnWidthChanging, listView_Records);
            listView_Records.DrawColumnHeader += Helper.listView_DrawColumnHeader;
            listView_Records.Sorting = SortOrder.Ascending;
            this.errorProvider.Icon = Helper.ResizeIcon(SystemIcons.Warning, SystemInformation.SmallIconSize);
            this.StatusStrip.ForeColor = Color.Black;
            this.StatusStrip.BackColor = Color.WhiteSmoke;
        }

        private void FormRebate_Load(object sender, EventArgs e)
        {
            // Set form size to screen size and center
            Screen screen = Screen.FromControl(this);
            int iHeight = screen.WorkingArea.Height - this.Height;
            this.Height += iHeight;

            //listView_Records.Height += iHeight;
            //groupBox1.Height += iHeight-2;
            //groupBox2.Height += iHeight-2;
            this.CenterToScreen();

            // load file contents to memory map and listView
            reloadRecordsFromFile();
            updateStatusStrip("Ready", Color.Black);
        }

        private void FName_TextChanged(object sender, EventArgs e)
        {
            startTime = DateTime.Now.ToString("HH:mm:ss");

            if (FName.TextLength < 0)
                errorProvider.SetError(FName, "First Name cannot be empty");
            else
                errorProvider.Clear();
            btnSave_Enabled();

        }

        private void LName_TextChanged(object sender, EventArgs e)
        {
            if (LName.TextLength < 0)
                errorProvider.SetError(LName, "Last Name cannot be empty");
            else
                errorProvider.Clear();
            btnSave_Enabled();
        }

        private void Addr1_TextChanged(object sender, EventArgs e)
        {
            if (Addr1.TextLength < 0)
                errorProvider.SetError(Addr1, "Address cannot be empty");
            else
                errorProvider.Clear();
            btnSave_Enabled();
        }

        private void City_TextChanged(object sender, EventArgs e)
        {
            if (City.TextLength < 0)
                errorProvider.SetError(City, "City cannot be empty");
            else
                errorProvider.Clear();
            btnSave_Enabled();
        }

        private void State_TextChanged(object sender, EventArgs e)
        {
            btnSave_Enabled();
            if (State.TextLength != 2)
                errorProvider.SetError(State, "State should have two letters");
            else
                errorProvider.Clear();
        }

        private void Zip_TextChanged(object sender, EventArgs e)
        {
            btnSave_Enabled();
            // Call SetError or Clear on the ErrorProvider
            if (Zip.TextLength < 0 || !Helper.IsValidZipCode(Zip.Text))
                errorProvider.SetError(Zip, "ZipCode accepts following format:\n" +
                    "12345 1234\n"+
                    "12345-1234\n" );
            else
                errorProvider.Clear();
        }

        private void Phone_TextChanged(object sender, EventArgs e)
        {
            btnSave_Enabled();
            // Call SetError or Clear on the ErrorProvider
            if (Phone.TextLength < 0 || !Helper.IsValidPhone(Phone.Text))
                errorProvider.SetError(Phone, "Phone number accepts formats: \n" +
                    "214 444 1134\n"+
                    "(214) 444 1134\n"+
                    "214-444-1134\n"+
                    "214.444.1134\n");
            else
                errorProvider.Clear();
        }

        private void Email_TextChanged(object sender, EventArgs e)
        {
            btnSave_Enabled();
            if (Email.TextLength < 0 || !Helper.IsValidEmail(Email.Text))
                errorProvider.SetError(Email, "Email-address example format:\n" +
                    "abc@gmail.com");
            else
                errorProvider.Clear();

        }
        private void btnSave_Enabled()
        {
            btnSave.Enabled = FName.TextLength > 0 && LName.TextLength > 0
                && Addr1.TextLength > 0 && City.TextLength > 0 && State.TextLength == 2
                && Zip.TextLength > 0 && Helper.IsValidZipCode(Zip.Text)
                && Phone.TextLength > 0 && Helper.IsValidPhone(Phone.Text)
                && Email.TextLength > 0 && Helper.IsValidEmail(Email.Text);
        }

        private void listView_Records_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Delete)
            {
                deleteRecord();
                e.Handled = true;
            }
            if (e.KeyCode == Keys.Enter)
            {
                if (this.listView_Records.SelectedItems.Count == 0)
                    return;
                ListViewItem item = listView_Records.SelectedItems[0];
                initSelectedRecord(item);
            }

        }
        private void listView_Records_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.listView_Records.SelectedItems.Count == 0)
                return;
            ListViewItem item = listView_Records.SelectedItems[0];
            initSelectedRecord(item);
        }

        private void initSelectedRecord(ListViewItem item)
        {
            string fullName = item.Text;
            Record record = recordMap[fullName];
            clearTextBoxes(panel1);
            FName.Text = record.FName;
            LName.Text = record.LName;
            MInit.Text = record.Minit;
            Addr1.Text = record.Addr1;
            Addr2.Text = record.Addr2;
            City.Text = record.City;
            State.Text = record.State;
            Zip.Text = record.Zip;
            Phone.Text = record.Phone;
            Email.Text = record.Email;
            if (record.Proof == true)
            {
                yesRadio.Checked = true;
                NoRadio.Checked = false;
            }
            else
            {
                yesRadio.Checked = false;
                NoRadio.Checked = true;
            }
            ReceivedDate.Value = record.ReceivedDate;
            btnDelete.Enabled = true;
            selectedRecord = record;
        }

        private void reloadRecordsFromFile()
        {
            // First cleanup form and memory
            listView_Records.Items.Clear();
            recordMap = new Dictionary<string, Record>();
            clearTextBoxes(panel1);
            selectedRecord = null;

            listView_Records.View = View.Details;
            if (!File.Exists(fileName))
            {
                updateStatusStrip("Ready", Color.Black);
                return;
            }
            using (StreamReader sr = new StreamReader(fileName))
            {
                string recordString = sr.ReadLine();
                while (recordString != null)
                {
                    string[] split = recordString.Split('\t');
                    if (split.Length != 14)
                        continue;
                    Boolean proof = true;
                    bool.TryParse(split[10], out proof);
                    DateTime receivedDate = DateTime.Parse(split[11]);

                    Record record = new Record(recordString, split[0], split[1], split[2],
                        split[3], split[4], split[5], split[6], split[7],
                        split[8], split[9], proof, receivedDate, split[12], split[13]);

                    // add record to memory
                    recordMap.Add(record.FullName, record);
                    // add record to listView
                    var item = new ListViewItem();
                    item.Text = record.FullName;
                    item.SubItems.Add(record.Phone + "");
                    listView_Records.Items.Add(item);

                    //check for further lines in file
                    recordString = sr.ReadLine();
                }
            }
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            clearTextBoxes(panel1);
            btnDelete.Enabled = false;
            selectedRecord = null;
            FName.Focus();
            updateStatusStrip("Ready", Color.Black);
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            endTime = DateTime.Now.ToString("HH:mm:ss");
            string fullName = FName.Text + " " + LName.Text + " " + MInit.Text;
            try
            {
                saveRecord(fullName, endTime);
                if (selectedRecord == null)
                {
                    // Adding new record completed, now clear form to add next new record
                    clearTextBoxes(panel1);
                    selectedRecord = null;
                    FName.Focus();
                }
            }
            catch (Exception)
            {
                updateStatusStrip("Save failed for " + fullName, Color.DarkRed);
                reloadRecordsFromFile();
            }

        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            deleteRecord();
        }

        private void saveRecord(string fullName, string endTime)
        {
            if (selectedRecord == null)
            {
                // If this is new record ADD
                if (recordMap.ContainsKey(fullName))
                {
                    // If there is already record with same full name
                    updateStatusStrip("Save failed. Record with this name already exists", Color.DarkRed);
                    return;
                }
            }
            else
            {   // If this is old record MODIFY
                if (recordMap.ContainsKey(fullName))
                {
                    Record anotherRecord = recordMap[fullName];
                    if (anotherRecord != null && anotherRecord != selectedRecord)
                    {
                        // There is another record with the fullName that you changed to now.
                        updateStatusStrip("Save failed. Name change conflicts with existing claim", Color.DarkRed);
                        return;
                    }
                }
            }

            bool isProofRadio = yesRadio.Checked;
            Record record;
            if (selectedRecord == null)
            {
                // ADD new record with start and end time
                record = new Record(FName.Text, LName.Text, MInit.Text,
                        Addr1.Text, Addr2.Text, City.Text, State.Text, Zip.Text,
                        Phone.Text, Email.Text, isProofRadio, ReceivedDate.Value.Date,
                        startTime, endTime);
                recordMap[record.FullName] = record;
                var item = new ListViewItem();
                item.Text = record.FullName;
                item.SubItems.Add(record.Phone + "");
                listView_Records.Items.Add(item);
            }
            else
            {
                // MODIFY old record without changing old start and end time
                record = new Record(FName.Text, LName.Text, MInit.Text,
                    Addr1.Text, Addr2.Text, City.Text, State.Text, Zip.Text,
                    Phone.Text, Email.Text, isProofRadio, ReceivedDate.Value.Date,
                    selectedRecord.StartTime, selectedRecord.EndTime);
                recordMap.Remove(record.FullName);
                recordMap[record.FullName] = record;
                ListViewItem item = listView_Records.SelectedItems[0];
                item.SubItems.Clear();
                item.Text = record.FullName;
                item.SubItems.Add(record.Phone + "");
            }
            updateStatusStrip("Submitted claim for " + fullName, Color.DarkGreen);

        }

        private void deleteRecord()
        {
            try
            {
                string fullName = selectedRecord.FullName;
                // Delete from memory map
                recordMap.Remove(fullName);

                // Delete from listView only if deleted successsfully from file
                if (listView_Records.Items.Count == 1)
                    listView_Records.Items.Clear(); // Remove doesn''t seem to be working for a list of size 1
                else
                    listView_Records.SelectedItems[0].Remove();

                clearTextBoxes(panel1);
                selectedRecord = null;
                btnDelete.Enabled = false;
                updateStatusStrip("Deleted details of " + fullName, Color.DarkGreen);
            }
            catch (Exception)
            {
                updateStatusStrip("Delete failed for " + selectedRecord.FullName, Color.DarkRed);
                reloadRecordsFromFile();
            }
        }
        protected void clearTextBoxes(Control p1)
        {
            foreach (Control ctrl in p1.Controls)
            {
                if (ctrl is TextBox)
                {
                    TextBox t = ctrl as TextBox;
                    if (t != null)
                        t.Text = String.Empty;
                }
                else
                {
                    if (ctrl.Controls.Count > 0)
                        clearTextBoxes(ctrl);
                }
            }
            errorProvider.Clear();
        }
        private void updateStatusStrip(string msg, Color color)
        {
            StatusLabel.Text = string.Format(msg);
            StatusStrip.ForeColor = color;
            StatusStrip.Refresh();
        }

        private void listView_Records_MouseClick(object sender, MouseEventArgs e)
        {
            if (this.listView_Records.SelectedItems.Count == 0)
                return;
            ListViewItem item = listView_Records.SelectedItems[0];
            initSelectedRecord(item);
        }

        private void listView_Records_ItemSelectionChanged(object sender, ListViewItemSelectionChangedEventArgs e)
        {
            if (this.listView_Records.SelectedItems.Count == 0)
                return;
            ListViewItem item = listView_Records.SelectedItems[0];
            initSelectedRecord(item);
        }

        // As Email is the last textbox, it accepts ENTER key to SAVE the form
        private void Email_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter && btnSave.Enabled == true)
            {
                endTime = DateTime.Now.ToString("HH:mm:ss");
                string fullName = FName.Text + " " + LName.Text + " " + MInit.Text;
                try
                {
                    saveRecord(fullName, endTime);
                    if (selectedRecord == null)
                    {
                        // Adding new record completed, now clear form to add next new record
                        clearTextBoxes(panel1);
                        selectedRecord = null;
                        FName.Focus();
                    }
                }
                catch (Exception)
                {
                    updateStatusStrip("Save failed for " + fullName, Color.DarkRed);
                    reloadRecordsFromFile();
                }
            }
        }

        // As Received Date is the last input box, it accepts ENTER key to SAVE the form
        private void ReceivedDate_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter && btnSave.Enabled == true)
            {
                endTime = DateTime.Now.ToString("HH:mm:ss");
                string fullName = FName.Text + " " + LName.Text + " " + MInit.Text;
                try
                {
                    saveRecord(fullName, endTime);
                    if (selectedRecord == null)
                    {
                        // Adding new record completed, now clear form to add next new record
                        clearTextBoxes(panel1);
                        selectedRecord = null;
                        FName.Focus();
                    }
                }
                catch (Exception)
                {
                    updateStatusStrip("Save failed for " + fullName, Color.DarkRed);
                    reloadRecordsFromFile();
                }
            }
        }

        private void FormRebate_FormClosing(object sender, FormClosingEventArgs e)
        {
            // Write changes done into file from memory map
            Helper.writeIntoFile(recordMap, fileName);
        }
    }
}
