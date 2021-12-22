using Notlarim102.DataAccessLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Notlarim102.DataAccessLayer.EntityFramework
{

    public class RepositoryBase
    {
        //amaç contex yenileniyor reposite ile ya eğer biz null ise reposie newle yoksa newleme yapıyoz

        private static NotlarimContext _db; //tek bir nesne oluştur dağıt singelton olyor

        private static object _lockSync = new object(); // bunu birden fazla context oluşursa bunu yapıyoz

        public RepositoryBase()
        {

        }

        //aşağıdaki yapı tek bir tane context varsa 
        /*public static  NotlarimContext CreateContext()
        {
            if (_db == null)
            {
                _db = new NotlarimContext();
            }
            return _db;
        }*/


        // aşağıdaki yapı birden cok context yapı varsa oluyor
        public static NotlarimContext CreateContext()
        {
            if (_db == null)
            {
                lock (_lockSync)
                {
                    if (_db == null)
                    {
                        _db = new NotlarimContext();
                    }
                }

            }
            return _db;
        }
    }
}
