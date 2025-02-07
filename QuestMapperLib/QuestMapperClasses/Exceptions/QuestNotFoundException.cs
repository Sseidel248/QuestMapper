using System;

namespace QuestMapperLib.Exceptions
{
    public class QuestNotFoundException : Exception
    {
        public QuestNotFoundException() : base("The specified quest could not be found.") { }

        public QuestNotFoundException(int viewId, int questId) : base($"The view with ID {viewId} have not a quest with ID {questId}.") { }
    }
}
