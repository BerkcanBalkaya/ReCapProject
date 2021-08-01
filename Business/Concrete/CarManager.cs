using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Business.Abstract;
using DataAccess.Abstract;
using Entities.Concrete;

namespace Business.Concrete
{
    public class CarManager:ICarService
    {
        ICarDal _icarCarDal;

        public CarManager(ICarDal icarCarDal)
        {
            _icarCarDal = icarCarDal;
        }

        public void Add(Car car)
        {
            if (car.Description.Length>2 && car.DailyPrice >0)
            {
                _icarCarDal.Add(car);
            }
            else
            {
                throw new Exception("Girdiğiniz değerler yanlıştır");
            }
        }

        public void Update(Car car)
        {
            _icarCarDal.Update(car);
        }

        public void Delete(Car car)
        {
            _icarCarDal.Delete(car);
        }

        public List<Car> GetCarsByBrandId(int id)
        {
            return _icarCarDal.GetAll(c=>c.BrandId==id);
        }

        public List<Car> GetCarsByColorId(int id)
        {
            return _icarCarDal.GetAll(c => c.ColorId == id);
        }


        public List<Car> GetAll()
        {
            return _icarCarDal.GetAll();
        }
    }
}
