using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuestMapperLib.Model
{
    public class NullNPC : INPC
    {
        private static readonly NullNPC _instance = new NullNPC();

        private NullNPC()
        {
            Id = -1;
            Name = "Unknown";
            Description = "This NPC does not exist.";
        }

        public int Id { get; }
        public string Name { get; set; }
        public string Description { get; set; }
        public bool IsNull => true;

        public static INPC Instance => _instance;
    }
}
