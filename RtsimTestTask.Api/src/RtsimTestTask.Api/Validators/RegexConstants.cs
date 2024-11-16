namespace RtsimTestTask.Api.Validators;

public static class RegexConstants
{
    public const string AtLeastOneUppercase = @"[A-Z]";
    public const string AtLeastOneLowercase = @"[a-z]";
    public const string AtLeastOneDigit = @"[0-9]";
    public const string AtLeastOneNonAlphanumeric  = @"\W";
    
    public const string PhonePattern  = @"^\+?[1-9]\d{1,14}$";
}