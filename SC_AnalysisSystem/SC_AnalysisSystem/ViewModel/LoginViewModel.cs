using SC_AnalysisSystem_Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SC_AnalysisSystem.ViewModel
{
    public class LoginViewModel : ViewModelBase
    {
        #region Property
        private string userName;
        public string UserName
        {
            get { return userName; }
            set 
            {
                userName = value;
                RaisePropertyChanged("UserName");
            }
        }

        private string password;
        public string Password
        {
            get { return password; }
            set
            { 
                password = value;
                RaisePropertyChanged("Password");
            }
        }

        private string loginInfo;
        public string LoginInfo
        {
            get { return loginInfo; }
            set 
            {
                loginInfo = value;
                RaisePropertyChanged("LoginInfo");
            }
        }
        #endregion

        #region Command
        public RelayCommand LoginCommand { get; set; }
        #endregion

        public LoginViewModel()
        {
            this.LoginCommand = new RelayCommand(Login);
        }

        private void Login()
        {

        }
    }
}
