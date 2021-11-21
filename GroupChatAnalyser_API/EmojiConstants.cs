using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GroupChatAnalyser_API
{
	public static class EmojiConstants
	{
		public const string LAUGH = "\u00f0\u009f\u0098\u0086";
		public const string HEART = "\u00e2\u009d\u00a4";
		public const string THUMBS_UP = "\u00f0\u009f\u0091\u008d";
		public const string CRYING = "\u00f0\u009f\u0098\u00a2";
		public const string FOOT = "\u00f0\u009f\u00a6\u00b6";
		public const string OK = "\u00f0\u009f\u0091\u008c";
		public const string ANGRY = "\u00f0\u009f\u0098\u00a0";
		public const string SHOCK = "\u00f0\u009f\u0098\u00ae";

		//TODO: Replace this with a dictionary
		public static string GetEmoji(string emoji)
		{
			switch (emoji)
			{
				case LAUGH:
					return "Laugh";
				case HEART:
					return "Heart";
				case THUMBS_UP:
					return "Thumbs up";
				case CRYING:
					return "Crying";
				case FOOT:
					return "Foot";
				case OK:
					return "Ok";
				case ANGRY:
					return "Angry";
				case SHOCK:
					return "Shock";
				default:
					return "Emoji not found - check https://apps.timwhitlock.info/emoji/tables/unicode";
			}
		}

	}
}
