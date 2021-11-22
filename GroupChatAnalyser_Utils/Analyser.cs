using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Newtonsoft.Json;
using Windows.Storage;

namespace GroupChatAnalyser_Utils
{
	public class Analyser
	{
		private StorageFolder _logsDir;

		private string _chatName;
		public string ChatName { get => _chatName; }

		private ChatLog _chatLog;
		public ChatLog ChatLog { get => _chatLog; }

		private List<ChatLog> _generatedLogs = new List<ChatLog>();

		private DateTime _startDate;
		public DateTime StartDate { get => _startDate; }

		private DateTime _endDate;
		public DateTime EndDate { get => _endDate; }

		public Analyser(StorageFolder logDir)
		{
			_logsDir = logDir;
		}

		public async Task Init()
		{
			await CompileLogs();
			SetValues();
		}

		private async Task CompileLogs()
		{
			IReadOnlyList<StorageFile> logs = await _logsDir.GetFilesAsync();

			foreach (StorageFile path in logs)
			{
				_generatedLogs.Add(JsonConvert.DeserializeObject<ChatLog>(await FileIO.ReadTextAsync(path)));
			}

			_chatLog = new ChatLog();

			_chatLog.title = _generatedLogs[0].title;
			_chatLog.participants = _generatedLogs[0].participants;
			_chatLog.messages = _generatedLogs[0].messages;

			_generatedLogs.Remove(_generatedLogs[0]);

			foreach (ChatLog log in _generatedLogs)
			{
				_chatLog.messages.AddRange(log.messages);
			}

			_generatedLogs.Clear();
		}


		private void SetValues()
		{
			_chatName = _chatLog.title;
			_endDate = DateConverter.TimeStampToDateTime((long)ChatLog.messages[0].timestamp_ms);
			_startDate = DateConverter.TimeStampToDateTime((long)ChatLog.messages.Last().timestamp_ms);

			foreach (Participant member in ChatLog.participants)
			{
				member.TotalMessagesSent = GetTotalMessagesForMember(member.name);
				GetTotalReactionsRecieved(member.name);
				member.TotalMessagesUnsent = GetTotalUnsentMessages(member.name);
			}
		}

		public Participant GetParticipantByName(string name) => ChatLog.participants.Where(p => p.name == name).FirstOrDefault();

		public int GetTotalMessagesForMember(string member) => ChatLog.messages.Where(m => m.sender_name == member).Count();

		public int GetTotalMessagesContainingText(string text) => ChatLog.messages.Where(m => m.content != null && m.content.ToLower().Contains(text.ToLower())).Count();

		public void GetTotalReactionsRecieved(string member)
		{
			Participant participant = GetParticipantByName(member);

			foreach (Message message in ChatLog.messages)
			{
				if (message.sender_name == member)
				{
					if (message.reactions != null)
					{
						foreach (Reaction reacc in message.reactions)
						{
							string emojiKey = EmojiConstants.Emojis.FirstOrDefault(x => x.Value == reacc.reaction).Key;
							if (emojiKey != null)
							{
								if (participant.ReactionsReceived.ContainsKey(emojiKey))
								{
									participant.ReactionsReceived[emojiKey]++;
								}
								else
								{
									participant.ReactionsReceived.Add(emojiKey, 1);
								}
							}

							Participant actor = GetParticipantByName(reacc.actor);
							if(actor != null) actor.TotalReactionsSent++;
						}
					}
				}
			}
		}

		public int GetTotalUnsentMessages(string member) => ChatLog.messages.Where(m => m.sender_name == member && m.is_unsent).Count();
	}
}
