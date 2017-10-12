﻿using System;
using System.Diagnostics;
using System.IO;
using System.Windows;

namespace Ezhu.AutoUpdater.UI
{
    /// <summary>
    /// Interaction logic for DownFileProcess.xaml
    /// </summary>
    public partial class AlertWin : WindowBase
    {
        public bool YesBtnSelected = false;
        public AlertWin(string msg)
        {
            InitializeComponent();

            Loaded += (sl, el) =>
            {
                YesButton.Content = "是";
                NoButton.Content = "否";
                txtMsg.Text = msg;
                YesButton.Click += (sender, e) =>
                {
                    YesBtnSelected = true;
                    Close();
                };

                NoButton.Click += (sender, e) =>
                {
                    YesBtnSelected = false;
                    Close();
                };
            };
        }
    }
}
