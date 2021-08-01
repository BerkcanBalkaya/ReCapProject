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
    public class ColorManager:IColorService
    {
        IColorDal _iColorDal;

        public ColorManager(IColorDal iColorDal)
        {
            _iColorDal = iColorDal;
        }

        public void Add(Color color)
        {
            _iColorDal.Add(color);
        }

        public void Update(Color color)
        {
            _iColorDal.Update(color);
        }

        public void Delete(Color color)
        {
            _iColorDal.Delete(color);
        }

        public List<Color> GetAll()
        {
            return _iColorDal.GetAll();
        }
    }
}
