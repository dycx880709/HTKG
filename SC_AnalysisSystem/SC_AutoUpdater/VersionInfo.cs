using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

namespace Ezhu.AutoUpdater
{
    [DataContract]
    public class VersionInfo
    {
        [DataMember(IsRequired = true)]
        public string version { get; set; }

        [DataMember(IsRequired = true)]
        public string url { get; set; }

        [DataMember(IsRequired = false)]
        public string requiredMinVersion { get; set; }

        [DataMember(IsRequired = true)]
        public string describe { get; set; }
    }
}
