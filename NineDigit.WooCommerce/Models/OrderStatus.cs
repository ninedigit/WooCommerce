using System.ComponentModel;

namespace NineDigit.WooCommerce;

public enum OrderStatus
{
    [Description("any")]
    Any,
    /// <summary>
    /// Order received, no payment initiated. Awaiting payment (unpaid).
    /// </summary>
    [Description("pending")]
    Pending,
    /// <summary>
    /// Payment received (paid) and stock has been reduced; order is awaiting fulfillment.
    /// All product orders require processing, except those that only contain products
    /// which are both virtual and downloadable.
    /// </summary>
    [Description("processing")]
    Processing,
    /// <summary>
    /// Awaiting payment – stock is reduced, but you need to confirm payment.
    /// </summary>
    [Description("on-hold")]
    OnHold,
    /// <summary>
    /// Order fulfilled and complete – requires no further action.
    /// </summary>
    [Description("completed")]
    Completed,
    /// <summary>
    /// Cancelled by an admin or the customer – stock is increased, no further action required.
    /// Please note: This status does not refund the customer.
    /// </summary>
    [Description("cancelled")]
    Cancelled,
    /// <summary>
    /// Refunded by an admin – no further action required.
    /// </summary>
    [Description("refunded")]
    Refunded,
    /// <summary>
    /// Payment failed or was declined (unpaid) or requires authentication (SCA).
    /// Note that this status may not show immediately
    /// and instead show as Pending until verified (e.g., PayPal).
    /// </summary>
    [Description("failed")]
    Failed,
    [Description("trash")]
    Trash
}
