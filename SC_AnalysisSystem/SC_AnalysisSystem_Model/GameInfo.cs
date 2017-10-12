using SC_AnalysisSystem_Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SC_AnalysisSystem.Model
{
    public enum GameState { Creating = 0, Initializing = 1, Gaming = 2, Analysising = 3, Done = 4 }
    public enum GameLevel {  Easy, Normal, Crazy }
    public enum GameType { Visual, Combat }

    public class GameInfo
    {
        /// <summary>
        /// 游戏ID
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// 训练场景ID
        /// </summary>
        public int SubjectID { get; set; }
        /// <summary>
        /// 训练场景名称-冗余
        /// </summary>
        public string SubjectName { get; set; }
        /// <summary>
        /// 游戏名称
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// 游戏描述
        /// </summary>
        public string Description { get; set; }
        /// <summary>
        /// 游戏开始时间
        /// </summary>
        public DateTime StartTime { get; set; }
        /// <summary>
        /// 游戏结束时间
        /// </summary>
        public DateTime EndTime { get; set; }
        /// <summary>
        /// 游戏状态
        /// </summary>
        public GameState State { get; set; }
        /// <summary>
        /// 玩家人数
        /// </summary>
        public int GamerCount { get; set; }
        /// <summary>
        /// 游戏建议用时，以分钟为单位
        /// </summary>
        public int ProposeTime { get; set; }
        /// <summary>
        /// 参与玩家
        /// </summary>
        public List<Gamer> Gamers { get; set; }
        /// <summary>
        /// 游戏难度
        /// </summary>
        public GameLevel HardLevel { get; set; }
        /// <summary>
        /// 游戏类型
        /// </summary>
        public GameType Type { get; set; }

        #region 评估系统不用传值
        /// <summary>
        /// 战术配合
        /// </summary>
        public string TacticsCooperation { get; set; }
        /// <summary>
        /// 战术细节
        /// </summary>
        public string TacticsDetail { get; set; }
        /// <summary>
        /// 战术规划
        /// </summary>
        public string TacticsPlan { get; set; }
        #endregion
    }
}
