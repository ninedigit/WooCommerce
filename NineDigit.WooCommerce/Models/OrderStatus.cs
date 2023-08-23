using System.ComponentModel;

namespace NineDigit.WooCommerce;

public enum OrderStatus
{
    [Description("any")]
    Any,
    [Description("pending")]
    Pending,
    [Description("processing")]
    Processing,
    [Description("on-hold")]
    OnHold,
    [Description("completed")]
    Completed,
    [Description("cancelled")]
    Cancelled,
    [Description("refunded")]
    Refunded,
    [Description("failed")]
    Failed,
    [Description("trash")]
    Trash
}
