using System;
using Business.Concrete;
using DataAccess.Concrete.EntityFramework;
using DataAccess.Concrete.InMemory;
using Entities.Concrete;

namespace Console
{
    class Program
    {
        static void Main(string[] args)
        {
            BrandManager brandManager = new BrandManager(new EfBrandDal());
            ColorManager colorManager = new ColorManager(new EfColorDal());
            CarManager carManager = new CarManager(new EfCarDal());

            //brandManager.Add(new Brand{Name = "Volvo"});
            //colorManager.Add(new Color{Name = "Blue"});
            carManager.Add(new Car{BrandId = 1,ColorId = 1,DailyPrice = 250,Description = "l",ModelYear = 1998});

            //foreach (var car in carManager.GetAll())
            //{
            //    System.Console.WriteLine(car.Description);
            //}

        }
    }
}
