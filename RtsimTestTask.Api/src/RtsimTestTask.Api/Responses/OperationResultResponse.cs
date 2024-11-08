namespace RtsimTestTask.Api.Responses;

public record OperationResultResponse<T>(
    bool Success,
    string Message,
    string[] Errors,
    T Data
) : OperationResultResponse(Success, Message, Errors);

public record OperationResultResponse(
    bool Success,
    string Message,
    string[] Errors
);