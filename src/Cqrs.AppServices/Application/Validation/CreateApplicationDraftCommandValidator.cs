using System;
using System.Collections.Generic;
using Cqrs.AppServices.Application.Commands;
using Cqrs.Infrastructure.Validation;

namespace Cqrs.AppServices.Application.Validation
{
    /// <summary>
    /// Валидатор команды создания черновика заявки.
    /// </summary>
    public class CreateApplicationDraftCommandValidator : IMessageValidator<CreateApplicationDraftCommand>
    {
        /// <inheritdoc />
        public bool Validate(CreateApplicationDraftCommand message, out IReadOnlyCollection<string> errors)
        {
            errors = Array.Empty<string>();
            return true;
        }
    }
}