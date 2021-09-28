using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core.Utilities.Results;
using Entities.Concrete;

namespace Business.Abstract
{
    public interface ICreditCardService
    {
        IResult Add(CreditCard creditCard);
        IResult Update(CreditCard creditCard);
        IResult Delete(CreditCard creditCard);
        IDataResult<List<CreditCard>> GetAll();
        IDataResult<CreditCard> GetById(int id);
        IDataResult<List<CreditCard>> GetByUserId(int userId);
    }
}
