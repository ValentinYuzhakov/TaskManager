using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskManager.Domain.Models.Abstracts;

namespace TaskManager.Domain.Models
{
    public class RefreshToken : Entity
    {
        public string Token { get; set; }
        public DateTime ExpireDate { get; set; }
        public Guid UserId { get; set; }
        public User User { get; set; }
    }
}
