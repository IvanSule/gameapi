namespace RPSSL.Application.Exceptions
{
    public sealed class CustomValidationException : Exception
    {
        public CustomValidationException(Dictionary<string, string[]> errors)
        : base("One or more validation errors occurred.") =>
        Errors = errors;

        public Dictionary<string, string[]> Errors { get; }
    }
}
