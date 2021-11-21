using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Newtonsoft.Json;

namespace GroupChatAnalyser_API
{
	public class Analyser
	{
		string _filePath;

		string _logDirectory;
		string[] _filePaths;

		string _chatName;
		public string ChatName { get => _chatName; }

		private ChatLog _chatLog;
		public ChatLog ChatLog { get => _chatLog; }

		List<ChatLog> _logs = new List<ChatLog>();

		private DateTime _startDate;
		public DateTime StartDate { get => _startDate; }

		private DateTime _endDate;
		public DateTime EndDate { get => _endDate; }

		public Analyser(string filePath)
		{
			_logDirectory = filePath;
		}

		public void Init()
		{
			GetLogs();
			CompileLogs();
			SetValues();
		}

		private void GetLogs() => _filePaths = Directory.GetFiles(_logDirectory);

		private void CompileLogs()
		{
			foreach(string path in _filePaths)
			{
				_logs.Add(JsonConvert.DeserializeObject<ChatLog>(File.ReadAllText(path)));
			}

			_chatLog = new ChatLog();

			_chatLog.title = _logs[0].title;
			_chatLog.participants = _logs[0].participants;
			_chatLog.messages = _logs[0].messages;

			_logs.Remove(_logs[0]);

			foreach (ChatLog log in _logs)
			{
				_chatLog.messages.AddRange(log.messages);
			}

			_logs.Clear();
		}


		private void SetValues()
		{
			_chatName = _chatLog.title;
			_endDate = DateConverter.TimeStampToDateTime((long)ChatLog.messages[0].timestamp_ms);
			_startDate = DateConverter.TimeStampToDateTime((long)ChatLog.messages.Last().timestamp_ms);

			foreach (Participant member in ChatLog.participants)
			{
				member.TotalMessagesSent = GetTotalMessagesForMember(member.name);
				member.TotalReactionsRecieved = GetTotalReactionsRecieved(member.name);
				member.TotalLaughReactionsRecieved = GetTotalReactionsRecieved(member.name, EmojiConstants.LAUGH);
				member.TotalMessagesUnsent = GetTotalUnsentMessages(member.name);
			}
		}

		private Participant GetParticipantByName(string name) => ChatLog.participants.Where(p => p.name == name).FirstOrDefault();

		public int GetTotalMessagesForMember(string member) => ChatLog.messages.Where(m => m.sender_name == member).Count();

		public int GetTotalMessagesContainingText(string text) => ChatLog.messages.Where(m => m.content != null && m.content.ToLower().Contains(text.ToLower())).Count();

		public int GetTotalReactionsRecieved(string member)
		{
			int count = 0;

			foreach(Message message in ChatLog.messages)
			{
				if(message.sender_name == member)
				{
					if(message.reactions != null)
					{
						count += message.reactions.Count;
					}
				}
			}

			return count;
		}

		public int GetTotalReactionsRecieved(string member, string emoji)
		{
			int count = 0;

			foreach (Message message in ChatLog.messages)
			{
				if (message.sender_name == member)
				{
					if (message.reactions != null)
					{
						foreach(Reaction reacc in message.reactions)
						{
							if(reacc.reaction == emoji)
							{
								count++;
							}
						}
					}
				}
			}

			return count;
		}

		public int GetTotalUnsentMessages(string member) => ChatLog.messages.Where(m => m.sender_name == member && m.is_unsent).Count();
	}
}
