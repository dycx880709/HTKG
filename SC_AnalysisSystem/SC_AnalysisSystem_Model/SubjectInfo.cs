using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace SC_AnalysisSystem.Model
{
    public class SubjectInfo
    {
        /// <summary>
        /// 场景ID编号
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// 缩略图地址
        /// </summary>
        public string ImageSource { get; set; }
        /// <summary>
        /// 场景动态图
        /// </summary>
        public string GifSource { get; set; }
        /// <summary>
        /// 场景名称
        /// </summary>
        public string SubjectName { get; set; }
        /// <summary>
        /// 场景描述
        /// </summary>
        public string Description { get; set; }
        /// <summary>
        /// 场景难度，由场景环境决定
        /// </summary>
        public double HardScore { get; set; }
        /// <summary>
        /// 场景地图尺寸
        /// </summary>
        public Size SubjectSize { get; set; }
    }
}
