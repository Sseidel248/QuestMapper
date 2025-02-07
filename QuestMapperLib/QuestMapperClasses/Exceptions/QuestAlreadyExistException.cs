using System;

namespace QuestMapperLib.Exceptions
{
    public class QuestAlreadyExistException : Exception
    {
        public QuestAlreadyExistException() : base("The specified quest already exist.") { }

        public QuestAlreadyExistException(int viewId, int questId) : base($"The view with ID {viewId} already have a quest with ID {questId}.") { }
    }
}
