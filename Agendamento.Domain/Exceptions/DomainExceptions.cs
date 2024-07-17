namespace Agendamento.Domain.Exceptions
{
    public class NotFoundException : Exception
    {
        public NotFoundException(string message) : base(message) { }
    }

    public class DomainValidationException : Exception
    {
        public List<string> ValidationErrors { get; }

        public DomainValidationException(string message) : base(message)
        {
            ValidationErrors = new List<string> { message };
        }

        public DomainValidationException(List<string> messages) : base(string.Join(Environment.NewLine, messages))
        {
            ValidationErrors = messages;
        }

        public static void When(bool hasError, string error)
        {
            if (hasError)
                throw new DomainValidationException(error);
        }

        public static void When(bool hasError, List<string> errors)
        {
            if (hasError && errors.Count > 0)
                throw new DomainValidationException(errors);
        }
    }

    public class DatabaseException : Exception
    {
        public DatabaseException(string message, Exception innerException) : base(message, innerException) { }
    }

    public class ConflictException : Exception
    {
        public ConflictException(string message) : base(message) { }
    }
}
