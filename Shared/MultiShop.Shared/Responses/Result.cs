using MultiShop.Shared.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;

namespace MultiShop.Shared.Responses
{
    public sealed class Result<T>
    {
        public T? Data { get; set; }
        public List<string> ErrorMessages { get; set; }
        public bool IsSuccessful { get; set; } = true;

        [JsonIgnore]
        public int StatusCode { get; set; } = 200;
        public UiCriticality Criticality { get; set; } = UiCriticality.None;

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
        }
        public Result(int statusCode, string errorMessage)
        {
            IsSuccessful = false;
            StatusCode = statusCode;
            ErrorMessages = new List<string> { errorMessage };
        }
        public static implicit operator Result<T>(T data)
        {
            return new Result<T>(data);
        }
        public static implicit operator Result<T>((int statusCode, List<string>errorMessages)parameters)
        {
            return new Result<T>(parameters.statusCode,parameters.errorMessages);
        }
        public static implicit operator Result<T>((int statusCode,string errorMessage) parameters)
        {
            return new Result<T>(parameters.statusCode, parameters.errorMessage);
        }
        public static Result<T> Succeed(T data)
        {
            return new Result<T>(data);
        }
        public static Result<T> Failure(int statusCode, List<string> errorMessages)
        {
            return new Result<T>(statusCode, errorMessages);
        }
        public static Result<T> Failure(int statusCode,string errorMessage)
        {
            return new Result<T>(statusCode, errorMessage);
        }
        public static Result<T> Failure(string errorMessage)
        {
            return new Result<T>(500,errorMessage);
        }
        public static Result<T> Failure(List<string> errorMessages)
        {
            return new Result<T>(500, errorMessages);
        }
    }
}
