using System;

namespace QuestMapperLib.Exceptions
{
    public class ViewNotFoundException : Exception
    {
        public ViewNotFoundException() : base("The specified view could not be found.") { }

        public ViewNotFoundException(int viewId) : base($"The view with ID {viewId} could not be found.") { }
    }
}
