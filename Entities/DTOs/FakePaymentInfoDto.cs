using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core.Entities;
using Entities.Concrete;

namespace Entities.DTOs
{
    public class FakePaymentInfoDto:IDto
    {
        public CreditCard CreditCard { get; set; } 
        public Rental Rental { get; set; } 
    }
}
