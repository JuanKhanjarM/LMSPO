namespace LMSPO.SqlServer.Exceptions
{
    public class CustomException : Exception
    {
        public CustomException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }
}
