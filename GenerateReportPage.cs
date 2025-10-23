using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.OleDb;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using PdfSharp.Pdf;
using MigraDoc.DocumentObjectModel;
using MigraDoc.DocumentObjectModel.Tables;
using MigraDoc.Rendering;
using System.Drawing.Printing;





namespace LIFT_System
{
    public partial class GenerateReportPage : Form
    {
        private readonly OleDbConnection conn =
            new OleDbConnection(
                @"Provider=Microsoft.ACE.OLEDB.16.0;Data Source=C:\Users\User\OneDrive - University of Witwatersrand\Desktop\LIFT.accdb;");

        PrintDocument _printDoc;
        int _printRowIndex;
        int[] _colWidthsPx;
        int _headerHeightPx;

        public GenerateReportPage()
        {
            InitializeComponent();
            InitForm();
        }
        private void InitForm()
        {
            cbReportType.Items.Clear();
            cbReportType.Items.AddRange(new object[]
            {
                "Students (Basic)",
                "Applications: Pending",
                "Applications: All",
                "Applications by Type (Counts)",
                "Applications by Status (Counts)",
                "Applications in Last 30 Days"
            });
            cbReportType.SelectedIndex = -1;

            if (lblStatus != null) lblStatus.Text = "Ready";
        }

        private void btnGenerate_Click(object sender, EventArgs e)
        {
            string type = cbReportType?.Text?.Trim() ?? "";
            if (string.IsNullOrWhiteSpace(type))
            {
                MessageBox.Show("Please choose a report type.", "Validation",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                cbReportType?.Focus();
                return;
            }

            // 1) Run the report (fill DataTable)
            DataTable dt = RunReport(type);
            if (dt == null) return; // error already shown

            dgvReport.DataSource = dt;

            // 2) Log the report run in Report table
            LogReportRun(type);

            if (lblStatus != null) lblStatus.Text = $"Generated: {type} ({dt.Rows.Count} row(s))";
        }

        private DataTable RunReport(string type)
        {
            try
            {
                string sql;

                switch (type)
                {
                    case "Students (Basic)":
                        sql = @"SELECT StudentNo, StudentFirstName, StudentLastName,
                                       StudentInstituition, StudentDegreeProgram
                                FROM Student
                                ORDER BY StudentNo";
                        break;

                    case "Applications: Pending":
                        sql = @"SELECT FundingApplicationID, StudentNo, FundingApplicationType,
                                       FundingApplicationAmount, FundingApplicationSubmissionDate,
                                       FundingApplicationStatus
                                FROM Funding_Application
                                WHERE FundingApplicationStatus = 'Pending'
                                ORDER BY FundingApplicationID DESC";
                        break;

                    case "Applications: All":
                        sql = @"SELECT FundingApplicationID, StudentNo, FundingApplicationType,
                                       FundingApplicationAmount, FundingApplicationSubmissionDate,
                                       FundingApplicationStatus
                                FROM Funding_Application
                                ORDER BY FundingApplicationID DESC";
                        break;

                    case "Applications by Type (Counts)":
                        sql = @"SELECT FundingApplicationType AS [Type], COUNT(*) AS [Count]
                                FROM Funding_Application
                                GROUP BY FundingApplicationType
                                ORDER BY COUNT(*) DESC";
                        break;

                    case "Applications by Status (Counts)":
                        sql = @"SELECT FundingApplicationStatus AS [Status], COUNT(*) AS [Count]
                                FROM Funding_Application
                                GROUP BY FundingApplicationStatus
                                ORDER BY COUNT(*) DESC";
                        break;

                    case "Applications in Last 30 Days":
                        // Access date literal with #...#
                        // last 30 days from today
                        sql = @"SELECT FundingApplicationID, StudentNo, FundingApplicationType,
                                       FundingApplicationAmount, FundingApplicationSubmissionDate,
                                       FundingApplicationStatus
                                FROM Funding_Application
                                WHERE FundingApplicationSubmissionDate >= DateAdd('d', -30, Date())
                                ORDER BY FundingApplicationSubmissionDate DESC";
                        break;

                    default:
                        MessageBox.Show("Unknown report type selected.", "Error",
                            MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return null;
                }

                using (var da = new OleDbDataAdapter(sql, conn))
                {
                    var dt = new DataTable();
                    da.Fill(dt);
                    return dt;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error generating report:\n" + ex.Message,
                    "Report Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return null;
            }
        }

        private void LogReportRun(string reportType)
        {
            try
            {
                if (!int.TryParse(Session.LoggedInID, out int adminId))
                {
                    MessageBox.Show("Admin ID in session is not numeric. Report will not be logged.",
                        "Session", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                string insert = @"INSERT INTO Report
                                  (ReportType, ReportGeneratedDate, AdministratorID)
                                  VALUES (?, ?, ?)";

                using (var cmd = new OleDbCommand(insert, conn))
                {
                    cmd.Parameters.Add("@p1", OleDbType.VarWChar).Value = reportType;
                    cmd.Parameters.Add("@p2", OleDbType.Date).Value = DateTime.Now; // Access Date/Time
                    cmd.Parameters.Add("@p3", OleDbType.Integer).Value = adminId;

                    conn.Open();
                    cmd.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error logging report run:\n" + ex.Message,
                    "Log Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            finally
            {
                if (conn.State != System.Data.ConnectionState.Closed) conn.Close();
            }
        }

        private void btnBack_Click(object sender, EventArgs e)
        {
            AdministratorDashboardPage administratorDashboardPage = new AdministratorDashboardPage();
            administratorDashboardPage.Show();
            this.Hide();
        }

        private void btnExport_Click(object sender, EventArgs e)
        {
            // get DataTable from grid
            DataTable dt = null;
            if (dgvReport != null && dgvReport.DataSource is DataTable)
                dt = (DataTable)dgvReport.DataSource;

            if (dt == null || dt.Rows.Count == 0)
            {
                MessageBox.Show("There is no data to export.", "Export", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            // reset print state
            _printRowIndex = 0;
            _colWidthsPx = null;
            _headerHeightPx = 0;

            _printDoc = new PrintDocument();
            _printDoc.DefaultPageSettings.Landscape = dt.Columns.Count > 6; // auto landscape if many columns
            _printDoc.PrintPage += PrintReportPage;

            // preview first (user can choose Microsoft Print to PDF there)
            using (PrintPreviewDialog preview = new PrintPreviewDialog())
            {
                preview.Document = _printDoc;
                // bigger preview window
                Form f = preview as Form;
                if (f != null)
                {
                    f.WindowState = FormWindowState.Maximized;
                }
                preview.ShowDialog(this);
            }

            // optional: let user print directly (usually from preview’s print button)
            using (PrintDialog pd = new PrintDialog())
            {
                pd.Document = _printDoc;
                if (pd.ShowDialog(this) == DialogResult.OK)
                {
                    _printDoc.Print();
                }
            }
        }
        private void PrintReportPage(object sender, PrintPageEventArgs e)
        {
            // DataTable from grid
            DataTable dt = dgvReport?.DataSource as DataTable;
            if (dt == null) { e.HasMorePages = false; return; }

            // page margins
            int left = e.MarginBounds.Left;
            int top = e.MarginBounds.Top;
            int right = e.MarginBounds.Right;
            int bottom = e.MarginBounds.Bottom;
            int pageWidth = right - left;

            // fonts
            using (System.Drawing.Font titleFont = new System.Drawing.Font("Arial", 12, FontStyle.Bold))
            using (System.Drawing.Font infoFont = new System.Drawing.Font("Arial", 8, FontStyle.Regular))
            using (System.Drawing.Font headerFont = new System.Drawing.Font("Arial", 9, FontStyle.Bold))
            using (System.Drawing.Font cellFont = new System.Drawing.Font("Arial", 9, FontStyle.Regular))
            using (Pen gridPen = new Pen(System.Drawing.Color.Black, 1f))
            using (SolidBrush brush = new SolidBrush(System.Drawing.Color.Black))
            using (SolidBrush grayBrush = new SolidBrush(System.Drawing.Color.FromArgb(240, 240, 240)))
            {
                // Title + timestamp
                string title = (cbReportType != null && !string.IsNullOrWhiteSpace(cbReportType.Text))
                               ? cbReportType.Text : "Generated Report";
                e.Graphics.DrawString(title, titleFont, brush, left, top);
                string ts = "Generated: " + System.DateTime.Now.ToString("yyyy/MM/dd HH:mm");
                SizeF tsSize = e.Graphics.MeasureString(ts, infoFont);
                e.Graphics.DrawString(ts, infoFont, Brushes.Gray, right - tsSize.Width, top);

                int y = top + (int)Math.Max(titleFont.GetHeight(e.Graphics), tsSize.Height) + 8;

                // compute column widths once (proportional to column header + sample data)
                if (_colWidthsPx == null)
                {
                    _colWidthsPx = new int[dt.Columns.Count];
                    float[] desired = new float[dt.Columns.Count];
                    // base on header text
                    for (int c = 0; c < dt.Columns.Count; c++)
                    {
                        desired[c] = e.Graphics.MeasureString(dt.Columns[c].ColumnName, headerFont).Width + 20; // padding
                    }
                    // sample a few rows
                    int sample = System.Math.Min(50, dt.Rows.Count);
                    for (int r = 0; r < sample; r++)
                    {
                        for (int c = 0; c < dt.Columns.Count; c++)
                        {
                            string s = dt.Rows[r][c] == System.DBNull.Value ? "" : System.Convert.ToString(dt.Rows[r][c]);
                            float w = e.Graphics.MeasureString(s ?? "", cellFont).Width + 16;
                            if (w > desired[c]) desired[c] = w;
                        }
                    }
                    // scale to page width
                    float total = desired.Sum();
                    if (total < 1f) total = 1f;
                    for (int c = 0; c < dt.Columns.Count; c++)
                    {
                        _colWidthsPx[c] = (int)(desired[c] / total * pageWidth);
                        if (_colWidthsPx[c] < 50) _colWidthsPx[c] = 50; // min width
                    }
                }

                // draw header row background + text
                int x = left;
                int headerHeight = 0;
                for (int c = 0; c < dt.Columns.Count; c++)
                {
                    Rectangle rect = new Rectangle(x, y, _colWidthsPx[c], 24);
                    e.Graphics.FillRectangle(grayBrush, rect);
                    e.Graphics.DrawRectangle(gridPen, rect);
                    // column name
                    RectangleF textRect = new RectangleF(rect.X + 4, rect.Y + 4, rect.Width - 8, rect.Height - 8);
                    e.Graphics.DrawString(dt.Columns[c].ColumnName, headerFont, brush, textRect);
                    x += rect.Width;
                    headerHeight = rect.Height;
                }
                y += headerHeight;
                _headerHeightPx = headerHeight;

                // rows
                int rowHeight = 22; // fixed row height works well; increase if your data wraps
                while (_printRowIndex < dt.Rows.Count)
                {
                    // check page overflow
                    if (y + rowHeight > bottom)
                    {
                        e.HasMorePages = true;
                        return;
                    }

                    x = left;
                    for (int c = 0; c < dt.Columns.Count; c++)
                    {
                        Rectangle rect = new Rectangle(x, y, _colWidthsPx[c], rowHeight);
                        e.Graphics.DrawRectangle(gridPen, rect);

                        string text = dt.Rows[_printRowIndex][c] == System.DBNull.Value
                                      ? "" : System.Convert.ToString(dt.Rows[_printRowIndex][c]);

                        RectangleF textRect = new RectangleF(rect.X + 4, rect.Y + 3, rect.Width - 8, rect.Height - 6);
                        e.Graphics.DrawString(text ?? "", cellFont, brush, textRect);

                        x += rect.Width;
                    }

                    _printRowIndex++;
                    y += rowHeight;
                }

                // all rows printed
                e.HasMorePages = false;
                _printRowIndex = 0;
            }

        }
    }
}
