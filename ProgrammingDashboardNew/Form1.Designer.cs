namespace ProgrammingDashboardNew
{
    partial class Form1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            jobsGrid = new DataGridView();
            releasedJobs = new CheckBox();
            operationFilter = new ComboBox();
            label1 = new Label();
            refreshButton = new Button();
            startedButton = new Button();
            clearTimestamp = new Button();
            label2 = new Label();
            ((System.ComponentModel.ISupportInitialize)jobsGrid).BeginInit();
            SuspendLayout();
            // 
            // jobsGrid
            // 
            jobsGrid.AllowUserToAddRows = false;
            jobsGrid.AllowUserToDeleteRows = false;
            jobsGrid.AllowUserToResizeColumns = false;
            jobsGrid.AllowUserToResizeRows = false;
            jobsGrid.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            jobsGrid.Location = new Point(12, 108);
            jobsGrid.Name = "jobsGrid";
            jobsGrid.Size = new Size(1372, 525);
            jobsGrid.TabIndex = 0;
            // 
            // releasedJobs
            // 
            releasedJobs.AutoSize = true;
            releasedJobs.Checked = true;
            releasedJobs.CheckState = CheckState.Checked;
            releasedJobs.Location = new Point(28, 79);
            releasedJobs.Name = "releasedJobs";
            releasedJobs.Size = new Size(145, 23);
            releasedJobs.TabIndex = 1;
            releasedJobs.Text = "Released Jobs Only";
            releasedJobs.UseVisualStyleBackColor = true;
            releasedJobs.CheckedChanged += releasedJobs_CheckedChanged;
            // 
            // operationFilter
            // 
            operationFilter.FormattingEnabled = true;
            operationFilter.Location = new Point(179, 78);
            operationFilter.Name = "operationFilter";
            operationFilter.Size = new Size(95, 25);
            operationFilter.TabIndex = 2;
            operationFilter.SelectedIndexChanged += operationFilter_SelectedIndexChanged;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(280, 81);
            label1.Name = "label1";
            label1.Size = new Size(105, 19);
            label1.TabIndex = 3;
            label1.Text = "Operation Filter";
            // 
            // refreshButton
            // 
            refreshButton.Location = new Point(12, 751);
            refreshButton.Name = "refreshButton";
            refreshButton.Size = new Size(118, 26);
            refreshButton.TabIndex = 4;
            refreshButton.Text = "Refresh Data";
            refreshButton.UseVisualStyleBackColor = true;
            refreshButton.Click += refreshButton_Click;
            // 
            // startedButton
            // 
            startedButton.Location = new Point(12, 687);
            startedButton.Name = "startedButton";
            startedButton.Size = new Size(118, 26);
            startedButton.TabIndex = 5;
            startedButton.Text = "Started";
            startedButton.UseVisualStyleBackColor = true;
            startedButton.Click += startedButton_Click;
            // 
            // clearTimestamp
            // 
            clearTimestamp.Location = new Point(12, 719);
            clearTimestamp.Name = "clearTimestamp";
            clearTimestamp.Size = new Size(118, 26);
            clearTimestamp.TabIndex = 6;
            clearTimestamp.Text = "Clear Timestamp";
            clearTimestamp.UseVisualStyleBackColor = true;
            clearTimestamp.Click += clearTimestamp_Click;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Font = new Font("Segoe UI", 30F);
            label2.Location = new Point(361, 9);
            label2.Name = "label2";
            label2.Size = new Size(559, 54);
            label2.TabIndex = 7;
            label2.Text = "PROGRAMMING DASHBOARD";
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(7F, 17F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.White;
            ClientSize = new Size(1400, 809);
            Controls.Add(label2);
            Controls.Add(clearTimestamp);
            Controls.Add(startedButton);
            Controls.Add(refreshButton);
            Controls.Add(label1);
            Controls.Add(operationFilter);
            Controls.Add(releasedJobs);
            Controls.Add(jobsGrid);
            Font = new Font("Segoe UI", 10F);
            FormBorderStyle = FormBorderStyle.FixedSingle;
            Name = "Form1";
            ShowIcon = false;
            Load += Form1_Load;
            ((System.ComponentModel.ISupportInitialize)jobsGrid).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private DataGridView jobsGrid;
        private CheckBox releasedJobs;
        private ComboBox operationFilter;
        private Label label1;
        private Button refreshButton;
        private Button startedButton;
        private Button clearTimestamp;
        private Label label2;
    }
}
