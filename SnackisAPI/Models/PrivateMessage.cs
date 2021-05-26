using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SnackisAPI.Models
{
    public class PrivateMessage
    {
        public Guid Id { get; set; }
        public string Message { get; set; }
        public string SenderId { get; set; }
        public string ReceiverId { get; set; }
        public DateTime DateTime { get; set; }
    }
}
