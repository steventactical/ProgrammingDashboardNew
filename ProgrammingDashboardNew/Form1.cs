using System;
using System.Data;
using System.Data.SQLite;
using Microsoft.Data.SqlClient;
using System.Windows.Forms;
using System.ComponentModel;

namespace ProgrammingDashboardNew
{
    public partial class Form1 : Form
    {
        private DataTable dt;
        private readonly bool useSqlite = true; // Set to true for SQLite, false for SQL Server

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            ReloadData();
            PopulateOperationList();
            ApplyFilters();
            ApplyFormatting();
        }

        private void ApplyFormatting()
        {
            // Form level formatting
            this.BackColor = Color.FromArgb(11, 28, 44);
            this.ForeColor = Color.White;
            this.Font = new Font("Segoe UI", 10);

            // Controls formatting
            refreshButton.FlatStyle = FlatStyle.Flat;
            refreshButton.BackColor = Color.FromArgb(255, 75, 62);
            refreshButton.ForeColor = Color.White;
            refreshButton.FlatAppearance.BorderSize = 0;
            startedButton.FlatStyle = FlatStyle.Flat;
            startedButton.BackColor = Color.FromArgb(255, 75, 62);
            startedButton.ForeColor = Color.White;
            startedButton.FlatAppearance.BorderSize = 0;
            clearTimestamp.FlatStyle = FlatStyle.Flat;
            clearTimestamp.BackColor = Color.FromArgb(255, 75, 62);
            clearTimestamp.ForeColor = Color.White;
            clearTimestamp.FlatAppearance.BorderSize = 0;
            completeButton.FlatStyle = FlatStyle.Flat;
            completeButton.BackColor = Color.FromArgb(255, 75, 62);
            completeButton.ForeColor = Color.White;
            completeButton.FlatAppearance.BorderSize = 0;
            resetMockDatabase.FlatStyle = FlatStyle.Flat;
            resetMockDatabase.BackColor = Color.FromArgb(255, 75, 62);
            resetMockDatabase.ForeColor = Color.White;
            resetMockDatabase.FlatAppearance.BorderSize = 0;
            if(useSqlite)
            {
                resetMockDatabase.Visible = true;
            }
            else
            {
                resetMockDatabase.Visible = false;
            }

            //DataGridView formatting
            jobsGrid.BackgroundColor = Color.White;
            jobsGrid.GridColor = Color.FromArgb(200, 200, 200);
            jobsGrid.DefaultCellStyle.BackColor = Color.White;
            jobsGrid.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(240, 240, 240);
            jobsGrid.DefaultCellStyle.ForeColor = Color.Black;
            jobsGrid.DefaultCellStyle.SelectionBackColor = Color.FromArgb(255, 75, 62);
            jobsGrid.DefaultCellStyle.SelectionForeColor = Color.White;
            jobsGrid.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(11, 28, 44);
            jobsGrid.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
            jobsGrid.EnableHeadersVisualStyles = false;

        }

        private void ReloadData()
        {
            if (useSqlite)
            {
                LoadFromSQLite();
            }
            else
            {
                LoadFromSqlServer();
            }
        }

        // Populates combobox with operation filter options
        private void PopulateOperationList()
        {
            operationFilter.Items.Clear();
            operationFilter.Items.Add("200 TOOL");
            operationFilter.Items.Add("220 MODEL");
            operationFilter.Items.Add("230 PROG");
            operationFilter.SelectedIndex = -1;
        }

