using System;

namespace Cqrs.Infrastructure.Exceptions
{
    /// <summary>
    /// Набор расширений для <see cref="Exception"/>.
    /// </summary>
    public static class ExceptionExtensions
    {
        /// <summary>
        /// Выполняет попытку извлечь пользовательское исключение.
        /// </summary>
        public static bool TryGetUserException(this Exception exception, out UserException userException)
        {
            userException = null;
            
            if (exception == null)
                throw new ArgumentException(nameof(exception));

            var current = exception;
            do
            {
                if (current is UserException ue)
                {
                    userException = ue;
                    return true;
                }
                
                current = current.InnerException;
                
            } while (current != null);

            return false;
        }
    }
}