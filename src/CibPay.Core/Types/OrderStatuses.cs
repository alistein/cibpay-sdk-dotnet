namespace CibPaySdk.Core.Types;

/// <summary>
/// Represents the various statuses an order can have during payment processing.
/// </summary>
public enum OrderStatuses
{
    /// <summary>
    /// The order has been created and an ID has been assigned. The status is valid until the cardholder presses the "Pay" button.
    /// </summary>
    New,

    /// <summary>
    /// The cardholder's actions are expected to authenticate on the side of the issuing bank (for example: entering a password for authorization, passing the 3-D Secure procedure).
    /// </summary>
    Prepared,

    /// <summary>
    /// Successful authorization has been completed: a response has been received indicating that funds have been successfully held in the cardholder's account to pay for the order.
    /// </summary>
    Authorized,

    /// <summary>
    /// The funds held were written off from the cardholder's account.
    /// </summary>
    Charged,

    /// <summary>
    /// Cancellation of holding funds.
    /// </summary>
    Reversed,

    /// <summary>
    /// A successful refund was made to the cardholder's account.
    /// </summary>
    Refunded,

    /// <summary>
    /// The order was rejected by the system (for example: due to limit or routing settings).
    /// </summary>
    Rejected,

    /// <summary>
    /// The order was identified as fraudulent and rejected by the system.
    /// </summary>
    Fraud,

    /// <summary>
    /// The order was rejected by the acquiring bank.
    /// </summary>
    Declined,

    /// <summary>
    /// The order was marked as chargeback.
    /// </summary>
    ChargedBack,

    /// <summary>
    /// Successful Enrollment (OCT) completed.
    /// </summary>
    Credited,

    /// <summary>
    /// An error occurred during execution and may require support assistance.
    /// </summary>
    Error
}

public static class OrderStatusUtils
{
    public static OrderStatuses? ParseToOrderStatus(string? statusText) =>
        statusText switch
        {
            "new" => OrderStatuses.New,
            "prepared" => OrderStatuses.Prepared,
            "authorized" => OrderStatuses.Prepared,
            "charged" => OrderStatuses.Charged,
            "reversed" => OrderStatuses.Reversed,
            "refunded" => OrderStatuses.Refunded,
            "rejected" => OrderStatuses.Rejected,
            "fraud" => OrderStatuses.Fraud,
            "declined" => OrderStatuses.Declined,
            "chargedback" => OrderStatuses.ChargedBack,
            "credited" => OrderStatuses.Credited,
            "error" => OrderStatuses.Error,

            _ => null
        };
}