        // Loads data from SQLite database
        private void LoadFromSQLite()
        {
            string dbPath = Path.Combine(Application.StartupPath, "Data", "MockERP.db");
            string connectionString = $"Data Source={dbPath};Version=3;";
            string query = @"SELECT 
            Q1.Job AS sJob,
            Q1.Sequence AS nSeq,
            Q1.Status AS sStatus,
            STRFTIME('%Y-%m-%d', Q1.Released_Date) AS Released_Date,
            Q1.Work_Center AS sWork_Center,
            Q1.Customer AS sCustomer,
            Q1.Part_Number AS sPart_Number,
            Q1.Drawing AS sDrawing,
            Q1.Rev AS sRev,
            Q1.Make_Quantity AS nMake_Quantity,
            Q1.JPri || ' ' || STRFTIME('%Y-%m-%d', MIN(Q1.Promised_Date)) AS JPri_DueDate,
            Q1.Floor_Notes AS Time_Stamp

            FROM (
            SELECT 
                Work_Center.Work_Center,
                Job.Job,
                Job.Status,
                Job.Customer,
                Job.Part_Number,
                Job.Drawing,
                Job.Description,
                Job.Rev,
                Job.Make_Quantity,
                Job_Operation.WC_Vendor,
                Job.Priority AS JPri,
                Job_Operation.Sequence,
                Q2.Promised_Date,
                Job.Released_Date,
                Job_Operation.Floor_Notes
            FROM 
                Work_Center 
                INNER JOIN Job_Operation ON Work_Center.Work_Center = Job_Operation.Work_Center
                INNER JOIN Job ON Job.Job = Job_Operation.Job
                LEFT JOIN (
                    SELECT 
                        Job,
                        Remaining_Quantity,
                        Promised_Date
                    FROM 
                        Delivery
                    WHERE 
                        Remaining_Quantity > 0
                ) AS Q2 ON Job.Top_Lvl_Job = Q2.Job
            WHERE 
                (Job_Operation.Status = 'o' OR Job_Operation.Status = 's') 
                AND Work_Center.Work_Center IN ('230 PROG', '220 MODEL', '200 TOOL')
            ) AS Q1

            GROUP BY  
                Q1.Work_Center, 
                Q1.Job,
                Q1.Status,
                Q1.Customer, 
                Q1.Part_Number, 
                Q1.Description,
                Q1.Rev, 
                Q1.Make_Quantity,  
                Q1.JPri,
                Q1.Sequence, 
                Q1.Released_Date, 
                Q1.Drawing, 
                Q1.Floor_Notes;";

            using (var conn = new SQLiteConnection(connectionString))
            {
                conn.Open();
                SQLiteDataAdapter adapter = new SQLiteDataAdapter(query, conn);
                dt = new DataTable();
                adapter.Fill(dt);
                jobsGrid.DataSource = dt;
            }
            FormatJobTable();
        }

        // Returns the SQL Server connection string
        private string GetSQLServerConnectionString()
        {
            return "Server=Clyde;Database=EMPOWER;Trusted_Connection=True;TrustServerCertificate=True;";
        }

        // Loads data from SQL Server database
        private void LoadFromSqlServer()
        {
            string query = @"SELECT 
            Q1.Job AS sJob,
            Q1.Sequence AS nSeq,
            Q1.Status AS sStatus,
            CONVERT(VARCHAR(10), Q1.Released_Date, 120) AS Released_Date,
            Q1.Work_Center AS sWork_Center,
            Q1.Customer AS sCustomer,
            Q1.Part_Number AS sPart_Number,
            Q1.Drawing AS sDrawing,
            Q1.Rev AS sRev,
            Q1.Make_Quantity AS nMake_Quantity,
            Q1.JPri + ' ' + CONVERT(VARCHAR(10), MIN(Q1.Promised_Date), 120) AS JPri_DueDate,
            Q1.Floor_Notes AS Time_Stamp
            FROM (
            SELECT 
                Work_Center.Work_Center,
                Job.Job,
                Job.Status,
                Job.Customer,
                Job.Part_Number,
                Job.Drawing,
                Job.Description,
                Job.Rev,
                Job.Make_Quantity,
                Job_Operation.WC_Vendor,
                CAST(Job.Priority AS VARCHAR(10)) AS JPri,
                Job_Operation.Sequence,
                Q2.Promised_Date,
                CAST(Job.Released_Date AS DATE) AS Released_Date,
                CAST(Job_Operation.Floor_Notes AS VARCHAR(8000)) AS Floor_Notes
            FROM 
                Work_Center 
                INNER JOIN Job_Operation ON Work_Center.Work_Center = Job_Operation.Work_Center
                INNER JOIN Job ON Job.Job = Job_Operation.Job
                LEFT JOIN (
                    SELECT 
                        Job,
                        Remaining_Quantity,
                        CAST(Promised_Date AS DATE) AS Promised_Date
                    FROM 
                        Delivery
                    WHERE 
                        Remaining_Quantity > 0
                ) AS Q2 ON Job.Top_Lvl_Job = Q2.Job
            WHERE 
                (Job_Operation.Status = 'o' OR Job_Operation.Status = 's') 
                AND Work_Center.Work_Center IN ('230 PROG', '220 MODEL', '200 TOOL')
            ) Q1
            GROUP BY  
                Q1.Work_Center, 
                Q1.Job,
                Q1.Status,
                Q1.Customer, 
                Q1.Part_Number, 
                Q1.Description,
                Q1.Rev, 
                Q1.Make_Quantity,  
                Q1.JPri,
                Q1.Sequence, 
                Q1.Released_Date, 
                Q1.Drawing, 
                Q1.Floor_Notes;";

            using (var conn = new SqlConnection(GetSQLServerConnectionString()))
            {
                conn.Open();
                SqlDataAdapter adapter = new SqlDataAdapter(query, conn);
                dt = new DataTable();
                adapter.Fill(dt);
                jobsGrid.DataSource = dt;
            }
            FormatJobTable();
        }

