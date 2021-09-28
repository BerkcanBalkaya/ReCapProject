using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core.Entities;

namespace Entities.Concrete
{
    public class CreditCard:IEntity
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public string CreditCardType { get; set; }
        public string CardNumber { get; set; }
        public string CVC { get; set; }
        public string ExpirationDateMonth { get; set; }
        public string ExpirationDateYear { get; set; }

    }
}
