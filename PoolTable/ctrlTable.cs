using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Diagnostics.Contracts;
using System.Drawing;
using System.Linq;
using System.Resources;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PoolTable
{
    public partial class ctrlTable : UserControl
    {
        public class TableCompletedEventArgs : EventArgs
        {
            public string timeText { get; }
            public int TimeInSeconds { get; }
            public decimal RatePerHour { get; }
            public decimal TotalCost { get; }
            public string TableName { get; }

            public TableCompletedEventArgs(string TableName, string timeText, int timeInSeconds, decimal ratePerHour, decimal totalCost)
            {
                this.timeText = timeText;
                TimeInSeconds = timeInSeconds;
                RatePerHour = ratePerHour;
                TotalCost = totalCost;
                this.TableName = TableName;
            }
        }

        public event EventHandler<TableCompletedEventArgs> TableCompleted;
        public void RaiseTableCompletedEvent(string TimeText, int TimeInSeconds, decimal RatePerHour, decimal TotalCost) => RaiseTableCompletedEvent(new TableCompletedEventArgs(TableName, TimeText, TimeInSeconds, RatePerHour, TotalCost));
        protected virtual void RaiseTableCompletedEvent(TableCompletedEventArgs e) => TableCompleted?.Invoke(this, e);

        private int _TimeInSeconds = 0;

        private decimal _HourlyRate = 10;
        [
            Category("pool Config"),
            Description("Hourly Rate"),
        ]
        public decimal HourlyRate
        {
            get { return _HourlyRate; }
            set { _HourlyRate = value; }
        }

        private string _TablePlayer = "Table";
        [
            Category("pool Config"),
            Description("Player Name"),
        ]
        public string TableName
        {
            get { return _TablePlayer; }
            set
            {
                _TablePlayer = value;
                lbPlayerName.Text = value;
            }
        }

        private string _TableTitle = "Table";
        [
            Category("pool Config"),
            Description("Table Title"),
        ]
        public string TableTitle
        {
            get { return _TableTitle; }
            set
            {
                _TableTitle = value;
                groupBox1.Text = value;
            }
        }

        public ctrlTable()
        {
            InitializeComponent();
        }

        private void EndGame()
        {

            timer1.Stop();
            pictureBox1.Image = Properties.Resources.Close;
            btnStartStop.Text = "Start";
            RaiseTableCompletedEvent(lbTimer.Text, _TimeInSeconds, _HourlyRate, (_HourlyRate * _TimeInSeconds) / 3600);
            _TimeInSeconds = 0;
        }

        private void buttonStartStop_Click(object sender, EventArgs e)
        {
            if (_TimeInSeconds == 0)
            {
                StartSession startSession = new StartSession(TableName, TableTitle, HourlyRate);
                if (startSession.ShowDialog() == DialogResult.OK)
                {
                    TableName = startSession.PlayerName;
                    HourlyRate = startSession.HourlyRate;
                    TableTitle = startSession.TableName;
                }
                else
                    return;

                lbTimer.Text = TimeSpan.FromSeconds(_TimeInSeconds).ToString(@"hh\:mm\:ss");
            }

            if (btnStartStop.Text == "Start")
            {
                btnStartStop.Text = "Stop";
                pictureBox1.Image = Properties.Resources.Open;
                timer1.Start();
            }
            else
            {
                pictureBox1.Image = Properties.Resources.Close;
                btnStartStop.Text = "Start";
                timer1.Stop();
            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            _TimeInSeconds += 1;
            lbTimer.Text = TimeSpan.FromSeconds(_TimeInSeconds).ToString(@"hh\:mm\:ss");
        }

        private void btnEnd_Click(object sender, EventArgs e)
        {
            if (_TimeInSeconds > 0)
                EndGame();
        }
    }
}
