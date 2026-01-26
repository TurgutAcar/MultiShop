//using MultiShop.Shared.Enums;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Text.Json.Serialization;

//namespace MultiShop.Shared.Responses
//{
//    public sealed class Result<T>
//    {
//        public T? Data { get; set; }
//        public List<string> ErrorMessages { get; set; }
//        public bool IsSuccessful { get; set; } = true;

//        [JsonIgnore]
//        public int StatusCode { get; set; } = 200;
//        public UiCriticality Criticality { get; set; } = UiCriticality.None;

//        public Result() { }

//        public Result(T data)
//        {
//            Data = data;
//        }

//        public Result(int statusCode, List<string> errorMessages)
//        {
//            IsSuccessful = false;
//            StatusCode = statusCode;
//            ErrorMessages = errorMessages;
//        }
//        public Result(int statusCode, string errorMessage)
//        {
//            IsSuccessful = false;
//            StatusCode = statusCode;
//            ErrorMessages = new List<string> { errorMessage };
//        }
//        public static implicit operator Result<T>(T data)
//        {
//            return new Result<T>(data);
//        }
//        public static implicit operator Result<T>((int statusCode, List<string>errorMessages)parameters)
//        {
//            return new Result<T>(parameters.statusCode,parameters.errorMessages);
//        }
//        public static implicit operator Result<T>((int statusCode,string errorMessage) parameters)
//        {
//            return new Result<T>(parameters.statusCode, parameters.errorMessage);
//        }
//        public static Result<T> Succeed(T data)
//        {
//            return new Result<T>(data);
//        }
//        public static Result<T> Failure(int statusCode, List<string> errorMessages)
//        {
//            return new Result<T>(statusCode, errorMessages);
//        }
//        public static Result<T> Failure(int statusCode,string errorMessage)
//        {
//            return new Result<T>(statusCode, errorMessage);
//        }
//        public static Result<T> Failure(string errorMessage)
//        {
//            return new Result<T>(500,errorMessage);
//        }
//        public static Result<T> Failure(List<string> errorMessages)
//        {
//            return new Result<T>(500, errorMessages);
//        }
//    }
//}
using MultiShop.Shared.Enums;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace MultiShop.Shared.Responses
{
    public sealed class Result<T>
    {
        public T? Data { get; set; }
        public List<string> ErrorMessages { get; set; } = new();
        public bool IsSuccessful { get; set; } = true;

        [JsonIgnore]
        public int StatusCode { get; set; } = 200;

        public UiCriticality Criticality { get; set; } = UiCriticality.None;

        // 🔥 UI için ana karar mekanizması
        public ResultSource Source { get; set; } = ResultSource.Success;

        public Result() { }

        public Result(T data)
        {
            Data = data;
        }

        public Result(int statusCode, List<string> errorMessages)
        {
            IsSuccessful = false;
            StatusCode = statusCode;
            ErrorMessages = errorMessages;
            Source = MapSource(statusCode);
        }

        public Result(int statusCode, string errorMessage)
        {
            IsSuccessful = false;
            StatusCode = statusCode;
            ErrorMessages = new List<string> { errorMessage };
            Source = MapSource(statusCode);
        }

        // 🔁 Implicit operators
        public static implicit operator Result<T>(T data)
            => new Result<T>(data);

        public static implicit operator Result<T>((int statusCode, List<string> errorMessages) parameters)
            => new Result<T>(parameters.statusCode, parameters.errorMessages);

        public static implicit operator Result<T>((int statusCode, string errorMessage) parameters)
            => new Result<T>(parameters.statusCode, parameters.errorMessage);

        // ✅ Factory methods
        public static Result<T> Succeed(T data)
            => new Result<T>(data)
            {
                Source = ResultSource.Success
            };

        public static Result<T> Failure(int statusCode, List<string> errorMessages)
            => new Result<T>(statusCode, errorMessages);

        public static Result<T> Failure(int statusCode, string errorMessage)
            => new Result<T>(statusCode, errorMessage);

        public static Result<T> Failure(string errorMessage)
            => new Result<T>(500, errorMessage);

        public static Result<T> Failure(List<string> errorMessages)
            => new Result<T>(500, errorMessages);

        // 🧠 Tek merkezden HTTP → UI anlamı
        private static ResultSource MapSource(int statusCode)
        {
            return statusCode switch
            {
                400 => ResultSource.ValidationError,
                401 => ResultSource.Unauthorized,
                403 => ResultSource.Forbidden,
                404 => ResultSource.NotFound,
                408 => ResultSource.Timeout,
                429 => ResultSource.RateLimited,
                503 => ResultSource.ServiceUnavailable,
                >= 500 => ResultSource.ApiError,
                _ => ResultSource.UnknownError
            };
        }
    }
}
