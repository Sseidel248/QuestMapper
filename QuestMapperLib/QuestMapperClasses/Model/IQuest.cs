using System.Collections.Generic;

namespace QuestMapperLib.Model
{
    public interface IQuest
    {
        int Id { get; }
        string Name { get; set; }
        string Description { get; set; }
        string Reward { get; set; }
        bool IsConnectedWithStart { get; set; }
        List<int> PreQuestIds { get; }
        bool IsNull { get; }
    }
}
