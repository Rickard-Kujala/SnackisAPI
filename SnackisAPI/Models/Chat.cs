using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SnackisAPI.Models
{
    public class Chat
    {
        public Guid Id { get; set; }
        public string ReceiverId { get; set; }
        public string SenderId { get; set; }
        public DateTime Date { get; set; }
        public bool IsRead { get; set; }
        public List<string> GroupMembers { get; set; } = new();
        public string GroupAdminId { get; set; }
    }
}
