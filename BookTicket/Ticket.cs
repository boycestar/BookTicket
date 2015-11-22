/*************************************************************************************
     * CLR 版本：       4.0.30319.34209
     * 类 名 称：       Ticket
     * 命名空间：       BookTicket
     * 创建时间：       11/22/2015 7:05:21 PM
     * 作    者：       boyce
     * 邮    箱:        yangbo@cfpamf.org.cn 
     * 说    明：       
     * 修改时间：
     * 修 改 人：
*************************************************************************************/

using System;

namespace BookTicket
{
    /// <summary>
    /// 站点车票出售情况
    /// </summary>
    public class Ticket
    {
        /// <summary>
        /// 虚拟主键
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// 车站站名
        /// </summary>
        public string StationName { get; set; }

        /// <summary>
        /// 站点序号，数值小代表站点靠前
        /// </summary>
        public int Sort { get; set; }

        /// <summary>
        /// 该站点作为起始站点，售出票的数量
        /// </summary>
        public int OriginationCount { get; set; }

        /// <summary>
        /// 该站点作为途径站点，售出票的数量
        /// </summary>
        public int WayStationCount { get; set; }

        /// <summary>
        /// 该站点作为终点站点，售出票的数量
        /// </summary>
        public int DestinationCount { get; set; }

        /// <summary>
        /// 该站点作为终点站点，售出票的数量
        /// </summary>
        public int Total { get; set; }
    }
}