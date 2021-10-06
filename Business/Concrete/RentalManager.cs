using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Business.Abstract;
using Business.Constants;
using Business.ValidationRules.FluentValidation;
using Core.Aspects.Autofac.Validation;
using Core.Utilities.BusinessRules;
using Core.Utilities.Results;
using DataAccess.Abstract;
using Entities.Concrete;
using Entities.DTOs;

namespace Business.Concrete
{
    public class RentalManager:IRentalService
    {
        private IRentalDal _rentalDal;

        public RentalManager(IRentalDal rentalDal)
        {
            _rentalDal = rentalDal;
        }
        [ValidationAspect(typeof(RentalValidator))]
        public IResult Add(Rental rental)
        {
            IResult result = BusinessRules.Run(isCarAvailable(rental));
            if (result!=null)
            {
                return result;
            }
            _rentalDal.Add(rental);
            return new SuccessResult(Messages.RentalAdded);
            
            
        }
        [ValidationAspect(typeof(RentalValidator))]

        public IResult Update(Rental rental)
        {
            IResult result = BusinessRules.Run(isCarAvailable(rental));
            if (result != null)
            {
                return result;
            }
            _rentalDal.Update(rental);
            return new SuccessResult(Messages.RentalUpdated);

        }
        public IResult Delete(Rental rental)
        {
            _rentalDal.Delete(rental);
            return new SuccessResult(Messages.RentalDeleted);
        }

        public IDataResult<List<Rental>> GetAll()
        {
            return new SuccessDataResult<List<Rental>>(_rentalDal.GetAll(), Messages.RentalsListed);
        }

        public IDataResult<Rental> GetById(int id)
        {
            return new SuccessDataResult<Rental>(_rentalDal.Get(r => r.Id == id), Messages.RentalListedById);
        }

        public IDataResult<List<RentalDetailDto>> GetRentalDetails()
        {
            return new SuccessDataResult<List<RentalDetailDto>>(_rentalDal.GetRentalDetails(), Messages.CarDetailListed);
        }

        public IResult isCarAvailable(Rental rental)
        {
            var result = _rentalDal.GetAll(r => r.CarId == rental.CarId);
            if (result.Any(r =>
                r.ReturnDate >= rental.ReturnDate &&
                r.RentDate <= rental.ReturnDate
            )) return new ErrorResult(Messages.RentalDateOfCarInvalid);
            return new SuccessResult();

        }
    }
}
