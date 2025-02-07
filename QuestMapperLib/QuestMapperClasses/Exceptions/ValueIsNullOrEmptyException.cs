using System;

namespace QuestMapperLib.Exceptions
{
    public class ValueIsNullOrEmptyException : Exception
    {
        public ValueIsNullOrEmptyException() : base("The object is null or Empty.") { }

        public ValueIsNullOrEmptyException(int objId, string className, string propertyName) :
            base($"{className} - The object with ID {objId} is null or Empty  (property: {propertyName}).")
        { }
    }
}
