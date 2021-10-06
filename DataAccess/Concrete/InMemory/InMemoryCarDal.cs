using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using DataAccess.Abstract;
using Entities.Concrete;
using Entities.DTOs;

namespace DataAccess.Concrete.InMemory
{
    public class InMemoryCarDal:ICarDal
    {
        public Car CarToUpdate;
        public Car CarToDelete;
        public List<Car> _cars = new List<Car>()
        {
            new Car {Id = 1, BrandId = 1, ColorId = 1, ModelYear = 1998, DailyPrice = 130, Description = "Ucuz"},
            new Car {Id = 2, BrandId = 1, ColorId = 2, ModelYear = 2000, DailyPrice = 150, Description = "Orta"},
            new Car {Id = 3, BrandId = 2, ColorId = 3, ModelYear = 2010, DailyPrice = 250, Description = "Pahalı"},
            new Car {Id = 4, BrandId = 3, ColorId = 2, ModelYear = 1995, DailyPrice = 100, Description = "Çok Ucuz"}
        };
        public void Add(Car entity)
        {
            _cars.Add(entity);
        }

        public void Update(Car entity)
        {
            CarToUpdate = _cars.SingleOrDefault(c => c.Id == entity.Id);
            CarToUpdate.BrandId = entity.BrandId;
            CarToUpdate.ColorId = entity.ColorId;
            CarToUpdate.DailyPrice = entity.DailyPrice;
            CarToUpdate.Description = entity.Description;
            CarToUpdate.ModelYear = entity.ModelYear;
        }

        public void Delete(Car entity)
        {
            CarToDelete = _cars.SingleOrDefault(c => c.Id == entity.Id);
            _cars.Remove(CarToDelete);
        }

        public Car Get(Expression<Func<Car, bool>> filter)
        {
            throw new NotImplementedException();
        }

        public List<Car> GetAll(Expression<Func<Car, bool>> filter = null)
        {
            throw new NotImplementedException();
        }

        public List<CarDetailDto> GetCarDetails()
        {
            throw new NotImplementedException();
        }

        public CarDetailDto GetCarDetailsByCarId(int carId)
        {
            throw new NotImplementedException();
        }

        public List<CarDetailDto> GetCarDetailsByBrandId(int brandId)
        {
            throw new NotImplementedException();
        }

        public List<CarDetailDto> GetCarDetailsByColorId(int colorId)
        {
            throw new NotImplementedException();
        }

        public List<CarDetailDto> GetCarDetailsByColorAndBrandId(int colorId, int brandId)
        {
            throw new NotImplementedException();
        }

        public List<CarDetailDto> GetTopTenMostRentedCars()
        {
            throw new NotImplementedException();
        }

        public Car Get(int id)
        {
            return _cars.SingleOrDefault(c => c.Id == id);
        }

        public List<Car> GetAll()
        {
            return _cars;
        }
    }
}
