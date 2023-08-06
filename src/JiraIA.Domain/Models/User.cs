using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JiraIA.Domain.Models
{
    public class User : BaseEntity
    {
        public string UserName { get; set; }

        public string Password { get; set; }

        public string Role { get; set; }

        public DateTime CreatedAt { get; set; }
    }
}
