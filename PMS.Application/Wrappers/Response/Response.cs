using System.Collections;

namespace PMS.Application.Wrappers.Response
{
    public class Response<T> : IResponse<T>
    {
        public List<string> Messages { get; set; } = new List<string>();

        public bool Succeeded { get; set; }

        public T Data { get; set; }
        //public IEnumerable<T> Data { get; set; }

        //public List<ValidationResult> ValidationErrors { get; set; }

        public Exception Exception { get; set; }

        public int Code { get; set; }

        int TotalCount { get; set; }




        #region Non Async Methods 

        #region Success Methods 

        public static Response<T> Success()
        {
            return new Response<T>
            {
                Succeeded = true
            };
        }

        public static Response<T> Success(string message)
        {
            return new Response<T>
            {
                Succeeded = true,
                Messages = new List<string> { message }
            };
        }

        public static Response<T> Success(T data)
        {
            return new Response<T>
            {
                Succeeded = true,
                Data = data
            };
        }

        public static Response<T> Success(T data, string message)
        {
            return new Response<T>
            {
                Succeeded = true,
                Messages = new List<string> { message },
                Data = data
            };
        }

        #endregion

        #region Failure Methods 

        public static Response<T> Failure()
        {
            return new Response<T>
            {
                Succeeded = false
            };
        }

        public static Response<T> Failure(string message)
        {
            return new Response<T>
            {
                Succeeded = false,
                Messages = new List<string> { message }
            };
        }

        public static Response<T> Failure(List<string> messages)
        {
            return new Response<T>
            {
                Succeeded = false,
                Messages = messages
            };
        }

        public static Response<T> Failure(T data)
        {
            return new Response<T>
            {
                Succeeded = false,
                Data = data
            };
        }

        public static Response<T> Failure(T data, string message)
        {
            return new Response<T>
            {
                Succeeded = false,
                Messages = new List<string> { message },
                Data = data
            };
        }

        public static Response<T> Failure(T data, List<string> messages)
        {
            return new Response<T>
            {
                Succeeded = false,
                Messages = messages,
                Data = data
            };
        }

        public static Response<T> Failure(Exception exception)
        {
            return new Response<T>
            {
                Succeeded = false,
                Exception = exception
            };
        }

        #endregion

        #endregion

        #region Async Methods 

        #region Success Methods 

        public static Task<Response<T>> SuccessAsync()
        {
            return Task.FromResult(Success());
        }

        public static Task<Response<T>> SuccessAsync(string message)
        {
            return Task.FromResult(Success(message));
        }

        public static Task<Response<T>> SuccessAsync(T data)
        {
            return Task.FromResult(Success(data));
        }

        public static Task<Response<T>> SuccessAsync(T data, string message)
        {
            return Task.FromResult(Success(data, message));
        }

        #endregion

        #region Failure Methods 

        public static Task<Response<T>> FailureAsync()
        {
            return Task.FromResult(Failure());
        }

        public static Task<Response<T>> FailureAsync(string message)
        {
            return Task.FromResult(Failure(message));
        }

        public static Task<Response<T>> FailureAsync(List<string> messages)
        {
            return Task.FromResult(Failure(messages));
        }

        public static Task<Response<T>> FailureAsync(T data)
        {
            return Task.FromResult(Failure(data));
        }

        public static Task<Response<T>> FailureAsync(T data, string message)
        {
            return Task.FromResult(Failure(data, message));
        }

        public static Task<Response<T>> FailureAsync(T data, List<string> messages)
        {
            return Task.FromResult(Failure(data, messages));
        }

        public static Task<Response<T>> FailureAsync(Exception exception)
        {
            return Task.FromResult(Failure(exception));
        }

        #endregion

        #endregion

    }
}