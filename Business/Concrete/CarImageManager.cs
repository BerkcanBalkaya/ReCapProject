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
using System.Linq;

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
        public IResult Add(CarImage carImage)
            {
            var result = BusinessRules.Run(CheckIfCarImageLimitExceed(carImage.CarId));
            if (result!=null)
            {
                return result;
            }
            carImage.UploadDate= DateTime.Now;
            _carImageDal.Add(carImage);
            return new SuccessResult(Messages.CarImageAdded);
        }
        [ValidationAspect(typeof(BrandValidator))]
        public IResult Update(CarImage carImage)
        {
            carImage.UploadDate = DateTime.Now;
            _carImageDal.Update(carImage);
            return new SuccessResult(Messages.CarImageUpdated);
        }
        public IResult Delete(CarImage carImage)
        {
            var result = BusinessRules.Run(CheckIfCarImageExists(carImage.CarId));
            if (result!=null)
            {
                return result;
            }
            _carImageDal.Delete(carImage);
            return new SuccessResult(Messages.CarImageDeleted);
        }

        public IDataResult<List<CarImage>> GetAll()
        {
            return new SuccessDataResult<List<CarImage>>(_carImageDal.GetAll(), Messages.CarImagesListed);
        }

        public IDataResult<CarImage> GetByCarId(int carId)
        {
            var result=BusinessRules.Run(CheckIfCarImageExists(carId));
            if (result!=null)
            {
                return (ErrorDataResult<CarImage>) result;
            }
            return new SuccessDataResult<CarImage>(_carImageDal.Get(c => c.CarId == carId), Messages.CarImageListedById);
        }
        private IDataResult<CarImage> CheckIfCarImageExists(int carId)
        {
            var result = _carImageDal.GetAll(c => c.CarId == carId).Any();
            if (!result)
            {
                
                return new ErrorDataResult<CarImage>(new CarImage{CarId = carId,ImagePath = DefaultRoutes.DefaultImage,UploadDate = DateTime.Now},Messages.CarImageEmpty);
            }
            return new SuccessDataResult<CarImage>(Messages.CarImageNotEmpty); 
        }

        private IResult CheckIfCarImageLimitExceed(int carId)
        {
            var result = _carImageDal.GetAll(c => c.CarId == carId).Count;
            if (result > 5 )
            {
                return new ErrorResult(Messages.CarImageLimit);
            }
            return new SuccessResult();
        }
        
    }
}