        // Formats the DataGridView columns
        private void FormatJobTable()
        {
            // Rename column headers
            jobsGrid.Columns["sJob"].HeaderText = "Job";
            jobsGrid.Columns["nSeq"].HeaderText = "Seq";
            jobsGrid.Columns["sStatus"].HeaderText = "Status";
            jobsGrid.Columns["Released_Date"].HeaderText = "Released Date";
            jobsGrid.Columns["sWork_Center"].HeaderText = "Operation";
            jobsGrid.Columns["sCustomer"].HeaderText = "Customer";
            jobsGrid.Columns["sPart_Number"].HeaderText = "Part #";
            jobsGrid.Columns["sDrawing"].HeaderText = "Drawing";
            jobsGrid.Columns["sRev"].HeaderText = "Revision";
            jobsGrid.Columns["nMake_Quantity"].HeaderText = "Qty";
            jobsGrid.Columns["JPri_DueDate"].HeaderText = "Priority / Due";
            jobsGrid.Columns["Time_Stamp"].HeaderText = "Time Stamp";
            // Resize and align columns
            jobsGrid.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            jobsGrid.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            jobsGrid.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            jobsGrid.RowHeadersWidth = 20;
            jobsGrid.Columns["nSeq"].Width = 40;
            jobsGrid.Columns["nMake_Quantity"].Width = 50;
            jobsGrid.Columns["sRev"].Width = 60;
            jobsGrid.Columns["JPri_DueDate"].Width = 115;
            jobsGrid.Sort(jobsGrid.Columns["JPri_DueDate"], ListSortDirection.Ascending);
        }

        // Event handler for checkbox to filter released jobs
        private void releasedJobs_CheckedChanged(object sender, EventArgs e)
        {
            ApplyFilters();
        }

        // Event handler for operation filter selection change
        private void operationFilter_SelectedIndexChanged(object sender, EventArgs e)
        {
            ApplyFilters();
        }

        // Applies filters based on user selections
        private void ApplyFilters()
        {
            if (dt == null) return;

            DataView dv = new DataView(dt);
            List<string> filters = new List<string>();

            // Filter: Released jobs only
            if (releasedJobs.Checked)
            {
                filters.Add("Released_Date IS NOT NULL AND Released_Date <> ''");
            }

            // Filter: Work Center / Operation
            if (operationFilter.SelectedIndex >= 0)
            {
                string selectedOp = operationFilter.SelectedItem.ToString().Replace("'", "''");
                filters.Add($"sWork_Center = '{selectedOp}'");
            }

            // Combine all filters
            dv.RowFilter = string.Join(" AND ", filters);

            jobsGrid.DataSource = dv;
            FormatJobTable();
        }

        // Event handler for refresh button click
        private void refreshButton_Click(object sender, EventArgs e)
        {
            ReloadData();
            ApplyFilters();
            FormatJobTable();
        }

        // Event handler for clicking started button
        private void startedButton_Click(object sender, EventArgs e)
        {
            if (jobsGrid.SelectedRows.Count == 0)
            {
                MessageBox.Show("Please select a row first.");
                return;
            }

            var selectedRow = jobsGrid.SelectedRows[0];
            string job = selectedRow.Cells["sJob"].Value.ToString();
            int seq = Convert.ToInt32(selectedRow.Cells["nSeq"].Value);
            string user = Environment.UserName.ToUpper();
            string timeStamp = DateTime.Now.ToString("MM/dd/yyyy HH:mm tt");
            string updateText = $"[{user} - {timeStamp}] - Started";

            if (useSqlite)
            {
                SQLiteStartedTimestamp(job, seq, updateText);
            }
            else
            {
                SQLStartedTimestamp(job, seq, updateText);
            }

            ReloadData();
            ApplyFilters();
        }

