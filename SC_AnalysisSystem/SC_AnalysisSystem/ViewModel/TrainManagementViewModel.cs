using SC_AnalysisSystem.Model;
using SC_AnalysisSystem_Core;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SC_AnalysisSystem.ViewModel
{
    public class TrainManagementViewModel : ViewModelBase
    {
        public ObservableCollection<SubjectInfo> SubjectInfos { get; set; }
        public ObservableCollection<PersonInfo> PersonInfos { get; set; }

        public RelayCommand<string> ButtonCommand { get; set; }
        public TrainManagementViewModel()
        {
            GenerateBindingDatas();
        }

        private void RegistMessenger()
        {
            App.Messenger.Register<PersonInfo>("TrainManagementViewModel_UpdatePerson", UpdatePersonInfo);
        }

        private void UpdatePersonInfo(PersonInfo person)
        {
            var res = PersonInfos.FirstOrDefault(t => t.Id == person.Id);
            if (res == null)
                PersonInfos.Add(person);
        }

        private void GenerateBindingDatas()
        {
            this.SubjectInfos = new ObservableCollection<SubjectInfo>();
            SubjectInfos.Add(new SubjectInfo());
            SubjectInfos.Add(new SubjectInfo());
            SubjectInfos.Add(new SubjectInfo());
            SubjectInfos.Add(new SubjectInfo());

            this.PersonInfos = new ObservableCollection<PersonInfo>();
            PersonInfos.Add(new PersonInfo());
            PersonInfos.Add(new PersonInfo());
            PersonInfos.Add(new PersonInfo());
        }
    }
}
