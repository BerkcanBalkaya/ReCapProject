using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entities.Concrete;

namespace Business.Abstract
{
    interface IColorService
    {
        void Add(Color color);
        void Update(Color color);
        void Delete(Color color);

        List<Color> GetAll();
    }
}
