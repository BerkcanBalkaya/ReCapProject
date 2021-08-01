using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entities.Concrete;
using Entities.DTOs;

namespace Business.Abstract
{
    public interface ICarService
    {
        void Add(Car car);
        void Update(Car car);
        void Delete(Car car);

        List<Car> GetCarsByBrandId(int id);
        List<Car> GetCarsByColorId(int id);
        List<Car> GetAll();
        Car GetById(int id);

        List<CarDetailDto> GetCarDetails();
    }
}
