using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Notlarim102.Common.Helper
{
    //22,12- 14
    public class ConfigHelper
    {
        //22,12- 16. web configden sonra geldik

      /*  public static string Get (string Key)
        {
            return ConfigurationManager.AppSettings[Key];
        }*/

        public static T Get<T>(string key)
        {
            //casting işlemi araştrıs
            return (T)Convert.ChangeType(ConfigurationManager.AppSettings[key],typeof(T));
        }

    }
}
