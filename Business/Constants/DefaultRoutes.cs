using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Constants
{
    public class DefaultRoutes
    {
        public static string DefaultImageFolder = $"{Directory.GetParent(Directory.GetCurrentDirectory())}\\WebAPI\\wwwroot\\images\\";
        public static string DefaultImage = $"{DefaultImageFolder}defaultLogo.jpg";
    }
}
