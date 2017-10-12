using SC_AnalysisSystem_Common;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SC_AnalysisSystem
{
    public class AppConfig
    {
        public bool SystemValidate { get; private set; }

        public bool FrameworkValidate { get; private set; }

        public bool CheckUpdater { get; private set; }

        public bool IsDebug { get; set; }

        public string HomeUrl { get; set; }
    
        public AppConfig()
        {
            ConfigUtil.Load(this);
        }
    }
}
