namespace RtsimTestTask.Core.DataTransferObjects;

public abstract record OperationResult<T>(bool IsSuccess, T? ReturnValue, IEnumerable<string>? Errors = null);

public record SuccessResult<T>(T ReturnValue) : OperationResult<T>(true, ReturnValue, null);

public record FailedResult<T>(IEnumerable<string> Errors) : OperationResult<T>(false, default, Errors);

public abstract record OperationResult(bool IsSuccess, IEnumerable<string>? Errors = null);

public record SuccessResult() : OperationResult(true, null);

public record FailedResult(IEnumerable<string> Errors) : OperationResult(false, Errors);