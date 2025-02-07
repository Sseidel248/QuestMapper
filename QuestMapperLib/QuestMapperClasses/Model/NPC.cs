using QuestMapperLib.Exceptions;

namespace QuestMapperLib.Model
{
    public class NPC : INPC
    {
        public int Id { get; set; }
        private string _name;
        public string Name
        {
            get { return _name; }
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                {
                    throw new ValueIsNullOrEmptyException(Id, typeof(NPC).Name, "Name");
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
                    throw new ValueIsNullException(Id, typeof(NPC).Name, "Description");
                }
                _description = value;
            }
        }

        public NPC(int id)
        {
            Id = id;
            _name = string.Empty;
            _description = string.Empty;
        }
        public virtual bool IsNull => false;
    }
}
