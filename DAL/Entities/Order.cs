using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Entities
{
    public class Order
    {
        public int Id { get; set; }
        public bool Status { get; set; }
        public virtual User User { get; set; }
        public int UserId { get; set; }
    }
}
