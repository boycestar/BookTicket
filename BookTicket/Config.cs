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
        private int _eachBuyMax = -1;
        private int _eachBuyMin = -1;
        private int _seatCount = -1;
        private int _stationCount = -1;
        private int _userBuyCount = -1;
        private int _userCount = -1;

        public Config()
        {
            AppSettings = new AppSettings();
        }

        private AppSettings AppSettings { get; set; }

        /// <summary>
        ///     站点数量
        /// </summary>
        public int StationCount
        {
            get
            {
                _stationCount = _stationCount == -1
                    ? Util.ConvertStringToInt(AppSettings.GetString("StationCount"), 10)
                    : _stationCount;
                return _stationCount;
            }
        }

        /// <summary>
        ///     座位数量
        /// </summary>
        public int SeatCount
        {
            get
            {
                _seatCount = _seatCount == -1
                    ? Util.ConvertStringToInt(AppSettings.GetString("SeatCount"), 10)
                    : _seatCount;
                return _seatCount;
            }
        }

        /// <summary>
        ///     购票人数
        /// </summary>
        public int UserCount
        {
            get
            {
                _userCount = _userCount == -1
                    ? Util.ConvertStringToInt(AppSettings.GetString("UserCount"), 10)
                    : _userCount;
                return _userCount;
            }
        }

        /// <summary>
        ///     每人可购买次数
        /// </summary>
        public int UserBuyCount
        {
            get
            {
                _userBuyCount = _userBuyCount == -1
                    ? Util.ConvertStringToInt(AppSettings.GetString("UserBuyCount"), 10)
                    : _userBuyCount;
                return _userBuyCount;
            }
        }

        /// <summary>
        ///     每人每次最少可购票量
        /// </summary>
        public int EachBuyMin
        {
            get
            {
                _eachBuyMin = _eachBuyMin == -1
                    ? Util.ConvertStringToInt(AppSettings.GetString("EachBuyMin"), 10)
                    : _eachBuyMin;
                return _eachBuyMin;
            }
        }

        /// <summary>
        ///     每人每次最多可购票量
        /// </summary>
        public int EachBuyMax
        {
            get
            {
                _eachBuyMax = _eachBuyMax == -1
                    ? Util.ConvertStringToInt(AppSettings.GetString("EachBuyMax"), 10)
                    : _eachBuyMax;
                return _eachBuyMax;
            }
        }
    }
}