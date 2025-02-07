using QuestMapperLib.Exceptions;
using System.Collections.Generic;

namespace QuestMapperLib.Model
{
    public class Quest : IQuest
    {
        public bool IsConnectedWithStart { get; set; }
        public int Id { get; set; }
        private string _name;
        public string Name
        {
            get { return _name; }
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                {
                    throw new ValueIsNullOrEmptyException(Id, typeof(Quest).Name, "Name");
                }
                _name = value;
            }
        }
        private string _description;
        public string Description
        {
            get { return _description; }
            set
            {
                if (value == null)
                {
                    throw new ValueIsNullException(Id, typeof(Quest).Name, "Description");
                }
                _description = value;
            }
        }
        private string _reward;
        public string Reward
        {
            get { return _reward; }
            set
            {
                if (value == null)
                {
                    throw new ValueIsNullException(Id, typeof(NPC).Name, "Reward");
                }
                _reward = value;
            }
        }
        public List<int> PreQuestIds { get; set; } = new List<int>();

        public Quest(int id)
        {
            Id = id;
            _name = "Questname";
            _description = string.Empty;
            _reward = string.Empty;
            IsConnectedWithStart = false;
        }

        public virtual bool IsNull => false;
    }
}
