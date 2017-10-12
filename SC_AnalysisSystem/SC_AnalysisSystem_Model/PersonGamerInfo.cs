using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SC_AnalysisSystem_Model
{
    public class PersoGamerInfo
    {
        public string Id { get; set; }
        /// <summary>
        /// 真实人物信息Id
        /// </summary>
        public string PersonId { get; set; }
        /// <summary>
        /// 玩家综合评分
        /// </summary>
        public double TotalScore { get; set; }
        /// <summary>
        /// X因素评分
        /// </summary>
        public double XScore { get; set; }
        /// <summary>
        /// 战术规则评分
        /// </summary>
        public double RuleScore { get; set; }
        /// <summary>
        /// 战术配合评分
        /// </summary>
        public double CooperateScore { get; set; }
        /// <summary>
        /// 战术细节评分
        /// </summary>
        public double DetailScore { get; set; }
        /// <summary>
        /// 完成场数
        /// </summary>
        public int CompleteCount { get; set; }
        /// <summary>
        /// 任务通过
        /// </summary>
        public int Success { get; set; }
        /// <summary>
        /// 任务失败
        /// </summary>
        public int Fail { get; set; }
    }
}
