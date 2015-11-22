using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using log4net;

namespace BookTicket
{
    public class Program
    {
        private static readonly object Sync = new object();
        public static readonly List<Ticket> StationList = new List<Ticket>();
        public static readonly List<string> Users = new List<string>();
        private static readonly ILog Log = LogManager.GetLogger(typeof (Program));
        private static readonly Config Config = new Config();

        public static void Main(string[] args)
        {
            InitStation();
            Log.Info("读取配置完毕。。");
            foreach (var user in Users)
            {
                new Thread(e => { UserBuyTicket(user); }).Start();
            }
        }

        /// <summary>
        ///     模拟用户购票
        /// </summary>
        /// <param name="userName">用户名</param>
        private static void UserBuyTicket(string userName)
        {
            // 每人可购票数量
            var count = Config.UserBuyCount;
            while (count > 0)
            {
                // 获取站点名称
                string originationName;
                string destinationName;
                var buyTicketCount = new Random().Next(Config.EachBuyMin, Config.EachBuyMax);
                GetStationName(out originationName, out destinationName);

                count--;

                try
                {
                    var queryTicket = QueryTicket(originationName, destinationName);
                    if (queryTicket <= buyTicketCount)
                    {
                        Console.WriteLine("用户[{0}],购买[{1}]张,从[{2}]->到[{3}]的车票时,因余票不足，无法购买", userName, buyTicketCount,
                            originationName, destinationName);
                        Log.WarnFormat("用户[{0}],购买[{1}]张,从[{2}]->到[{3}]的车票时,因余票不足，无法购买", userName, buyTicketCount,
                            originationName, destinationName);
                    }
                    if (BuyTickets(originationName, destinationName, buyTicketCount))
                    {
                        Console.WriteLine("用户[{0}],购买[{1}]张,从[{2}]->到[{3}]的车票！", userName, buyTicketCount,
                            originationName, destinationName);
                        Log.InfoFormat("用户[{0}],购买[{1}]张,从[{2}]->到[{3}]的车票！", userName, buyTicketCount,
                            originationName, destinationName);
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine("用户[{0}],购买[{1}]张,从[{2}]->到[{3}]的车票时发生异常，具体信息如下：{4}", userName, buyTicketCount,
                        originationName, destinationName, ex.Message);
                    Log.ErrorFormat("用户[{0}],购买[{1}]张,从[{2}]->到[{3}]的车票时发生异常，具体信息如下：{4}", userName, buyTicketCount,
                        originationName, destinationName, ex.Message);
                    Log.Error(ex.Message, ex);
                }
            }
        }

        /// <summary>
        /// 获取任意两个站点
        /// </summary>
        /// <param name="originationName">起始站名</param>
        /// <param name="destinationName">终点站名</param>
        private static void GetStationName(out string originationName, out string destinationName)
        {
            var random = new Random();
            var station1 = random.Next(0, Config.StationCount);
            var station2 = random.Next(0, Config.StationCount);

            // 随机数相等，两个相同站点不能售票，需要重新获取另一个站点的随机数
            if (station1 == station2)
            {
                if (station1 == 0)
                {
                    station2 = random.Next(1, Config.StationCount);
                }

                else if (station2 == Config.StationCount)
                {
                    station1 = random.Next(0, Config.StationCount - 1);
                }
            }

            if (station1 > station2)
            {
                originationName = StationList[station2].StationName;
                destinationName = StationList[station1].StationName;
            }
            else
            {
                originationName = StationList[station1].StationName;
                destinationName = StationList[station2].StationName;
            }
        }

        /// <summary>
        ///     读取配置
        /// </summary>
        private static void InitStation()
        {
            var seatCount = Config.SeatCount;
            for (var i = 0; i < Config.StationCount; i++)
            {
                var stationName = string.Format("S{0}", i);
                StationList.Add(new Ticket {Id = Guid.NewGuid(), StationName = stationName, Sort = i, Total = seatCount});
                Console.WriteLine("初始化站点信息，站点[{0}]", stationName);
            }

            for (var i = 0; i < Config.UserCount; i++)
            {
                Users.Add(string.Format("User{0}", i));
            }
        }

        /// <summary>
        ///     查询任意两个站点之间余票的数量
        /// </summary>
        /// <param name="origination"></param>
        /// <param name="destination"></param>
        /// <returns></returns>
        private static int QueryTicket(string origination, string destination)
        {
            var originationStation = StationList.FirstOrDefault(e => e.StationName.ToLower() == origination.ToLower());
            var destinationStation = StationList.FirstOrDefault(e => e.StationName.ToLower() == destination.ToLower());

            // 1. 判断始发站是否存在
            // 2. 判断终点站是否存在
            if (originationStation == null || destinationStation == null)
                return 0;

            // 判断车站顺序选择是否正确
            if (originationStation.Sort > destinationStation.Sort)
                throw new QueryTicketException(string.Format("始发站[{0}],终点站[{1}],顺序选择错误,请调换始发站和终点站，或重新选择！",
                    originationStation.StationName,
                    destinationStation.StationName));

            var seatCount = originationStation.Total;

            // 查找站点的序号，范围:从始发站终点站之间经过的站点
            var results = StationList.Where(e => e.Sort >= originationStation.Sort && e.Sort <= destinationStation.Sort);

            // 途径站点中售出的票数量是否已经达到票数量的上限
            // 1. 判断票数量是否充足（所有卖出的起点为该站点的票数 + 所有卖出的途径该站的票数）
            var maxCount1 = results.Max(e => Util.Sum(new[] {e.OriginationCount, e.WayStationCount}));
            if (maxCount1 >= seatCount)
                return 0;

            // 2. 判断票数量是否充足（所有卖出的途径该站点的票数 + 所有卖出的终点为该站的票数）
            var maxCount2 = results.Max(e => Util.Sum(new[] {e.WayStationCount, e.DestinationCount}));
            if (maxCount2 >= seatCount)
                return 0;

            var result1 = seatCount - maxCount1;
            var result2 = seatCount - maxCount2;
            return result1 < result2 ? result1 : result2;
        }

        /// <summary>
        ///     购买任意两个站点之间的票
        /// </summary>
        /// <param name="origination">起始站</param>
        /// <param name="destination">终点站</param>
        /// <param name="count">购票数量</param>
        /// <returns>购买是否成功</returns>
        private static bool BuyTickets(string origination, string destination, int count)
        {
            lock (Sync)
            {
                // 是否满足票条件
                var queryTicket = QueryTicket(origination, destination);
                if (queryTicket < count)
                    return false;

                // 获取起始、终止站点信息
                var originationStation =
                    StationList.FirstOrDefault(e => e.StationName.ToLower() == origination.ToLower());
                var destinationStation =
                    StationList.FirstOrDefault(e => e.StationName.ToLower() == destination.ToLower());

                // 获取起始站、终点站、以及中间经过的站点信息
                var results =
                    StationList.Where(e => e.Sort >= originationStation.Sort && e.Sort <= destinationStation.Sort)
                        .OrderBy(e => e.Sort).ToArray();

                // 更新站点出票量
                var resultCount = results.Count();
                for (var i = 0; i < resultCount; i++)
                {
                    if (i == 0) // 起始站时，更新站起始站出票量
                    {
                        results[i].OriginationCount += count;
                        Console.WriteLine("");
                    }
                    else if (i == resultCount - 1) // 途径站时，更新站点途径站出票量
                    {
                        results[i].DestinationCount += count;
                    }
                    else // 终点站时，更新站点站点站出票量
                    {
                        results[i].WayStationCount += count;
                    }
                }

                return true;
            }
        }
    }
}