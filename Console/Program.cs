using Business.Concrete;
using DataAccess.Concrete.EntityFramework;
using Entities.Concrete;
using System;
using Business.Abstract;

namespace ConsoleUI
{
    class Program
    {
        static void Main(string[] args)
        {
            BrandManager brandManager = new BrandManager(new EfBrandDal());
            ColorManager colorManager = new ColorManager(new EfColorDal());
            CarManager carManager = new CarManager(new EfCarDal());

            Console.WriteLine("\n--------------Brand actions---------\n");

            brandManager.Add(new Brand { Name = "Opel" });
            foreach (var brand in brandManager.GetAll())
            {
                Console.WriteLine(brand.Name);
            }
            Console.WriteLine(brandManager.GetById(5).Name);
            brandManager.Update(new Brand { Id = 5, Name = "Toyota" });
            Console.WriteLine(brandManager.GetById(5).Name);
            brandManager.Delete(new Brand { Id = 5 });

            Console.WriteLine("\n--------------Color actions---------\n");

            colorManager.Add(new Color { Name = "Red" });
            foreach (var color in colorManager.GetAll())
            {
                Console.WriteLine(color.Name);
            }
            Console.WriteLine(colorManager.GetById(1));
            colorManager.Update(new Color { Id = 1, Name = "Red" });
            Console.WriteLine(colorManager.GetById(1).Name);
            colorManager.Delete(new Color { Id = 1 });

            Console.WriteLine("\n--------------Car actions---------\n");

            carManager.Add(new Car { BrandId = 1, ColorId = 1, DailyPrice = 250, Description = "l", ModelYear = 1998 });
            foreach (var car in carManager.GetAll())
            {
                Console.WriteLine(car.Description);
            }

            Console.WriteLine(carManager.GetById(1));
            carManager.Update(new Car { BrandId = 1, ColorId = 2, DailyPrice = 500, Description = "Supra", ModelYear = 2009 });
            Console.WriteLine(carManager.GetById(1).Description);
            carManager.Delete(new Car { BrandId = 1 });


            Console.WriteLine("\n--------------CarDetailDto actions-----------\n");
            foreach (var cars in carManager.GetCarDetails())
            {
                Console.WriteLine("{0}  {1}  {2}  {3}", cars.CarName, cars.BrandName, cars.ColorName, cars.DailyPrice);
            }


            //foreach (var car in carManager.GetAll())
            //{
            //    System.Console.WriteLine(car.Description);
            //}

        }
    }
}
