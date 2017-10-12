using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SC_AnalysisSystem_Core;
using SC_AnalysisSystem.Model;

namespace SC_AnalysisSystem.ViewModel
{
    public class PersonDetailInfoViewModel : ViewModelBase
    {
        #region Property
        private PersonInfo person;

        private string ipConnectMsg;
        public string IpConnectMsg
        {
            get { return ipConnectMsg; }
            set 
            { 
                ipConnectMsg = value;
                RaisePropertyChanged("IpConnectMsg");
            }
        }

        private string ipAddress;
        public string IpAddress
        {
            get { return ipAddress; }
            set 
            { 
                ipAddress = value;
                RaisePropertyChanged("IpAddress");
            }
        }

        private DateTime employmentDate;
        public DateTime EmploymentDate
        {
            get { return employmentDate; }
            set
            {
                employmentDate = value;
                RaisePropertyChanged("EmploymentDate");
            }
        }

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

        private int age;
        public int Age
        {
            get { return age; }
            set
            { 
                age = value;
                RaisePropertyChanged("Age");
            }
        }

        private string role;
        public string Role
        {
            get { return role; }
            set
            {
                role = value;
                RaisePropertyChanged("Role");
            }
        }

        private string source;
        public string Source
        {
            get { return source; }
            set 
            { 
                source = value;
                RaisePropertyChanged("Source");
            }
        }

        private int id;
        public int Id
        {
            get { return id; }
            set 
            { 
                id = value;
                RaisePropertyChanged("Id");
            }
        }

        private string otherName;
        public string OtherName
        {
            get { return otherName; }
            set 
            { 
                otherName = value;
                RaisePropertyChanged("OtherName");
            }
        }

        private string special;
        public string Special
        {
            get { return special; }
            set 
            {
                special = value;
                RaisePropertyChanged("Special");
            }
        }
        #endregion

        #region Command
        public RelayCommand<string> ButtonCommand { get; set; }
        #endregion

        public PersonDetailInfoViewModel()
        {
            this.ButtonCommand = new RelayCommand<string>(CommandOperation);
            RefreshApartmentInfo();
        }

        private void RefreshApartmentInfo()
        {

        }

        private void CommandOperation(string operate)
        {
            switch (operate)
            {
                case "refresh":
                    RefreshApartmentInfo();
                    break;
                case "connect":
                    ConnectClient();
                    break;
                case "confirm":
                    SubmitPersonInfo();
                    break;
                case "cannel":
                    App.Messenger.Notify("PersonDetailInfoPage_NavigateBack");
                    break;
            }
        }

        private void ConnectClient()
        {

        }

        private void SubmitPersonInfo()
        {
            App.Messenger.Notify("TrainManagementViewModel_UpdatePerson", person);
            App.Messenger.Notify("PersonDetailInfoPage_NavigateBack");
        }
    }
}
