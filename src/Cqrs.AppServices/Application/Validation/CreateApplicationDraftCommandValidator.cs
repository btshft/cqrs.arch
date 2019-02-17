using System;
using System.Collections.Generic;
using Cqrs.Contracts.Application.Commands;
using Cqrs.Infrastructure.Validation;

namespace Cqrs.AppServices.Application.Validation
{
    /// <summary>
    /// Валидатор команды создания черновика заявки.
    /// </summary>
    public class CreateApplicationDraftCommandValidator : IMessageValidator<CreateApplicationDraft>
    {
        /// <inheritdoc />
        public bool Validate(CreateApplicationDraft message, out IReadOnlyCollection<string> errors)
        {
            errors = Array.Empty<string>();
            return true;
        }
    }
}