using System.Collections.Generic;

namespace QuestMapperLib.Model
{
    public class NullQuest : IQuest
    {
        private static readonly NullQuest _instance = new NullQuest();

        private NullQuest()
        {
            Id = -1;
            Name = "No Quest Available";
            Description = "This quest does not exist.";
            Reward = "No Reward";
            IsConnectedWithStart = false;
            PreQuestIds = new List<int>();
        }

        public int Id { get; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Reward { get; set; }
        public bool IsConnectedWithStart { get; set; }
        public List<int> PreQuestIds { get; }
        public bool IsNull => true;

        public static IQuest Instance => _instance;
    }
}
