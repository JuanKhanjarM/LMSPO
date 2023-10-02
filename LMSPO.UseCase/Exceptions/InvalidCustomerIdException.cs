namespace LMSPO.UseCase.Exceptions
{
    public class InvalidCustomerIdException : CustomException
    {
        public InvalidCustomerIdException() { }
        public InvalidCustomerIdException(string message) : base(message) { }
        public InvalidCustomerIdException(string message, Exception innerException) : base(message, innerException) { }
    }
}
