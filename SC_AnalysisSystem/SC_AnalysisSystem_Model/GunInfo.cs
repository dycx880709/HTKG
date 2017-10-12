using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SC_AnalysisSystem_Model
{
    public enum GunType { Handgun, MachineGun }

    public class GunInfo
    {
        public string Id { get; set; }
        /// <summary>
        /// 手枪类型
        /// </summary>
        public GunType Type { get; set; }
        /// <summary>
        /// 装弹量
        /// </summary>
        public int Capacity { get; set; }
        /// <summary>
        /// 射程
        /// </summary>
        public int Range { get; set; }
        /// <summary>
        /// 子弹射击初速度
        /// </summary>
        public int BulletSpeed { get; set; }
        /// <summary>
        /// 弹夹数量
        /// </summary>
        public int ClipCount { get; set; }
        /// <summary>
        /// 阻力系数
        /// </summary>
        public double DragForce { get; set; }
        /// <summary>
        /// 射击威力
        /// </summary>
        public double Power { get; set; }
    }
}
