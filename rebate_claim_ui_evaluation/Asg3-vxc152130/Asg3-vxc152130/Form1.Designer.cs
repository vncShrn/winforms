namespace Asg3_vxc152130
{
    partial class Form1
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea3 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Legend legend3 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            System.Windows.Forms.DataVisualization.Charting.Series series7 = new System.Windows.Forms.DataVisualization.Charting.Series();
            System.Windows.Forms.DataVisualization.Charting.Series series8 = new System.Windows.Forms.DataVisualization.Charting.Series();
            System.Windows.Forms.DataVisualization.Charting.Series series9 = new System.Windows.Forms.DataVisualization.Charting.Series();
            System.Windows.Forms.DataVisualization.Charting.Title title3 = new System.Windows.Forms.DataVisualization.Charting.Title();
            this.toolTipFields = new System.Windows.Forms.ToolTip(this.components);
            this.btnInputFile = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.label_Browse = new System.Windows.Forms.Label();
            this.panel2 = new System.Windows.Forms.Panel();
            this.groupBox_Results = new System.Windows.Forms.GroupBox();
            this.chart_Time = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.listView_Results = new System.Windows.Forms.ListView();
            this.columnHeader1 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader2 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.panel2.SuspendLayout();
            this.groupBox_Results.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.chart_Time)).BeginInit();
            this.SuspendLayout();
            // 
            // btnInputFile
            // 
            this.btnInputFile.BackColor = System.Drawing.SystemColors.Menu;
            this.btnInputFile.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.900001F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnInputFile.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.btnInputFile.Location = new System.Drawing.Point(56, 17);
            this.btnInputFile.Margin = new System.Windows.Forms.Padding(4);
            this.btnInputFile.Name = "btnInputFile";
            this.btnInputFile.Size = new System.Drawing.Size(445, 103);
            this.btnInputFile.TabIndex = 0;
            this.btnInputFile.Text = "BROWSE";
            this.toolTipFields.SetToolTip(this.btnInputFile, "Select the rebate file containing all entered records to perform analysis, otherw" +
        "ise default file is taken");
            this.btnInputFile.UseVisualStyleBackColor = false;
            this.btnInputFile.Click += new System.EventHandler(this.btnInputFile_Click);
            // 
            // button1
            // 
            this.button1.BackColor = System.Drawing.SystemColors.Menu;
            this.button1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.900001F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button1.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.button1.Location = new System.Drawing.Point(56, 185);
            this.button1.Margin = new System.Windows.Forms.Padding(4);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(445, 103);
            this.button1.TabIndex = 2;
            this.button1.Text = "RUN";
            this.toolTipFields.SetToolTip(this.button1, "Perform evaluation on the selected input file");
            this.button1.UseVisualStyleBackColor = false;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.DefaultExt = "txt";
            this.openFileDialog1.FileName = "CS6326Asg2.txt";
            this.openFileDialog1.Filter = "txt files (*.txt)|*.txt";
            this.openFileDialog1.InitialDirectory = "\"C:\\\"";
            this.openFileDialog1.RestoreDirectory = true;
            this.openFileDialog1.Title = "Browse Rebate Text File";
            // 
            // label_Browse
            // 
            this.label_Browse.AutoSize = true;
            this.label_Browse.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.900001F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label_Browse.Location = new System.Drawing.Point(598, 49);
            this.label_Browse.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label_Browse.MaximumSize = new System.Drawing.Size(1500, 0);
            this.label_Browse.Name = "label_Browse";
            this.label_Browse.Size = new System.Drawing.Size(0, 39);
            this.label_Browse.TabIndex = 1;
            this.label_Browse.Visible = false;
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.btnInputFile);
            this.panel2.Controls.Add(this.label_Browse);
            this.panel2.Controls.Add(this.button1);
            this.panel2.Location = new System.Drawing.Point(57, 103);
            this.panel2.Margin = new System.Windows.Forms.Padding(4);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(1918, 352);
            this.panel2.TabIndex = 0;
            // 
            // groupBox_Results
            // 
            this.groupBox_Results.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.groupBox_Results.Controls.Add(this.chart_Time);
            this.groupBox_Results.Controls.Add(this.listView_Results);
            this.groupBox_Results.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.900001F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox_Results.Location = new System.Drawing.Point(57, 463);
            this.groupBox_Results.Margin = new System.Windows.Forms.Padding(4);
            this.groupBox_Results.Name = "groupBox_Results";
            this.groupBox_Results.Padding = new System.Windows.Forms.Padding(4);
            this.groupBox_Results.Size = new System.Drawing.Size(1918, 1151);
            this.groupBox_Results.TabIndex = 13;
            this.groupBox_Results.TabStop = false;
            this.groupBox_Results.Text = "RESULTS";
            this.groupBox_Results.Visible = false;
            // 
            // chart_Time
            // 
            this.chart_Time.BorderlineColor = System.Drawing.Color.Black;
            this.chart_Time.BorderlineDashStyle = System.Windows.Forms.DataVisualization.Charting.ChartDashStyle.Solid;
            chartArea3.AxisX.Title = "type";
            chartArea3.AxisY.Title = "mm:ss";
            chartArea3.Name = "ChartArea1";
            this.chart_Time.ChartAreas.Add(chartArea3);
            legend3.Name = "Legend1";
            this.chart_Time.Legends.Add(legend3);
            this.chart_Time.Location = new System.Drawing.Point(890, 60);
            this.chart_Time.Margin = new System.Windows.Forms.Padding(4);
            this.chart_Time.Name = "chart_Time";
            this.chart_Time.Palette = System.Windows.Forms.DataVisualization.Charting.ChartColorPalette.Berry;
            series7.ChartArea = "ChartArea1";
            series7.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.StackedColumn;
            series7.Legend = "Legend1";
            series7.Name = "min";
            series7.YValueType = System.Windows.Forms.DataVisualization.Charting.ChartValueType.Time;
            series8.ChartArea = "ChartArea1";
            series8.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.StackedColumn;
            series8.Legend = "Legend1";
            series8.Name = "avg";
            series8.YValueType = System.Windows.Forms.DataVisualization.Charting.ChartValueType.Time;
            series9.ChartArea = "ChartArea1";
            series9.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.StackedColumn;
            series9.Legend = "Legend1";
            series9.Name = "max";
            series9.YValueType = System.Windows.Forms.DataVisualization.Charting.ChartValueType.Time;
            this.chart_Time.Series.Add(series7);
            this.chart_Time.Series.Add(series8);
            this.chart_Time.Series.Add(series9);
            this.chart_Time.Size = new System.Drawing.Size(986, 1047);
            this.chart_Time.TabIndex = 4;
            this.chart_Time.Text = "chart1";
            title3.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.900001F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            title3.Name = "Title1";
            title3.Text = "Time Duration";
            this.chart_Time.Titles.Add(title3);
            // 
            // listView_Results
            // 
            this.listView_Results.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader1,
            this.columnHeader2});
            this.listView_Results.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.900001F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.listView_Results.FullRowSelect = true;
            this.listView_Results.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.None;
            this.listView_Results.Location = new System.Drawing.Point(22, 60);
            this.listView_Results.Margin = new System.Windows.Forms.Padding(4);
            this.listView_Results.Name = "listView_Results";
            this.listView_Results.Size = new System.Drawing.Size(835, 1047);
            this.listView_Results.TabIndex = 3;
            this.listView_Results.UseCompatibleStateImageBehavior = false;
            this.listView_Results.View = System.Windows.Forms.View.Details;
            // 
            // columnHeader1
            // 
            this.columnHeader1.Width = 600;
            // 
            // columnHeader2
            // 
            this.columnHeader2.Width = 200;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(19F, 38F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.ClientSize = new System.Drawing.Size(2039, 1728);
            this.Controls.Add(this.groupBox_Results);
            this.Controls.Add(this.panel2);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.900001F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "Form1";
            this.Text = "Evaluation";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.groupBox_Results.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.chart_Time)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ToolTip toolTipFields;
        private System.Windows.Forms.Button btnInputFile;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private System.Windows.Forms.Label label_Browse;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.GroupBox groupBox_Results;
        private System.Windows.Forms.ListView listView_Results;
        private System.Windows.Forms.ColumnHeader columnHeader1;
        private System.Windows.Forms.ColumnHeader columnHeader2;
        private System.Windows.Forms.DataVisualization.Charting.Chart chart_Time;
    }
}