        // Updates the timestamp to started for a job operation in SQL Server
        private void SQLStartedTimestamp(string job, int seq, string updateText)
        {
            string updateQuery = @"
                UPDATE Job_Operation
                SET Floor_Notes = CAST(ISNULL(Floor_Notes, '') AS VARCHAR(MAX)) + CHAR(13) + CHAR(10) + @line
                WHERE Job = @job AND Sequence = @seq;";

            using (var conn = new SqlConnection(GetSQLServerConnectionString()))
            {
                conn.Open();
                using (var cmd = new SqlCommand(updateQuery, conn))
                {
                    cmd.Parameters.AddWithValue("@line", updateText);
                    cmd.Parameters.AddWithValue("@job", job);
                    cmd.Parameters.AddWithValue("@seq", seq);
                    cmd.ExecuteNonQuery();
                }
            }
        }

        // Updates the timestamp to started for a job operation in SQLite
        private void SQLiteStartedTimestamp(string job, int seq, string updateText)
        {
            string dbPath = Path.Combine(Application.StartupPath, "Data", "MockERP.db");
            string connectionString = $"Data Source={dbPath};Version=3;";
            string updateQuery = @"
                UPDATE Job_Operation
                SET Floor_Notes = COALESCE(Floor_Notes, '') || CHAR(13) || CHAR(10) || @line
                WHERE Job = @job AND Sequence = @seq;";

            using (var conn = new SQLiteConnection(connectionString))
            {
                conn.Open();
                using (var cmd = new SQLiteCommand(updateQuery, conn))
                {
                    cmd.Parameters.AddWithValue("@line", updateText);
                    cmd.Parameters.AddWithValue("@job", job);
                    cmd.Parameters.AddWithValue("@seq", seq);
                    cmd.ExecuteNonQuery();
                }
            }
        }

        private void clearTimestamp_Click(object sender, EventArgs e)
        {
            if (jobsGrid.SelectedRows.Count == 0)
            {
                MessageBox.Show("Please select a row first.");
                return;
            }

            var selectedRow = jobsGrid.SelectedRows[0];
            string job = selectedRow.Cells["sJob"].Value.ToString();
            int seq = Convert.ToInt32(selectedRow.Cells["nSeq"].Value);

            if (useSqlite)
            {
                SQLiteClearTimestamp(job, seq);
            }
            else
            {
                SQLClearTimestamp(job, seq);
            }

            ReloadData();
            ApplyFilters();
        }

        // Clears the timestamp for a job operation in SQLite
        private void SQLiteClearTimestamp(string job, int seq)
        {
            string dbPath = Path.Combine(Application.StartupPath, "Data", "MockERP.db");
            string connectionString = $"Data Source={dbPath};Version=3;";
            string updateQuery = @"
                UPDATE Job_Operation
                SET Floor_Notes = ''
                WHERE Job = @job AND Sequence = @seq;";

            using (var conn = new SQLiteConnection(connectionString))
            {
                conn.Open();
                using (var cmd = new SQLiteCommand(updateQuery, conn))
                {
                    cmd.Parameters.AddWithValue("@job", job);
                    cmd.Parameters.AddWithValue("@seq", seq);
                    cmd.ExecuteNonQuery();
                }
            }
        }

        // Clears the timestamp for a job operation in SQL Server
        private void SQLClearTimestamp(string job, int seq)
        {
            string updateQuery = @"
                UPDATE Job_Operation
                SET Floor_Notes = ''
                WHERE Job = @job AND Sequence = @seq;";

            using (var conn = new SqlConnection(GetSQLServerConnectionString()))
            {
                conn.Open();
                using (var cmd = new SqlCommand(updateQuery, conn))
                {
                    cmd.Parameters.AddWithValue("@job", job);
                    cmd.Parameters.AddWithValue("@seq", seq);
                    cmd.ExecuteNonQuery();
                }
            }
        }

