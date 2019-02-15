using System;
using System.Collections.Generic;
using Cqrs.AppServices.Application.Queries;
using Cqrs.Domain.Models;
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
            if (message.Filter == null)
            {
                errorList.Add("Фильтр не задан");
            }
            else if (message.Filter.Status.HasValue && !Enum.IsDefined(typeof(ApplicationStatus), message.Filter.Status.Value))
            {
                errorList.Add($"Значение '{message.Filter.Status}' не является действительным для статуса заявки");
            }

            errors = errorList;
            return errors.Count == 0;
        }
    }
}