using System;
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
			ChatAnalyser = new Analyser(@"");
			
			ChatAnalyser.Init();

			Console.WriteLine("Reading - " + ChatAnalyser.ChatName);
			Console.WriteLine(ChatAnalyser.ChatLog.messages.Count + " total messages between " + ChatAnalyser.StartDate + " and " + ChatAnalyser.EndDate);
			Console.WriteLine();
			Console.WriteLine("Members:");

			foreach(Participant member in ChatAnalyser.ChatLog.participants)
			{
				Console.WriteLine(member + ":");
				Console.WriteLine("    - Total messages sent: " + member.TotalMessagesSent);
				Console.WriteLine("    - Total messages unsent: " + member.TotalMessagesUnsent);
				Console.WriteLine("    - Total reactions received: " + member.TotalReactionsRecieved);
				Console.WriteLine("    - Total laugh reactions received: " + member.TotalLaughReactionsRecieved);
			}

			Console.WriteLine();
		}
	}
}
