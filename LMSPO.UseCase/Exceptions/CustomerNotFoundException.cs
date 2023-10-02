namespace LMSPO.UseCase.Exceptions
{
    public class CustomerNotFoundException : CustomException
    {
        public CustomerNotFoundException() { }
        public CustomerNotFoundException(string message) : base(message) { }
        public CustomerNotFoundException(string message, Exception innerException) : base(message, innerException) { }
    }
}
