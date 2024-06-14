namespace Agendamento.Domain.Exceptions
{
    public class NotFoundException : Exception
    {
        public NotFoundException(string message) : base(message) { }
    }

    public class ValidationException : Exception
    {
        public ValidationException(string message) : base(message) { }

        public static void When(bool hasError, string error)
        {
            if (hasError)
                throw new ValidationException(error);
        }
    }

    public class DatabaseException : Exception
    {
        public DatabaseException(string message, Exception innerException) : base(message, innerException) { }
    }
}