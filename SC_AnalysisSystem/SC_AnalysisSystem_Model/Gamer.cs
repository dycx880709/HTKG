using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;

namespace SC_AnalysisSystem_Model
{
    public class Gamer
    {
        public string Id { get; set; }
        /// <summary>
        /// 真实人物id
        /// </summary>
        public string PersonId { get; set; }
        /// <summary>
        /// 血量
        /// </summary>
        public double HP { get; set; }
        /// <summary>
        /// 游戏角色
        /// </summary>
        public string Role { get; set; }
        /// <summary>
        /// 方位
        /// </summary>
        public Point Location { get; set; }
        /// <summary>
        /// 枪支Id
        /// </summary>
        public int GunId { get; set; }
        /// <summary>
        /// 玩家Ip地址
        /// </summary>
        public string IpAddress { get; set; }
        /// <summary>
        /// 延迟度
        /// </summary>
        public int Delay { get; set; }
    }   
}
