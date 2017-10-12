using SC_AnalysisSystem_Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SC_AnalysisSystem.ViewModel
{
    public class MainViewModel : ViewModelBase
    {
        #region Command
        public RelayCommand<string> ButtonCommand { get; set; }
        #endregion

        public MainViewModel()
        {
            this.ButtonCommand = new RelayCommand<string>(s => App.Messenger.Notify("MainWindow_Navigate", s));
        }
    }
}
