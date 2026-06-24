using PMS.Application.Common.Exceptions;
using PMS.Application.Wrappers.Response;
using MediatR;
using System.ComponentModel.DataAnnotations;

namespace PMS.Application.Wrappers
{
    public class ListPagedQuery<TDto> : IRequest<IPagedListResponse<TDto>>
    {
        private const int DEFAULT_PAGESIZE = 20;
        private const int MAX_PAGESIZE = 100;


        /// <summary>
        /// The index of the page to fetch.
        /// </summary>
        [Range(1, int.MaxValue, ErrorMessage = "The minimum page index is 1.")]
        public int PageIndex { get; set; } = 1;

        /// <summary>
        /// The page size used for fetching data.
        /// </summary>
        [Range(1, MAX_PAGESIZE)]
        public int PageSize { get; set; } = DEFAULT_PAGESIZE;

        /// <summary>
        /// The expression used for sorting.
        /// </summary>
        public string OrderBy { get; set; } = "id";
        public string? OrderDirection { get; set; } = "asc";

        /// <summary>
        /// The expression used for filtering the results.
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
