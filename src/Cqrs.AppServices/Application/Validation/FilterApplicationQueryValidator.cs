using System;
using System.Collections.Generic;
using Cqrs.Contracts.Application.Queries;
using Cqrs.Domain;
using Cqrs.Infrastructure.Validation;

namespace Cqrs.AppServices.Application.Validation
{
    public class FilterApplicationQueryValidator : IMessageValidator<FilterApplicationsQuery>
    {
        /// <inheritdoc />
        public bool Validate(FilterApplicationsQuery message, out IReadOnlyCollection<string> errors)
        {
            errors = Array.Empty<string>();
            
            var errorList = new List<string>();
            if (message == null)
            {
                errorList.Add("Фильтр не задан");
            }
            else if (message.Status.HasValue && !Enum.IsDefined(typeof(ApplicationStatus), message.Status.Value))
            {
                errorList.Add($"Значение '{message.Status}' не является действительным для статуса заявки");
            }

            errors = errorList;
            return errors.Count == 0;
        }
    }
}