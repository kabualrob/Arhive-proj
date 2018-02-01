using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Arhive2018.Entities
{
    public class User
    {
        public int Id { get; set; }
        public string Fio { get; set; }
        public string Description { get; set; }
        public bool IsAdmin { get; set; }
        public bool Blocked { get; set; }

        public User(int id,string fio, string description, bool isAdmin, bool blocked)
        {
            Id = id;
            Fio = fio;
            Description = description;
            IsAdmin = isAdmin;
            Blocked = blocked;
        }
    }
}
