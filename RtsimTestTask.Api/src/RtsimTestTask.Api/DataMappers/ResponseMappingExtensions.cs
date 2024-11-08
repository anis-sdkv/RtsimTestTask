using RtsimTestTask.Api.Responses;
using RtsimTestTask.Core.DataTransferObjects;

namespace RtsimTestTask.Api.DataMappers;

internal static class ResponseMappingExtensions
{
    internal static OperationResultResponse MapToResponse(this RegisterResult result) => result switch
    {
        OperationResult<Guid> success => success.MapToSuccessResponse(),
        RegisterFailed failed => failed.MapToFailedResponse(),
        _ => throw new InvalidOperationException()
    };

    private static OperationResultResponse<Guid> MapToSuccessResponse(this RegisterSuccess success) =>
        new(
            true,
            "The account has been successfully created!",
            Array.Empty<string>(),
            success.Id
        );

    private static OperationResultResponse MapToFailedResponse(this RegisterFailed failed) =>
        new(
            true,
            "Failed to create an account. Check for errors!",
            failed.Errors.ToArray()
        );
}