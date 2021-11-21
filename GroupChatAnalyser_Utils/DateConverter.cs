using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GroupChatAnalyser_Utils
{
	public static class DateConverter
	{
		public static DateTime TimeStampToDateTime(long unixTimeStamp)
		{
			DateTime dateTime = new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc);
			dateTime = dateTime.AddMilliseconds(unixTimeStamp).ToLocalTime();
			return dateTime;
		}
	}
}
