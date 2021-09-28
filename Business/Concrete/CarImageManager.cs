using Business.Abstract;
using Business.Constants;
using Business.ValidationRules.FluentValidation;
using Core.Aspects.Autofac.Validation;
using Core.Utilities.BusinessRules;
using Core.Utilities.Results;
using DataAccess.Abstract;
using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Microsoft.AspNetCore.Http;

namespace Business.Concrete
{

    public class CarImageManager : ICarImageService
    {
        ICarImageDal _carImageDal;

        public CarImageManager(ICarImageDal carImageDal)
        {
            _carImageDal = carImageDal;
        }
        [ValidationAspect(typeof(CarImageValidator))]
        public IResult Add(IFormFile formFile, CarImage carImage)
        {
            var fileNameWithGUID = AddFile(formFile, carImage).Data;
            carImage.ImagePath = Path.Combine(carImage.ImagePath, fileNameWithGUID);
            var result = BusinessRules.Run(CheckIfCarImageLimitExceed(carImage.CarId));
            if (result != null)
            {
                return result;
            }
            carImage.UploadDate = DateTime.Now;
            _carImageDal.Add(carImage);
            return new SuccessResult(Messages.CarImageAdded);
        }

        

        [ValidationAspect(typeof(BrandValidator))]
        public IResult Update(IFormFile formFile, CarImage carImage)
        {
            var path = Path.GetDirectoryName(carImage.ImagePath);
            var result = BusinessRules.Run(GetById(carImage.Id),DeleteFile(carImage),AddFile(formFile, carImage));
            if (!result.Success)
            {
                return result;
            }
            carImage.ImagePath = Path.Combine(path,((SuccessDataResult<string>)result).Data);
            carImage.UploadDate = DateTime.Now;
            _carImageDal.Update(carImage);
            return new SuccessResult(Messages.CarImageUpdated);
        }
        public IResult Delete(CarImage carImage)
        {
            _carImageDal.Delete(carImage);
            DeleteFile(carImage);
            return new SuccessResult(Messages.CarImageDeleted);
        }

        public IDataResult<List<CarImage>> GetAll()
        {
            return new SuccessDataResult<List<CarImage>>(_carImageDal.GetAll(), Messages.CarImagesListed);
        }

        public IDataResult<CarImage> GetById(int id)
        {
            return new SuccessDataResult<CarImage>(_carImageDal.Get(cI => cI.Id == id), Messages.CarImagesListed);
        }

        public IDataResult<List<CarImage>> GetByCarId(int carId)
        {
            return CheckIfCarImageExists(carId);
        }
        private IDataResult<List<CarImage>> CheckIfCarImageExists(int carId)
        {
            var result = _carImageDal.GetAll(c => c.CarId == carId).Any();
            if (!result)
            {
                return new SuccessDataResult<List<CarImage>>(new List<CarImage>
                    { new() { CarId = carId, ImagePath = DefaultRoutes.DefaultImage, UploadDate = DateTime.Now } }, Messages.CarImagesListed);
            }
            return new SuccessDataResult<List<CarImage>>(_carImageDal.GetAll(c => c.CarId == carId), Messages.CarImageListedById);
        }

        private IResult CheckIfCarImageLimitExceed(int carId)
        {
            var result = _carImageDal.GetAll(c => c.CarId == carId).Count;
            if (result > 5)
            {
                return new ErrorResult(Messages.CarImageLimit);
            }
            return new SuccessResult();
        }
        private IDataResult<string> AddFile(IFormFile formFile, CarImage carImage)
        {
            string fileNameWithGUID = $"{Guid.NewGuid().ToString()}{Path.GetExtension(formFile.FileName)}";

            if (!Directory.Exists(carImage.ImagePath))
            {
                Directory.CreateDirectory(carImage.ImagePath);
            }

            using (FileStream fileStream = System.IO.File.Create(Path.Combine(carImage.ImagePath, fileNameWithGUID)))
            {
                formFile.CopyTo(fileStream);
                fileStream.Flush();
            }

            return new SuccessDataResult<string>(fileNameWithGUID,Messages.FileAdded);
        }

        private IResult DeleteFile(CarImage carImage)
        {
            try
            {
                if (!System.IO.File.Exists(carImage.ImagePath))
                {
                    throw new FileNotFoundException();
                }

                System.IO.File.Delete(carImage.ImagePath);
                return new SuccessResult();
            }
            catch (Exception e)
            {

                return new ErrorResult(e.Message);
            }
        }
    }
}
