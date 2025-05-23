﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PoolTable
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void ShowInvoice(ctrlTable.TableCompletedEventArgs e)
        {
            Invoice form = new Invoice(e.TableName, e.timeText, e.TimeInSeconds, e.RatePerHour, e.TotalCost);
            form.Show();
        }

        private void EndGame_TableCompleted(object sender, ctrlTable.TableCompletedEventArgs e) => ShowInvoice(e);
    }
}