        private void completeButton_Click(object sender, EventArgs e)
        {
            if (jobsGrid.SelectedRows.Count == 0)
            {
                MessageBox.Show("Please select a row first.");
                return;
            }

            var selectedRow = jobsGrid.SelectedRows[0];
            string job = selectedRow.Cells["sJob"].Value.ToString();
            int seq = Convert.ToInt32(selectedRow.Cells["nSeq"].Value);
            string user = Environment.UserName.ToUpper();
            string timeStamp = DateTime.Now.ToString("MM/dd/yyyy HH:mm tt");
            string updateText = $"[{user} - {timeStamp}] - Complete";

            if (useSqlite)
            {
                SQLiteCompleteTimestamp(job, seq, updateText);
            }
            else
            {
                SQLCompleteTimestamp(job, seq, updateText);
            }

            ReloadData();
            ApplyFilters();
        }

        // Completes the timestamp for a job operation in SQL Server and closes operation
        private void SQLCompleteTimestamp(string job, int seq, string updateText)
        {
            using (var conn = new SqlConnection(GetSQLServerConnectionString()))
            {
                conn.Open();
                string noteQuery = @"
                    UPDATE Job_Operation
                    SET Floor_Notes = CAST(ISNULL(Floor_Notes, '') AS VARCHAR(MAX)) 
                        + CHAR(13) + CHAR(10) + @line
                    WHERE Job = @job AND Sequence = @seq;";

                using (var cmd1 = new SqlCommand(noteQuery, conn))
                {
                    cmd1.Parameters.AddWithValue("@line", updateText);
                    cmd1.Parameters.AddWithValue("@job", job);
                    cmd1.Parameters.AddWithValue("@seq", seq);
                    cmd1.ExecuteNonQuery();
                }

                string statusQuery = @"
                    UPDATE Job_Operation
                    SET Status = 'C'
                    WHERE Job = @job AND Work_Center IN ('230 PROG', '220 MODEL');";

                using (var cmd2 = new SqlCommand(statusQuery, conn))
                {
                    cmd2.Parameters.AddWithValue("@job", job);
                    cmd2.ExecuteNonQuery();
                }
            }
        }

        // Completes the timestamp for a job operation in SQLite and closes operation
        private void SQLiteCompleteTimestamp(string job, int seq, string updateText)
        {
            string dbPath = Path.Combine(Application.StartupPath, "Data", "MockERP.db");
            string connectionString = $"Data Source={dbPath};Version=3;";

            using (var conn = new SQLiteConnection(connectionString))
            {
                conn.Open();
                string updateNotesQuery = @"
                    UPDATE Job_Operation
                    SET Floor_Notes = COALESCE(Floor_Notes, '') || CHAR(13) || CHAR(10) || @line
                    WHERE Job = @job AND Sequence = @seq;";

                using (var cmd1 = new SQLiteCommand(updateNotesQuery, conn))
                {
                    cmd1.Parameters.AddWithValue("@line", updateText);
                    cmd1.Parameters.AddWithValue("@job", job);
                    cmd1.Parameters.AddWithValue("@seq", seq);
                    cmd1.ExecuteNonQuery();
                }

                string updateStatusQuery = @"
                    UPDATE Job_Operation
                    SET Status = 'Closed'
                    WHERE Job = @job AND Work_Center IN ('230 PROG', '220 MODEL');";

                using (var cmd2 = new SQLiteCommand(updateStatusQuery, conn))
                {
                    cmd2.Parameters.AddWithValue("@job", job);
                    cmd2.ExecuteNonQuery();
                }
            }
        }

        private void resetMockDatabase_Click(object sender, EventArgs e)
        {
            ResetMockDatabase();
        }

        // Resets the mock database by setting all job operations to 'o' status and clearing floor notes
        private void ResetMockDatabase()
        {
            string dbPath = Path.Combine(Application.StartupPath, "Data", "MockERP.db");
            string connectionString = $"Data Source={dbPath};Version=3;";

            using (var conn = new SQLiteConnection(connectionString))
            {
                conn.Open();

                string resetQuery = @"
            UPDATE Job_Operation
            SET 
                Status = 'o',
                Floor_Notes = NULL;";

                using (var cmd = new SQLiteCommand(resetQuery, conn))
                {
                    cmd.ExecuteNonQuery();
                }
            }

            ReloadData();
            ApplyFilters();
        }
    }
}

