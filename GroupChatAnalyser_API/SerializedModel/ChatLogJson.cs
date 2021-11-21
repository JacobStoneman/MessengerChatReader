using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GroupChatAnalyser_API
{
    //TODO: reformat names and getters
    public class Participant
    {
        public string name { get; set; }

        public int TotalMessagesSent { get; set; }
        public int TotalMessagesUnsent { get; set; }

        public Dictionary<string, int> ReactionsReceived = new Dictionary<string, int>();


        public int TotalReactionsRecieved { get; set; }
        public int TotalLaughReactionsRecieved { get; set; }
        

        public override string ToString() => name;
	}

    public class Photo
    {
        public string uri { get; set; }
    }

    public class Reaction
    {
        public string reaction { get; set; }
        public string actor { get; set; }

        public string Emoji
        {
            //TODO: EmojiConstants.get(reaction); -- get formatted emoji name from list of defined constants
            get => reaction;
        }
    }

    public class Message
    {
        public string sender_name { get; set; }
        public object timestamp_ms { get; set; }
        public List<Photo> photos { get; set; }
        public List<Reaction> reactions { get; set; }
        public bool is_unsent { get; set; }
        public string content { get; set; }

        public int GetTotalReactionsByEmoji(string emoji)
        {
            int count = 0;
            foreach (Reaction reacc in reactions)
            {
                if (reacc.reaction == emoji)
                {
                    count++;
                }
            }
            return count;
        }
    }

    public class ChatLog
    {
        public List<Participant> participants { get; set; }
        public List<Message> messages { get; set; }
        public string title { get; set; }
    }
}
