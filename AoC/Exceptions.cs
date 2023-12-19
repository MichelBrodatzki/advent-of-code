namespace AoC
{
    public class InputFileInvalidException : Exception {
        public InputFileInvalidException()
        {
        }
        
        public InputFileInvalidException(string message) 
            : base(message)
        {
        }

        public InputFileInvalidException(string message, Exception inner)
            : base(message, inner)
        {
        }
    }
}