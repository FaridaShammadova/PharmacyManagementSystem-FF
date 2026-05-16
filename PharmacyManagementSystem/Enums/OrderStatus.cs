using System.ComponentModel;

namespace PharmacyManagementSystem.Enums
{
    public enum OrderStatus
    {
        [Description("Gözləmədə")]
        Pending,

        [Description("Təsdiqləndi")]
        Preparing,

        [Description("Yoldadır")]
        Shipping,

        [Description("Çatdırıldı")]
        Delivered,

        [Description("Ləğv Edildi")]
        Cancelled
    }
}
