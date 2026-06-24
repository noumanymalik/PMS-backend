using PMS.Application.Common.Exceptions;
using PMS.Application.Wrappers.Response;
using MediatR;
using System.ComponentModel.DataAnnotations;

namespace PMS.Application.Wrappers
{
    public class ListQuery<TDto> : IRequest<IResponse<TDto>>
    {
        /// <summary>
        /// The expression used for sorting.
        /// </summary>
        public string OrderBy { get; set; } = "id";
        public string? OrderDirection { get; set; } = "asc";

        /// <summary>
        /// The expression used for filtering the results.
        /// e.g. Name eq 'Rice'
        /// </summary>
        public string? Filter { get; set; }
        public string? SearchText { get; set; }


        public void ThrowOrderByIncorrectException(Exception? innerException)
        {
            throw new InputValidationException(innerException,
                (
                    PropertyName: nameof(OrderBy),
                    ErrorMessage: $"The specified orderBy string '{OrderBy}' is invalid."
                )
            );
        }

        public void ThrowFilterIncorrectException(Exception? innerException)
        {
            throw new InputValidationException(innerException,
                (
                    PropertyName: nameof(Filter),
                    ErrorMessage: $"The specified filter string '{Filter}' is invalid."
                )
            );
        }
    }
}
