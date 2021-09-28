using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Business.Abstract;
using Entities.Concrete;
using Entities.DTOs;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PaymentController : ControllerBase
    {
        IRentalService _rentalService;
        ICreditCardService _creditCardService;
        public PaymentController(IRentalService rentalService, ICreditCardService creditCardService)
        {
            _rentalService = rentalService;
            _creditCardService = creditCardService;
        }

        [HttpPost("getpayment")]
        public ActionResult GetPayment(FakePaymentInfoDto fakePaymentInfoDto)
        {
            var result = _rentalService.Add(fakePaymentInfoDto.Rental);
            if (result.Success)
            {
                return Ok(result);
            }

            return BadRequest(result);
        }

    }
}
