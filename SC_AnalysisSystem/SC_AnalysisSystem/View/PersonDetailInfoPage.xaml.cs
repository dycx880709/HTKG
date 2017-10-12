﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace SC_AnalysisSystem.View
{
    /// <summary>
    /// PersonDetailInfoPage.xaml 的交互逻辑
    /// </summary>
    public partial class PersonDetailInfoPage : Page
    {
        public PersonDetailInfoPage()
        {
            InitializeComponent();
            RegisterMessenger();
        }

        private void RegisterMessenger()
        {
            App.Messenger.Register("PersonDetailInfoPage_NavigateBack", () =>
                {
                    if (NavigationService.CanGoBack)
                        NavigationService.GoBack();
                });
        }
    }
}
