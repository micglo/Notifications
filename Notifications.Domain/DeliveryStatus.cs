using System.ComponentModel.DataAnnotations;

namespace Notifications.Domain
{
    public enum DeliveryStatus
    {
        [Display(Name = "Wysłany")]
        Sent = 0,
        [Display(Name = "Powiadomiony")]
        Notified = 1,
        [Display(Name = "Odczytany")]
        Delivered = 2,
        [Display(Name = "Usunięty")]
        Deleted = 3,
        [Display(Name = "Przedawniony")]
        Obsolete = -1,
    }
}