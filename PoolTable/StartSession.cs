using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PoolTable
{
    public partial class StartSession : Form
    {
        public string PlayerName { get; set; }
        public string TableName { get; set; }
        public decimal HourlyRate { get; set; }
        public StartSession(string playerName, string tableName, decimal hourlyRate)
        {
            InitializeComponent();

            PlayerName = playerName;
            TableName = tableName;
            HourlyRate = hourlyRate;
        }

        private bool _ErrorCheck()
        {
            errorProvider1.Clear();

            if (string.IsNullOrWhiteSpace(textBoxPlayerName.Text))
            {
                errorProvider1.SetError(textBoxPlayerName, "Please enter a player name.");
                return false;
            }

            if (string.IsNullOrWhiteSpace(textBoxTableName.Text))
            {
                errorProvider1.SetError(textBoxTableName, "Please enter a table name.");
                return false;
            }

            if (!decimal.TryParse(textBoxHourlyRate.Text, out decimal hourlyRate) || hourlyRate <= 0)
            {
                errorProvider1.SetError(textBoxHourlyRate, "Please enter a valid hourly rate.");
                return false;
            }

            return true;
        }

        private void StartSession_Load(object sender, EventArgs e)
        {
            this.AcceptButton = btnStart;
            this.CancelButton = btnCancel;

            textBoxPlayerName.Text = PlayerName;
            textBoxTableName.Text = TableName;
            textBoxHourlyRate.Text = HourlyRate.ToString();
        }

        private void btnStart_Click(object sender, EventArgs e)
        {
            if (_ErrorCheck())
            {
                PlayerName = textBoxPlayerName.Text;
                TableName = textBoxTableName.Text;
                HourlyRate = decimal.TryParse(textBoxHourlyRate.Text, out var rate) ? rate : 0;

                this.DialogResult = DialogResult.OK;
                this.Close();
            }
        }

        private void btnCancel_Click(object sender, EventArgs e) => this.Close();
    }
}
