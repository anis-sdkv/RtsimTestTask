namespace RtsimTestTask.Domain.Constants;

public static class PasswordComplexityContanst
{
    public const int MinPasswordLength = 6;
    public const bool RequiresUppercase = true;
    public const bool RequiresLowercase = true;
    public const bool RequiresDigit = true;
    public const bool RequireNonAlphanumeric = true;
}