using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Printing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PoolTable
{
    public partial class Invoice : Form
    {
        public string TableName { get; set; }
        public string TimeConsumed { get; set; }
        public int TotalSecond { get; set; }
        public decimal HourlyRate { get; set; }
        public decimal totalFees { get; set; }

        public Invoice(string TableName, string TimeConsumed, int TotalSecond, decimal HourlyRate, decimal totalFees)
        {
            InitializeComponent();

            this.TableName = TableName;
            this.TimeConsumed = TimeConsumed;
            this.TotalSecond = TotalSecond;
            this.HourlyRate = HourlyRate;
            this.totalFees = totalFees;
        }
        private void PrintInvoice()
        {
            PrintDocument doc = new PrintDocument();
            doc.PrintPage += (sender, e) =>
            {
                Font font = new Font("Arial", 12);
                float y = 100;

                e.Graphics.DrawString("pool table billiards invoice", new Font("Arial", 16, FontStyle.Bold), Brushes.Black, 100, y);
                y += 40;
                e.Graphics.DrawString($"Table Name {TableName}", font, Brushes.Black, 100, y);
                y += 30;
                e.Graphics.DrawString($"Time Consumed : {TimeConsumed}", font, Brushes.Black, 100, y);
                y += 30;
                e.Graphics.DrawString($"Hourly rate: {HourlyRate}", font, Brushes.Black, 100, y);
                y += 30;
                e.Graphics.DrawString($"Total Fees: {totalFees}", font, Brushes.Black, 100, y);
            };

            PrintPreviewDialog preview = new PrintPreviewDialog();
            preview.Document = doc;
            preview.ShowDialog();
        }

        private void btnPrint_Click(object sender, EventArgs e) => PrintInvoice();

        private void Form2_Load_1(object sender, EventArgs e)
        {
            lbTableName.Text = TableName.Trim();
            lbHourlyRate.Text = HourlyRate.ToString();
            lbTimeConsumed.Text = TimeConsumed.ToString();
            lbTotalFees.Text = totalFees.ToString("00.00");
            lbDate.Text = DateTime.Now.ToString();
            lbTotalSeconds.Text = TotalSecond.ToString();
        }
    }
}
