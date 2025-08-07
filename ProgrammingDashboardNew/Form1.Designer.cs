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
            jobsGrid.Location = new Point(12, 95);
            jobsGrid.Name = "jobsGrid";
            jobsGrid.Size = new Size(1608, 559);
            jobsGrid.TabIndex = 0;
            // 
            // releasedJobs
            // 
            releasedJobs.AutoSize = true;
            releasedJobs.Checked = true;
            releasedJobs.CheckState = CheckState.Checked;
            releasedJobs.Location = new Point(36, 60);
            releasedJobs.Name = "releasedJobs";
            releasedJobs.Size = new Size(126, 19);
            releasedJobs.TabIndex = 1;
            releasedJobs.Text = "Released Jobs Only";
            releasedJobs.UseVisualStyleBackColor = true;
            releasedJobs.CheckedChanged += releasedJobs_CheckedChanged;
            // 
            // operationFilter
            // 
            operationFilter.FormattingEnabled = true;
            operationFilter.Location = new Point(224, 58);
            operationFilter.Name = "operationFilter";
            operationFilter.Size = new Size(116, 23);
            operationFilter.TabIndex = 2;
            operationFilter.SelectedIndexChanged += operationFilter_SelectedIndexChanged;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(346, 60);
            label1.Name = "label1";
            label1.Size = new Size(89, 15);
            label1.TabIndex = 3;
            label1.Text = "Operation Filter";
            // 
            // refreshButton
            // 
            refreshButton.Location = new Point(1517, 891);
            refreshButton.Name = "refreshButton";
            refreshButton.Size = new Size(103, 23);
            refreshButton.TabIndex = 4;
            refreshButton.Text = "Refresh Data";
            refreshButton.UseVisualStyleBackColor = true;
            refreshButton.Click += refreshButton_Click;
            // 
            // startedButton
            // 
            startedButton.Location = new Point(12, 681);
            startedButton.Name = "startedButton";
            startedButton.Size = new Size(118, 23);
            startedButton.TabIndex = 5;
            startedButton.Text = "Started";
            startedButton.UseVisualStyleBackColor = true;
            startedButton.Click += startedButton_Click;
            // 
            // clearTimestamp
            // 
            clearTimestamp.Location = new Point(12, 780);
            clearTimestamp.Name = "clearTimestamp";
            clearTimestamp.Size = new Size(118, 23);
            clearTimestamp.TabIndex = 6;
            clearTimestamp.Text = "Clear Timestamp";
            clearTimestamp.UseVisualStyleBackColor = true;
            clearTimestamp.Click += clearTimestamp_Click;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1632, 926);
            Controls.Add(clearTimestamp);
            Controls.Add(startedButton);
            Controls.Add(refreshButton);
            Controls.Add(label1);
            Controls.Add(operationFilter);
            Controls.Add(releasedJobs);
            Controls.Add(jobsGrid);
            Name = "Form1";
            Text = "Form1";
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
    }
}
