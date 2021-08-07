using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Business.Abstract;
using Business.Constants;
using Core.Utilities.Results;
using DataAccess.Abstract;
using Entities.Concrete;

namespace Business.Concrete
{
    public class RentalManager:IRentalService
    {
        private IRentalDal _rentalDal;

        public RentalManager(IRentalDal rentalDal)
        {
            _rentalDal = rentalDal;
        }

        public IResult Add(Rental rental)
        {
            if (!isCarAvailable(rental))
            {
                return new ErrorResult(Messages.UserInvalid);
            }
            _rentalDal.Add(rental);
            return new SuccessResult(Messages.UserAdded);
        }

        public IResult Update(Rental rental)
        {
            if (!isCarAvailable(rental))
            {
                return new ErrorResult(Messages.RentalInvalid);
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

        public bool isCarAvailable(Rental rental)
        {
            var result = _rentalDal.GetAll(r => r.CarId == rental.CarId);
            if (result.Any(r =>
                r.ReturnDate >= rental.ReturnDate &&
                r.RentDate <= rental.ReturnDate
            )) return false;
            return true;

        }
    }
}
