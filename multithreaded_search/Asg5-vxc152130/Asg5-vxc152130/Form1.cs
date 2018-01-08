using System;
using System.ComponentModel;
using System.Drawing;
using System.IO;
using System.Threading;
using System.Windows.Forms;

/********************************************************************************
 * A multi-threaded program that searches a large text file for a string.
 * It finds all occurrences of the string and shows them in a listView.  
 * There is a delay of 1ms on reading every line
 * 
 * Finally the matched count, total lines read from file, 
 * and the list of matched strings are displayed
 * 
 * @author: Vincy Shrine
 * @net-id: vxc152130
 * @Date: Nov/12/2017
 * 
 ********************************************************************************/
namespace Asg5_vxc152130
{
    public partial class TextSearchForm : Form
    {
        // Input text file
        private string fileName;

        // total matches - updated after every match found
        private int matchCount = 0;

        // total line count - updated in screen whenever match found and at end of execution you have correct value
        private int lineCount = 0;

        /**  - Not used 
        // producer-consumer Q, where bgWorker puts into Q, and UI threads reads from Q
        private ConcurrentQueue<ListItem> linesQ = new ConcurrentQueue<ListItem>();
        */

        public TextSearchForm()
        {
            InitializeComponent();
            listView.DrawColumnHeader += listView_DrawColumnHeader;
        }

        private void TextSearchForm_Load(object sender, EventArgs e)
        {
            // Set form size to screen size and center
            Screen screen = Screen.FromControl(this);
            int iHeight = screen.WorkingArea.Height - this.Height;
            this.Height += iHeight;

            // listView should end few inches above end of the screen height
            // groupBox1.Height += iHeight;
            // listView.Height += iHeight;
            this.CenterToScreen();

            listView.View = View.Details;
            listView.AutoResizeColumns(ColumnHeaderAutoResizeStyle.ColumnContent);
            listView.AutoResizeColumns(ColumnHeaderAutoResizeStyle.HeaderSize);

            statusLabel.Text = "Begin Search";
        }

        private void browseBtn_Click(object sender, EventArgs e)
        {
            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                // update fileName textBox when File Dialog is useds
                fileName = openFileDialog.FileName;
                browseInput.Text = fileName;
            }
        }

        private void browseInput_TextChanged(object sender, EventArgs e)
        {
            fileName = browseInput.Text;

            // Enable search button only if both inputFileName and inputSearchText are notNull
            if (!string.IsNullOrWhiteSpace(searchInput.Text) && !string.IsNullOrWhiteSpace(browseInput.Text))
                searchBtn.Enabled = true;
            else
                searchBtn.Enabled = false;
        }

        private void searchInput_TextChanged(object sender, EventArgs e)
        {
            // Enable search button only if both inputFileName and inputSearchText are notNull
            if (!string.IsNullOrWhiteSpace(searchInput.Text) && !string.IsNullOrWhiteSpace(browseInput.Text))
                searchBtn.Enabled = true;
            else
                searchBtn.Enabled = false;
        }

        private void searchBtn_Click(object sender, EventArgs e)
        {
            if (searchBtn.Text == "Search")
            {
                if (bgWorkerFileReader.IsBusy != true)
                {
                    // Start the asynchronous operation
                    searchBtn.Text = "Cancel";
                    statusLabel.Text = "Search in progress...";
                    linesCntLabel.Text = "0";
                    matchCntLabel.Text = "0";
                    listView.Items.Clear();
                    searchBtn.Focus();
                    bgWorkerFileReader.RunWorkerAsync();
                }
            }
            else if (searchBtn.Text == "Cancel")
            {
                if (bgWorkerFileReader.WorkerSupportsCancellation == true)
                {
                    // Cancel the asynchronous operation
                    searchBtn.Enabled = false;
                    statusLabel.Text = "Cancelling Search...";
                    bgWorkerFileReader.CancelAsync();
                }
            }
        }

        private void backgroundWorker_DoWork(object sender, DoWorkEventArgs e)
        {
            BackgroundWorker worker = sender as BackgroundWorker;
            StreamReader streamReader = null;
            String line;
            String searchText = searchInput.Text;
            matchCount = 0;

            try {
                streamReader = new StreamReader(fileName.Trim());
            } catch (FileNotFoundException){
                MessageBox.Show("Please select a valid text file");
                return;
            }

            while ((line = streamReader.ReadLine()) != null)
            {
                // ReportProgress should not be too frequently called. So advisable to sleep for minimum of 100ms
                // Sleep for 100ms
                Thread.Sleep(1);
                
                // Process line if cancellation not invoked
                if (worker.CancellationPending == true)
                {
                    e.Cancel = true;
                    break;
                }
                else
                {
                    lineCount++;
                    // Perform a time consuming operation and report progress
                    if (line.ContainsIgnoreCase(searchText))
                    {
                        // Report progress to main thread (to populate listView for matched Item)
                        worker.ReportProgress(0, new ListItem(lineCount, line/*,++matchCount*/));
                    }
                }
            }
        }

        private void backgroundWorker_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {

            // Fetch object passed from bgWorkerThread
            ListItem lineItem = (ListItem)e.UserState;

            // Update match Count view label
            matchCntLabel.Text = ++matchCount + "";

            // Also update local variable for total lines count
            linesCntLabel.Text = lineCount + "";

            // Add the matched line# and line to ListView
            ListViewItem item = new ListViewItem(lineItem.getLineNo() + "");
            item.SubItems.Add(lineItem.getLine());
            listView.Items.Add(item);
            listView.EnsureVisible(listView.Items.Count - 1);

        }

        private void backgroundWorker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            if (!e.Cancelled && e.Error == null)
            {
                // On neither error nor cancellation, the task has come to graceful completion
                if (matchCount == 0)
                    statusLabel.Text = "Search returned 0 results";
                else
                    statusLabel.Text = "Search complete";
            }
            else if (e.Cancelled == true)
            {
                // Cancellation invoked
                statusLabel.Text = "Search cancelled";
            }
            else if (e.Error != null)
            {
                // Internal program error occurred (which should never happen !)
                statusLabel.Text = "Error during Search: " + e.Error.Message;
            }
            linesCntLabel.Text = lineCount + "";
        
            // Reset labels
            searchBtn.Text = "Search";
            // if cancel had been called, button would be disabled, enable it again for Search
            searchBtn.Enabled = true;
            lineCount = 0;
            matchCount = 0;
            // User can type next search input text
            searchInput.Focus();
        }

        /**
         * Redraw listView column header 
         */
        public static void listView_DrawColumnHeader(object sender, DrawListViewColumnHeaderEventArgs e)
        {

            using (var sf = new StringFormat())
            {
                sf.Alignment = StringAlignment.Center;
                using (var headerFont = new Font("Microsoft Sans Serif", 10, FontStyle.Bold))
                {
                    e.Graphics.FillRectangle(Brushes.AliceBlue, e.Bounds);
                    e.Graphics.DrawString(e.Header.Text, headerFont,
                        Brushes.Black, e.Bounds, sf);
                }
            }
        }
    }
}
