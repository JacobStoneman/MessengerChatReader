using System;
using System.Collections.Generic;
using GroupChatAnalyser_API;

namespace GroupChatAnalyser
{
	class Program
	{
		static Analyser ChatAnalyser;

		static void Main(string[] args)
		{
			Console.WriteLine("Running GroupChatReader");
			Console.WriteLine("This may take a while");
			Console.WriteLine();

			Init();
		}

		static void Init()
		{
			ChatAnalyser = new Analyser(@"C:\Users\jacob\Documents\Files\GroupchatAnalyser\GroupChatAnalyser\MessengerChatReader\GroupChatAnalyser\chatlogs\");
			
			ChatAnalyser.Init();

			Console.WriteLine("Reading - " + ChatAnalyser.ChatName);
			Console.WriteLine(ChatAnalyser.ChatLog.messages.Count + " total messages between " + ChatAnalyser.StartDate + " and " + ChatAnalyser.EndDate);
			Console.WriteLine();
			Console.WriteLine("Members:");

			foreach(Participant member in ChatAnalyser.ChatLog.participants)
			{
				Console.WriteLine(member + ":");
				Console.WriteLine("    - Total messages sent: " + member.TotalMessagesSent);

				if (member.TotalMessagesUnsent != 0)
				{
					Console.WriteLine("    - Total messages unsent: " + member.TotalMessagesUnsent);
				}

				foreach (KeyValuePair<string, int> emoji in member.ReactionsReceived)
				{
					Console.WriteLine("    - Total " + emoji.Key + " reactions received: " + emoji.Value);
				}
			}

			Console.WriteLine();
		}
	}
}
