using SC_AnalysisSystem_Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SC_AnalysisSystem.ViewModel
{
    public class GamerChooseViewModel
    {
        public ObservableCollection<Gamer> Gamers { get; set; }

        public GamerChooseViewModel()
        {
            this.Gamers = new ObservableCollection<Gamer>();
            GenerateGamers();
        }

        private void GenerateGamers()
        {
            Gamers.Add(new Gamer() { Delay = 200, PersonName = "zzz", State = GamerState.Online, IpAddress = "102.23.1.13" });
            Gamers.Add(new Gamer() { Delay = 120, PersonName = "aaa", State = GamerState.Gaming, IpAddress = "102.23.1.15" });
            Gamers.Add(new Gamer() { Delay = 30, PersonName = "qqq", State = GamerState.Preparing, IpAddress = "102.23.1.29" });
        }
    }
}
