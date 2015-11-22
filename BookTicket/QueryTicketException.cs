/*************************************************************************************
     * CLR 版本：       4.0.30319.34209
     * 类 名 称：       QueryTicketException
     * 命名空间：       BookTicket
     * 创建时间：       11/22/2015 10:35:50 PM
     * 作    者：       boyce
     * 邮    箱:        yangbo@cfpamf.org.cn 
     * 说    明：       
     * 修改时间：
     * 修 改 人：
*************************************************************************************/

using System;
using System.Runtime.Serialization;

namespace BookTicket
{
    public class QueryTicketException : Exception
    {
        public QueryTicketException()
        {
        }

        public QueryTicketException(string message) : base(message)
        {
        }

        public QueryTicketException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected QueryTicketException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}