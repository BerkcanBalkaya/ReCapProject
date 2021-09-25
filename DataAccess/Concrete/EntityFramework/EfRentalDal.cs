using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core.DataAccess.EntityFramework;
using DataAccess.Abstract;
using Entities.Concrete;
using Entities.DTOs;

namespace DataAccess.Concrete.EntityFramework
{
    public class EfRentalDal:EfEntityRepositoryBase<Rental,ReCapProjectContext>,IRentalDal
    {
        public List<RentalDetailDto> GetRentalDetails()
        {
            using (ReCapProjectContext context = new ReCapProjectContext())
            {
                var result = from r in context.Rentals
                    join c in context.Cars on r.CarId equals c.Id
                    join b in context.Brands on c.BrandId equals b.Id
                    join cu in context.Customers on  r.CustomerId equals cu.Id 
                    join u in context.Users on cu.Id equals u.Id
                    select new RentalDetailDto()
                    {
                        Id = r.Id,
                        BrandName = b.Name,
                        CustomerFullName = CapitalizeFirstLetter(u.FirstName) +" "+ CapitalizeFirstLetter(u.LastName),
                        RentDate = r.RentDate,
                        ReturnDate = r.ReturnDate
                    };
                return result.ToList();
            }
        }
        private static string CapitalizeFirstLetter(string s)
        {
            if (String.IsNullOrEmpty(s))
                return s;
            if (s.Length == 1)
                return s.ToUpper();
            return s.Remove(1).ToUpper() + s.Substring(1);
        }
    }
}
