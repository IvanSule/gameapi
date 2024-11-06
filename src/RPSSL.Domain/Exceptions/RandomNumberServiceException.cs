namespace RPSSL.Domain.Exceptions
{
    public sealed class RandomNumberServiceException : Exception
    {
        public RandomNumberServiceException(string message)
        : base(message) { }
    }
}
