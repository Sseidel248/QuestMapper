using System;

namespace QuestMapperLib.Exceptions
{
    public class ValueToAddIsNullException : Exception
    {
        public ValueToAddIsNullException() : base("The object is null.") { }

        public ValueToAddIsNullException(string className) :
            base($"{className} - The object is null.")
        { }
    }
}
