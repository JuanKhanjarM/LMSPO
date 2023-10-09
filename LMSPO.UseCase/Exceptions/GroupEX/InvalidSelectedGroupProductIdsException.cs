namespace LMSPO.UseCase.Exceptions.GroupEX
{
    public class InvalidSelectedGroupProductIdsException: Exception
    {
        public InvalidSelectedGroupProductIdsException()
        {
        }

        public InvalidSelectedGroupProductIdsException(string message)
            : base(message)
        {
        }

        public InvalidSelectedGroupProductIdsException(string message, Exception innerException)
            : base(message, innerException)
        {
        }
    }
}
