using System;

namespace Cqrs.Infrastructure.Exceptions
{
    /// <summary>
    /// Исключение отображаемое пользователю.
    /// </summary>
    [Serializable]
    public class UserException : Exception
    {
        /// <summary>
        /// Описание.
        /// </summary>
        public string Description { get; }
        
        public UserException(string message, string description) 
            : base(message)
        {
            Description = description;
        }

        public UserException(string message, string description, Exception innerException) 
            : base(message, innerException)
        {
            Description = description;
        }       
    }
}