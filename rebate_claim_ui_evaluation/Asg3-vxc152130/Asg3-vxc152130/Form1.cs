using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;


namespace Asg3_vxc152130
{
    public partial class Form1 : Form
    {
        private string defaultFileName = "CS6326Asg2.txt";
        private string resultsFileName = "CS6326Asg3.txt";

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            // Set form size to screen size and center
            Screen screen = Screen.FromControl(this);
            int iHeight = screen.WorkingArea.Height - this.Height;
            this.Height += iHeight;
            this.CenterToScreen();
            groupBox_Results.Height += iHeight;
            chart_Time.Height += iHeight;
            listView_Results.Height += iHeight;
        }


        private void btnInputFile_Click(object sender, EventArgs e)
        {
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                label_Browse.Text = openFileDialog1.FileName;
                label_Browse.Visible = true;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            /** Cleanup - Begin */

            listView_Results.Items.Clear();
            foreach (var series in chart_Time.Series)
                series.Points.Clear();

            /** Cleanup - End */

            groupBox_Results.Visible = true;
            
            //Set default file name
            if (label_Browse.Text == "")
            {
                label_Browse.Text = defaultFileName;
                label_Browse.Visible = true;
            }

            StreamReader sr = null;
            try
            {
                sr = new StreamReader(label_Browse.Text);
            }
            catch (FileNotFoundException)
            {
                MessageBox.Show("Please select a valid input file");
                label_Browse.Text = "";
                return;
            }

            List<string> startTimeList = new List<string>();
            List<string> endTimeList = new List<string>();

            int totalRecords = 0;
            int bkSpcCnt = 0;
            string line;
            string[] split;

            // Read the start and end times of the records from the input file
            while ((line = sr.ReadLine()) != null)
            {
                split = line.Split('\t');
                if (split.Length == 15)
                {
                    startTimeList.Add( split[12]);
                    endTimeList.Add( split[13]);
                    bkSpcCnt += Convert.ToInt32(split[14]);
                    totalRecords++;
                }
            }
            sr.Close();

            string[] startTimeArr = startTimeList.ToArray();
            string[] endTimeArr =endTimeList.ToArray();
            string[] perEntryTimeArr = new string[totalRecords];
            string[] interRecordTimeArr = new string[totalRecords-1];

            // Calculates the time taken to record per entry
            for (int k = 0; k < totalRecords; k++)
                perEntryTimeArr[k] = DateTime.Parse(endTimeArr[k]).Subtract(DateTime.Parse(startTimeArr[k])).ToString();

            // Calculates the inter-record time between entering 2 entries
            for (int k = 0; k < totalRecords - 1; k++)
                interRecordTimeArr[k] = DateTime.Parse(startTimeArr[k + 1]).Subtract(DateTime.Parse(endTimeArr[k])).ToString();

            // Min, Max and Average time for entering single record 
            string minPerEntryTime = perEntryTimeArr.Min();
            string maxPerEntryTime = perEntryTimeArr.Max();

            double sum = 0D;
            for (int i= 0 ; i < totalRecords ; i++)
                sum += TimeSpan.Parse(perEntryTimeArr[i]).Ticks;

            double avg = sum / (double)totalRecords;
            string avgPerEntryTime = new TimeSpan((long)Math.Ceiling(avg)).ToString();

            // Time calculation for gap between 2 records

            string minInterRecordTime = interRecordTimeArr.Min();
            string maxInterRecordTime = interRecordTimeArr.Max();

            sum = 0D;
            foreach (string value in interRecordTimeArr)
                sum += TimeSpan.Parse(value).Ticks;

            avg = sum / (double)(totalRecords - 1);
            string avgInterRecordTime = new TimeSpan((long)Math.Ceiling(avg)).ToString();

            // Total time 
            string totalTime = DateTime.Parse(endTimeArr[totalRecords-1]).Subtract(DateTime.Parse(startTimeArr[0])).ToString();

            this.chart_Time.ChartAreas[0].AxisY.LabelStyle.Format = "mm:ss";
            this.chart_Time.Series["min"].Points.AddXY("per-record", minPerEntryTime);
            this.chart_Time.Series["avg"].Points.AddXY("per-record", avgPerEntryTime);
            this.chart_Time.Series["max"].Points.AddXY("per-record", maxPerEntryTime);

            this.chart_Time.Series["min"].Points.AddXY("inter-record", minInterRecordTime);
            this.chart_Time.Series["avg"].Points.AddXY("inter-record", avgInterRecordTime);
            this.chart_Time.Series["max"].Points.AddXY("inter-record", maxInterRecordTime);


            var item = new ListViewItem();
            item.Text = "Total number of records";
            item.SubItems.Add("" + totalRecords);
            listView_Results.Items.Add(item);

            item = new ListViewItem();
            item.Text = "Number of backspaces used";
            item.SubItems.Add("" + bkSpcCnt);
            listView_Results.Items.Add(item);

            item = new ListViewItem();
            item.Text = "Total time taken to enter " + totalRecords + " records";
            item.SubItems.Add("" + DateTime.Parse(totalTime).ToString(@"mm\:ss"));
            listView_Results.Items.Add(item);

            item = new ListViewItem();
            item.Text = "Minimum time for an entry creation";
            item.SubItems.Add("" + DateTime.Parse(minPerEntryTime).ToString(@"mm\:ss"));
            listView_Results.Items.Add(item);

            item = new ListViewItem();
            item.Text = "Average time for an entry creation";
            item.SubItems.Add("" + DateTime.Parse(avgPerEntryTime).ToString(@"mm\:ss"));
            listView_Results.Items.Add(item);

            item = new ListViewItem();
            item.Text = "Maximum time for an entry creation";
            item.SubItems.Add("" + DateTime.Parse(maxPerEntryTime).ToString(@"mm\:ss"));
            listView_Results.Items.Add(item);

            item = new ListViewItem();
            item.Text = "Minimum inter-record delay";
            item.SubItems.Add("" + DateTime.Parse(minInterRecordTime).ToString(@"mm\:ss"));
            listView_Results.Items.Add(item);

            item = new ListViewItem();
            item.Text = "Average inter-record delay";
            item.SubItems.Add("" + DateTime.Parse(avgInterRecordTime).ToString(@"mm\:ss"));
            listView_Results.Items.Add(item);

            item = new ListViewItem();
            item.Text = "Maximum inter-record delay";
            item.SubItems.Add("" + DateTime.Parse(maxInterRecordTime).ToString(@"mm\:ss"));
            listView_Results.Items.Add(item);

            // Writes the results intp the output text file.
            using (StreamWriter sw = new StreamWriter(resultsFileName, false))
            {
                StringBuilder sb = new StringBuilder();
                sb.Append(string.Format("{0}\r\n", "Number of records: " + totalRecords));
                sb.Append(string.Format("{0}\r\n", "Minimum entry time: " + DateTime.Parse(minPerEntryTime).ToString(@"mm\:ss")));
                sb.Append(string.Format("{0}\r\n", "Maximum entry time: " + DateTime.Parse(maxPerEntryTime).ToString(@"mm\:ss")));
                sb.Append(string.Format("{0}\r\n", "Average entry time: " + DateTime.Parse(avgPerEntryTime).ToString(@"mm\:ss")));
                sb.Append(string.Format("{0}\r\n", "Minimum inter-record time: " + DateTime.Parse(minInterRecordTime).ToString(@"mm\:ss")));
                sb.Append(string.Format("{0}\r\n", "Maximum inter-record time: " + DateTime.Parse(maxInterRecordTime).ToString(@"mm\:ss")));
                sb.Append(string.Format("{0}\r\n", "Average inter-record time: " + DateTime.Parse(avgInterRecordTime).ToString(@"mm\:ss")));
                sb.Append(string.Format("{0}\r\n", "Total time: " + DateTime.Parse(totalTime).ToString(@"mm\:ss")));
                sb.Append(string.Format("{0}\r\n", "Backspace count: " + bkSpcCnt));
                sw.WriteLine(sb.ToString());
            }

        }

    }
}
