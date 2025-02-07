using System;

namespace QuestMapperLib.Exceptions
{
    public class ValueIsNullException : Exception
    {
        public ValueIsNullException() : base("The object is null.") { }

        public ValueIsNullException(int objId, string className, string propertyName) :
            base($"{className} - The object with ID {objId} is null (property: {propertyName}).")
        { }
    }
}
