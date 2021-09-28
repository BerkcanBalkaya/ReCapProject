using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Business.Constants;
using System;
using System.Linq;
using System.IO;
using Business.Abstract;
using Entities.Concrete;
using Microsoft.AspNetCore.Hosting;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CarImagesController : ControllerBase
    {
        ICarImageService _carImageService;
        IWebHostEnvironment _webHostEnvironment;
        public CarImagesController(ICarImageService carImageService, IWebHostEnvironment webHostEnvironment)
        {
            _carImageService = carImageService;
            _webHostEnvironment = webHostEnvironment;
        }

        [HttpGet("getall")]
        public IActionResult GetAll()
        {
            var result = _carImageService.GetAll();
            if (result.Success)
            {
                return Ok(result);
            }

            return BadRequest(result);
        }

        //TODO: will be removed in production(foreach)
        [HttpGet("getbycarid")]
        public IActionResult GetByCarId(int id)
        {
            var result = _carImageService.GetByCarId(id);
            
            foreach (var carImage in result.Data)
            {
                carImage.ImagePath = $"{DefaultRoutes.DefaultImageFolder}{Path.GetFileName(carImage.ImagePath)}";
            }
            if (result.Success)
            {
                return Ok(result);
            }

            return BadRequest(result);
        }

        [HttpPost("add")]
        public IActionResult Add([FromForm] CarImage carImage, IFormFile image)
        {
            try
            {
                carImage.ImagePath = _webHostEnvironment.WebRootPath+"/images";
                var result = _carImageService.Add(image, carImage);
                if (result.Success)
                {
                    return Ok(result);
                }
                return BadRequest(result);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }

        }

        [HttpPost("update")]
        public IActionResult Update([FromForm]CarImage carImage,IFormFile image)
        {
            var result=_carImageService.Update(image,carImage);
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }

        [HttpPost("delete")]
        public IActionResult Delete([FromForm]CarImage carImage)
        {
            try
            {
                var result = _carImageService.Delete(carImage);
                if (result.Success)
                {
                    return Ok(result);
                }
                return BadRequest(result);
                
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
            
        }
    }
}
