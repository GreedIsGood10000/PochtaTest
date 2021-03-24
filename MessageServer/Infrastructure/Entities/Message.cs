using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Net;

namespace MessageServer.Infrastructure.Entities
{
    public class Message
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [MaxLength(40)]
        public string SenderIp { get; set; }

        public string MessageText { get; set; }

        public static Message Create(string messageText, IPAddress senderIp)
        {
            return new Message
            {
                MessageText = messageText,
                SenderIp = senderIp.ToString()
            };
        }
    }
}
