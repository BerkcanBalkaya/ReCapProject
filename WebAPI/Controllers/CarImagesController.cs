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
                string path = _webHostEnvironment.WebRootPath+"/images";
                string fileNameWithGUID = $"{Guid.NewGuid().ToString()}{Path.GetExtension(image.FileName)}";

                if (!Directory.Exists(path))
                {
                    Directory.CreateDirectory(path);
                }

                using (FileStream fileStream = System.IO.File.Create(Path.Combine(path, fileNameWithGUID)))
                {
                    image.CopyTo(fileStream);
                    fileStream.Flush();
                }
                carImage.ImagePath = Path.Combine(path, fileNameWithGUID);
                var result = _carImageService.Add(carImage);
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
            try
            {
                string path = _webHostEnvironment.WebRootPath + "/images";
                string fileNameWithGUID = $"{Guid.NewGuid().ToString()}{Path.GetExtension(image.FileName)}";
                var iresult = _carImageService.GetAll().Data.Where(c=>c.Id==carImage.Id).Any();

                if (iresult)
                {
                    if (!System.IO.File.Exists(carImage.ImagePath))
                    {
                        throw new FileNotFoundException();
                    }
                    System.IO.File.Delete(carImage.ImagePath);
                    using (FileStream fileStream = System.IO.File.Create(Path.Combine(path, fileNameWithGUID)))
                    {
                        image.CopyTo(fileStream);
                        fileStream.Flush();
                    }
                    carImage.ImagePath = Path.Combine(path, fileNameWithGUID);
                    var result = _carImageService.Update(carImage);
                    if (result.Success)
                    {
                        return Ok(result);
                    }
                    return BadRequest(result);
                }
                return BadRequest(iresult);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
            
        }

        [HttpPost("delete")]
        public IActionResult Delete([FromForm]CarImage carImage)
        {
            try
            {
                if (!System.IO.File.Exists(carImage.ImagePath))
                {
                    throw new FileNotFoundException();
                }
                System.IO.File.Delete(carImage.ImagePath);

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
