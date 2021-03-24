using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MessageClient.Infrastructure.Entities
{
    public class Message
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        public string MessageText { get; set; }

        public bool IsSend { get; set; }

        public static Message Create(string messageText, bool isSent = false)
        {
            return new Message
            {
                IsSend = isSent,
                MessageText = messageText
            };
        }
    }
}
