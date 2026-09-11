using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HackNewsApp.Domain.Enums;

namespace HackNewsApp.Application.Common
{
    public abstract class ServiceBase
    {
        protected async Task<Result<T>> ExecuteAsync<T>(Func<Task<T>> operation)
        {
            try
            {
                var result = await operation();
                return Result<T>.Success(result);
            }
            catch (HttpRequestException ex)
            {
                return Result<T>.Failure($"External API Error: {ex.Message}", ErrorType.ExternalService);
            }
            catch (TaskCanceledException ex)
            {
                return Result<T>.Failure("Request timed out.", ErrorType.ExternalService);
            }
            catch (UnauthorizedAccessException ex)
            {
                return Result<T>.Failure(ex.Message, ErrorType.Unauthorised);
            }
            catch (Exception ex)
            {
                return Result<T>.Failure($"Unexpected Error: {ex.Message}", ErrorType.Unexpected);
            }


        }

        protected Result<T> Execute<T>(Func<T> operation)
        {
            try
            {
                var result = operation();
                return Result<T>.Success(result);
            }
            catch (Exception ex)
            {
                return Result<T>.Failure(ex.Message, ErrorType.Unexpected);
            }
        }
    }
}
