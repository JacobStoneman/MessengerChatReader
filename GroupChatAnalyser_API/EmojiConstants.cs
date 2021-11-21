using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GroupChatAnalyser_API
{
	public static class EmojiConstants
	{
		public static readonly Dictionary<string, string> Emojis = new Dictionary<string, string>()
		{
			{ "LAUGH", "\u00f0\u009f\u0098\u0086" },
			{ "HEART", "\u00e2\u009d\u00a4" },
			{ "THUMBS_UP", "\u00f0\u009f\u0091\u008d" },
			{ "CRYING", "\u00f0\u009f\u0098\u00a2" },
			{ "FOOT", "\u00f0\u009f\u00a6\u00b6" },
			{ "OK", "\u00f0\u009f\u0091\u008c" },
			{ "ANGRY", "\u00f0\u009f\u0098\u00a0" },
			{ "SHOCK", "\u00f0\u009f\u0098\u00ae" }


			//{"\u00f0\u009f\u0098\u0086", "Laugh" },
			//{"\u00e2\u009d\u00a4", "Heart" },
			//{ "\u00f0\u009f\u0091\u008d","Thumbs up" },
			//{ "\u00f0\u009f\u0098\u00a2", "Crying" },
			//{ "\u00f0\u009f\u00a6\u00b6", "Foot" },
			//{ "\u00f0\u009f\u0091\u008c", "Ok" },
			//{ "\u00f0\u009f\u0098\u00a0", "Angry" },
			//{ "\u00f0\u009f\u0098\u00ae", "Shock" }
		};
	}
}
