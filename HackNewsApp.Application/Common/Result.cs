using HackNewsApp.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HackNewsApp.Application.Common
{
    public class Result<T>
    {
        public bool IsSuccess { get; }
        public T? Value { get; }
        public string? Error { get; }
        public ErrorType? ErrorType { get; }

        protected Result(bool isSuccess, T? value, string? error, ErrorType? errorType)
        {
            IsSuccess = isSuccess;
            Value = value;
            Error = error;
            ErrorType = errorType;
        }

        public static Result<T> Success(T value)
            => new(true, value, null, null);

        public static Result<T> Failure(string error, ErrorType errorType)
            => new(false, default, error, errorType);
    }
}
