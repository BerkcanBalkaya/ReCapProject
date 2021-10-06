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

        public List<CarDetailDto> GetTopTenMostRentedCars()
        {
            using (ReCapProjectContext context = new ReCapProjectContext())
            {
                var topIds = (from id in (from rental in context.Rentals
                        join car in context.Cars on rental.CarId equals car.Id
                        group rental by car.Id
                        into grouping
                        orderby grouping.Count() descending
                        select new
                        {
                            Id =
                                grouping.Key
                        })
                    select id.Id).Take(10);

                var result = from car in context.Cars
                    join brand in context.Brands on car.BrandId equals brand.Id
                    join color in context.Colors on car.ColorId equals color.Id
                    join topSales in topIds on car.Id equals topSales
                    select new CarDetailDto
                    {
                        CarId = car.Id,
                        CarName = car.Description,
                        BrandName = brand.Name,
                        ColorName = color.Name,
                        DailyPrice = car.DailyPrice,
                        ModelYear = car.ModelYear
                    };
                return result.ToList();
            }
        }
    }
}
