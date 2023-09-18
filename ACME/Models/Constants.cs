namespace ACME.Models;

public class ApiRoute
{
    public const string Default = "api/[controller]/[action]";    
}

public static class AuthConstants
{
    public const string ApiKeySectionName = "Authentication:ApiKey";
    public const string ApiKeyHeaderName = "X-Api-Key";
}

public class DefaultValue
{
    public const decimal SalesTax = 5.0M;
}

public enum OrderStatus
{
    New,
    InProgress,
    PaidInFull,
    ChangeDue,
    Shipped,
}

public class ErrorMessages
{
    public const string ApiKeyMissing = "API Key is missing.";
    public const string ApiKeyInvalid = "API Key is invalid.";
    public const string OrderNumberInvalid = "Invalid order [{0}].";
    public const string OrderShippedCannotBeModified = "Order [{0}] has already been shipped and cannot accept any modifications.";
    public const string OrderPaymentExceedsAmountDue = "The payment amount {0:C} exceeds the amount due {1:C}";
}