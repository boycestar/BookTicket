/*************************************************************************************
     * CLR 版本：       4.0.30319.34209
     * 类 名 称：       Config
     * 命名空间：       BookTicket
     * 创建时间：       11/22/2015 8:31:24 PM
     * 作    者：       boyce
     * 邮    箱:        yangbo@cfpamf.org.cn 
     * 说    明：       
     * 修改时间：
     * 修 改 人：
*************************************************************************************/

using ServiceStack.Configuration;

namespace BookTicket
{
    public class Config
    {
        public Config()
        {
            AppSettings = new AppSettings();
        }

        private AppSettings AppSettings { get; set; }

        /// <summary>
        /// 站点数量
        /// </summary>
        public int StationCount
        {
            get { return Util.ConvertStringToInt(AppSettings.GetString("StationCount"), 10); }
        }

        /// <summary>
        /// 座位数量
        /// </summary>
        public int SeatCount
        {
            get { return Util.ConvertStringToInt(AppSettings.GetString("SeatCount"), 10); }
        }

        /// <summary>
        /// 购票人数
        /// </summary>
        public int UserCount
        {
            get { return Util.ConvertStringToInt(AppSettings.GetString("UserCount"), 10); }
        }

        /// <summary>
        /// 每人可购买次数
        /// </summary>
        public int UserBuyCount
        {
            get { return Util.ConvertStringToInt(AppSettings.GetString("UserBuyCount"), 10); }
        }

        /// <summary>
        /// 每人每次最少可购票量
        /// </summary>
        public int EachBuyMin
        {
            get { return Util.ConvertStringToInt(AppSettings.GetString("EachBuyMin"), 10); }
        }

        /// <summary>
        /// 每人每次最多可购票量
        /// </summary>
        public int EachBuyMax
        {
            get { return Util.ConvertStringToInt(AppSettings.GetString("EachBuyMax"), 10); }
        }
    }
}