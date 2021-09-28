using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Core.DataAccess.EntityFramework;
using DataAccess.Abstract;
using Entities.Concrete;
using Entities.DTOs;
using Microsoft.EntityFrameworkCore;

namespace DataAccess.Concrete.EntityFramework
{
    public class EfCarDal : EfEntityRepositoryBase<Car, ReCapProjectContext>, ICarDal
    {
        public List<CarDetailDto> GetCarDetails()
        {
            using (ReCapProjectContext context = new ReCapProjectContext())
            {
                var result = from c in context.Cars
                             join b in context.Brands on c.BrandId equals b.Id
                             join co in context.Colors on c.ColorId equals co.Id
                             select new CarDetailDto
                             {
                                 CarId = c.Id,
                                 CarName = c.Description,
                                 BrandName = b.Name,
                                 DailyPrice = c.DailyPrice,
                                 ColorName = co.Name,
                                 ModelYear = c.ModelYear
                             };
                return result.ToList();
            }
        }

        public CarDetailDto GetCarDetailsByCarId(int carId)
        {
            using (ReCapProjectContext context = new ReCapProjectContext())
            {
                var result = from c in context.Cars
                    join b in context.Brands on c.BrandId equals b.Id
                    join co in context.Colors on c.ColorId equals co.Id
                    where c.Id == carId
                    select new CarDetailDto
                    {
                        CarId = c.Id,
                        CarName = c.Description,
                        BrandName = b.Name,
                        DailyPrice = c.DailyPrice,
                        ColorName = co.Name,
                        ModelYear = c.ModelYear
                    };
                return result.SingleOrDefault();
            }
        }

        public List<CarDetailDto> GetCarDetailsByBrandId(int brandId)
        {
            using (ReCapProjectContext context = new ReCapProjectContext())
            {
                var result = from c in context.Cars
                             join b in context.Brands on c.BrandId equals b.Id
                             join co in context.Colors on c.ColorId equals co.Id
                             where c.BrandId == brandId
                             select new CarDetailDto
                             {
                                 CarId = c.Id,
                                 CarName = c.Description,
                                 BrandName = b.Name,
                                 DailyPrice = c.DailyPrice,
                                 ColorName = co.Name,
                                 ModelYear = c.ModelYear
                             };
                return result.ToList();
            }
        }

        public List<CarDetailDto> GetCarDetailsByColorId(int colorId)
        {
            using (ReCapProjectContext context = new ReCapProjectContext())
            {
                var result = from c in context.Cars
                             join b in context.Brands on c.BrandId equals b.Id
                             join co in context.Colors on c.ColorId equals co.Id
                             where c.ColorId == colorId
                             select new CarDetailDto
                             {
                                 CarId = c.Id,
                                 CarName = c.Description,
                                 BrandName = b.Name,
                                 DailyPrice = c.DailyPrice,
                                 ColorName = co.Name,
                                 ModelYear = c.ModelYear
                             };
                return result.ToList();
            }
        }

        public List<CarDetailDto> GetCarDetailsByColorAndBrandId(int colorId, int brandId)
        {
            using (ReCapProjectContext context = new ReCapProjectContext())
            {
                var result = from c in context.Cars
                             join b in context.Brands on c.BrandId equals b.Id
                             join co in context.Colors on c.ColorId equals co.Id
                             where c.ColorId == colorId && c.BrandId == brandId
                             select new CarDetailDto
                             {
                                 CarId = c.Id,
                                 CarName = c.Description,
                                 BrandName = b.Name,
                                 DailyPrice = c.DailyPrice,
                                 ColorName = co.Name,
                                 ModelYear = c.ModelYear
                             };
                return result.ToList();
            }
        }
    }
}
