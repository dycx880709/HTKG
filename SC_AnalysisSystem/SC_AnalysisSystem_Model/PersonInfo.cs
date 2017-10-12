using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SC_AnalysisSystem.Model
{
    public class PersonInfo
    {
        public string Id { get; set; }
        /// <summary>
        /// 玩家用户名
        /// </summary>
        public string UserName { get; set; }
        /// <summary>
        /// 别名-绰号-代号
        /// </summary>
        public string OtherName { get; set; }
        /// <summary>
        /// 年龄
        /// </summary>
        public int Age { get; set; }
        /// <summary>
        /// 任职职务
        /// </summary>
        public int Role { get; set; }
        /// <summary>
        /// 所属部门Id
        /// </summary>
        public int ApartmentId { get; set; }
        /// <summary>
        /// 联系方式
        /// </summary>
        public string Telephone { get; set; }
        /// <summary>
        /// 特长
        /// </summary>
        public string Special { get; set; }
        /// <summary>
        /// 玩家头像信息
        /// </summary>
        public string Source { get; set; }
        /// <summary>
        /// 入伍时间
        /// </summary>
        public DateTime EmploymentDate { get; set; }
    }
}
