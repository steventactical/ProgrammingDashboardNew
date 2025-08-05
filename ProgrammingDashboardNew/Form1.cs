using System;
using System.Data;
using System.Data.SQLite;
using System.Windows.Forms;

namespace ProgrammingDashboardNew
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            string dbPath = "Data\\MockERP.db";
            string connectionString = $"Data Source={dbPath};Version=3;";

            using (SQLiteConnection conn = new SQLiteConnection(connectionString))
            {
                conn.Open();
                //string query = "SELECT * FROM Job"; // or use your custom query
                string query = "SELECT \r\n    " +
                    "Q1.Work_Center AS sWork_Center,\r\n   " +
                    "Q1.Job AS sJob,\r\n    " +
                    "Q1.Status AS sStatus,\r\n    " +
                    "Q1.Customer AS sCustomer,\r\n    " +
                    "Q1.Part_Number AS sPart_Number,\r\n    " +
                    "Q1.Drawing AS sDrawing,\r\n    " +
                    "Q1.Description AS sDescription,\r\n    " +
                    "Q1.Rev AS sRev,\r\n    " +
                    "Q1.Make_Quantity AS nMake_Quantity,\r\n    " +
                    "Q1.WC_Vendor AS sWC_Vendor,\r\n    " +
                    "Q1.JPri AS JPri,\r\n    " +
                    "Q1.Sequence AS nSequence,\r\n    " +
                    "Q1.Floor_Notes AS Current_User,\r\n\r\n      " +
                    "SUBSTR(\r\n        " +
                    "Q1.Floor_Notes,\r\n        " +
                    "INSTR(Q1.Floor_Notes, '[') + 1,\r\n        " +
                    "INSTR(Q1.Floor_Notes || ' ', ' ') - INSTR(Q1.Floor_Notes, '[') - 1\r\n    ) AS Parsed_User,\r\n\r\n    " +
                    "STRFTIME('%Y-%m-%d', MIN(Q1.Promised_Date)) AS MinOfPromised_Date,\r\n    " +
                    "STRFTIME('%Y-%m-%d', Q1.Released_Date) AS Released_Date\r\n\r\n" +
                    "FROM (\r\n    SELECT \r\n        " +
                    "Work_Center.Work_Center,\r\n        " +
                    "Job.Job,\r\n        " +
                    "Job.Status,\r\n        " +
                    "Job.Customer,\r\n        " +
                    "Job.Part_Number,\r\n        " +
                    "Job.Drawing,\r\n        " +
                    "Job.Description,\r\n        " +
                    "Job.Rev,\r\n        " +
                    "Job.Make_Quantity,\r\n        " +
                    "Job_Operation.WC_Vendor,\r\n        " +
                    "Job.Priority AS JPri,\r\n        " +
                    "Job_Operation.Sequence,\r\n        " +
                    "Q2.Promised_Date,\r\n        " +
                    "Job.Released_Date,\r\n        " +
                    "Job_Operation.Floor_Notes\r\n    " +
                    "FROM \r\n        " +
                    "Work_Center \r\n        " +
                    "INNER JOIN Job_Operation " +
                    "ON Work_Center.Work_Center = Job_Operation.Work_Center\r\n        " +
                    "INNER JOIN Job " +
                    "ON Job.Job = Job_Operation.Job\r\n        " +
                    "LEFT JOIN (\r\n            " +
                    "SELECT \r\n                " +
                    "Job,\r\n                " +
                    "Remaining_Quantity,\r\n                " +
                    "Promised_Date\r\n            " +
                    "FROM \r\n                " +
                    "Delivery\r\n            " +
                    "WHERE \r\n                " +
                    "Remaining_Quantity > 0\r\n        " +
                    ") AS Q2 ON Job.Top_Lvl_Job = Q2.Job\r\n    " +
                    "WHERE \r\n        " +
                    "(Job_Operation.Status = 'o' OR Job_Operation.Status = 's') \r\n        " +
                    "AND Work_Center.Work_Center IN ('230 PROG', '220 MODEL', '200 TOOL', 'PROG ARROW')\r\n) AS Q1\r\nGROUP BY  \r\n    Q1.Work_Center, \r\n    Q1.Job,\r\n    Q1.Status,\r\n    Q1.Customer, \r\n    Q1.Part_Number, \r\n    Q1.Description,\r\n    Q1.Rev, \r\n    Q1.Make_Quantity,  \r\n    Q1.WC_Vendor, \r\n    Q1.JPri,\r\n    Q1.Sequence, \r\n    Q1.Released_Date, \r\n    Q1.Drawing, \r\n    Q1.Floor_Notes\r\n";
                SQLiteDataAdapter adapter = new SQLiteDataAdapter(query, conn);
                DataTable dt = new DataTable();
                adapter.Fill(dt);
                jobsGrid.DataSource = dt;
            }
        }
    }
}
