
using Notlarim102.DataAccessLayer;
using Notlarim102.Entity;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Notlarim102.DataAccessLayer.EntityFramework
{
    // 14/12//2
    //RepositoryBase miras alma oluşturdukdtan sonra oldu. saat6.30 da
    public class Repository<T>:RepositoryBase,IRepository<T> where T :  class
    {
        NotlarimContext db;
        // 14/12//4
        private DbSet<T> objSet;
        public Repository() //constructer yapttık ki objSet ilk dolsunda aşağıdakiler boş kalmasın diye yaptıık.
        {
                db = RepositoryBase.CreateContext();
                objSet= db.Set<T>();
        }


        public List<T> List()
        {
            return objSet.ToList();
        }

        //koşullur listeleme için//// expressin linq.exp
        public List<T> List(Expression<Func<T,bool>> eresult )
        {
            return objSet.Where(eresult).ToList(); //bu ifadeye bir bak c# func diye
            //db.Categories.Where(x => x.Id == 5).ToList(); yazıyorduk üsttekileri tanımlayınca
            //x => x.Id == 5  eresult a eşit
        }

        public int Insert(T obj)
        {
            objSet.Add(obj);
            // 22/12//9
            if (obj is MyEntityBase) //obj nin içiersindeki veriler myentitiy de varmı
            {
                MyEntityBase o = obj as MyEntityBase; // entitybase ile eşleşenleri al
                DateTime now = DateTime.Now;
                o.CreatedOn = now;
                o.ModifiedOn = now;
                o.ModifiedUserName = "system";

            }

           /* ModifiedOn = now,
            CreatedOn = now,
            ModifiedUserName = "system"*/
            return Save();  //biz burda her biri için db.SaveChanges yazmaktansa bunu yazduk
        }
        public int Update(T obj)
        { // 22/12//10
            if (obj is MyEntityBase) //obj nin içiersindeki veriler myentitiy de varmı
            {
                MyEntityBase o = obj as MyEntityBase; // entitybase ile eşleşenleri al
               
                o.ModifiedOn = DateTime.Now;
                o.ModifiedUserName = "system";

            }
            return Save();
        }
        public int Delete(T obj)
        {
            objSet.Remove(obj);
            return Save();
        }

            public T Find(Expression<Func<T,bool>> eresult)
            {
                return objSet.FirstOrDefault(eresult);
            }

        public int Save()
        {
            return db.SaveChanges();
        }

        public IQueryable<T> listQueryable()
        {
            return objSet.AsQueryable<T>();
        }
    }
}
