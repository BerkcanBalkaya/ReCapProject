using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Business.Abstract;
using Entities.Concrete;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CreditCardController : ControllerBase
    {
        private ICreditCardService _creditCardService;

        public CreditCardController(ICreditCardService creditCardService)
        {
            _creditCardService = creditCardService;
        }

        [HttpGet("getbyuserid")]
        public IActionResult GetByUserId(int userId)
        {
            var result = _creditCardService.GetByUserId(userId);
            if (result.Success)
            {
                return Ok(result);
            }

            return BadRequest(result);
        }

        [HttpGet("getall")]
        public IActionResult GetAll()
        {
            var result = _creditCardService.GetAll();
            if (result.Success)
            {
                return Ok(result);
            }

            return BadRequest(result);
        }
        [HttpGet("getbyid")]
        public IActionResult GetById(int id)
        {
            var result = _creditCardService.GetById(id);
            if (result.Success)
            {
                return Ok(result);
            }

            return BadRequest(result);
        }

        [HttpPost("add")]
        public IActionResult Add(CreditCard creditCard)
        {
            var result = _creditCardService.Add(creditCard);
            if (result.Success)
            {
                return Ok(result);
            }

            return BadRequest(result);
        }

        [HttpPost("update")]
        public IActionResult Update(CreditCard creditCard)
        {
            var result = _creditCardService.Update(creditCard);
            if (result.Success)
            {
                return Ok(result);
            }

            return BadRequest(result);
        }

        [HttpPost("delete")]
        public IActionResult Delete(CreditCard creditCard)
        {
            var result = _creditCardService.Delete(creditCard);
            if (result.Success)
            {
                return Ok(result);
            }

            return BadRequest(result);
        }
    }
}
