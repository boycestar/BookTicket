/*************************************************************************************
     * CLR 版本：       4.0.30319.34209
     * 类 名 称：       Util
     * 命名空间：       BookTicket
     * 创建时间：       11/22/2015 8:37:06 PM
     * 作    者：       boyce
     * 邮    箱:        yangbo@cfpamf.org.cn 
     * 说    明：       
     * 修改时间：
     * 修 改 人：
*************************************************************************************/

using System.Collections.Generic;
using System.Linq;

namespace BookTicket
{
    public class Util
    {
        /// <summary>
        ///     数字字符串转换为数值，如果转换不成功则将 defaultValue 返回
        /// </summary>
        /// <param name="source">需要转换的字符串</param>
        /// <param name="defaultValue">如果转换失败，则将该值返回</param>
        /// <returns>字符串转换为数值的结果</returns>
        /// 0
        public static int ConvertStringToInt(string source, int defaultValue)
        {
            int result;
            return int.TryParse(source, out result) ? result : defaultValue;
        }

        /// <summary>
        /// 多个加数相加
        /// </summary>
        /// <param name="addends">需要相加的列表</param>
        /// <returns>加数的和</returns>
        public static int Sum(IList<int> addends)
        {
            if (addends == null || !addends.Any())
                return 0;
            return addends.Sum(e => e);
        }
    }
}