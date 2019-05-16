namespace pavlovLab.Storage
{
    [System.Serializable]
    public class IncorrectMyModDataException : System.Exception
    {
        public IncorrectMyModDataException() { }
        public IncorrectMyModDataException(string message) : base(message) { }
        public IncorrectMyModDataException(string message, System.Exception inner) : base(message, inner) { }
        protected IncorrectMyModDataException(
            System.Runtime.Serialization.SerializationInfo info,
            System.Runtime.Serialization.StreamingContext context) : base(info, context) { }
    }
}