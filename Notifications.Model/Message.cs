using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Notifications.Model
{
    public class Message
    {
        [Required]
        [DisplayName("Odbiorcy")]
        public string Recipients { get; set; }

        [Required]
        [DisplayName("Temat")]
        public string Subject { get; set; }

        [Required]
        [DisplayName("Wiadomość")]
        public string Text { get; set; }
    }
}