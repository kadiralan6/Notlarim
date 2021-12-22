using Notlarim102.DataAccessLayer;
using Notlarim102.DataAccessLayer.EntityFramework;
using Notlarim102.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Notlarim102.BusinessLayer
{
    
    // buraya eklemelerden sonra geldik

    public class Test
    { // 14/12//3
        //Test amaçlı oldugu için hepsini yazdık
        Repository<NotlarimUser> ruser = new Repository<NotlarimUser>();
        Repository<Category> rcat = new Repository<Category>();
        Repository<Note> rnote = new Repository<Note>();
        Repository<Comment> rcom = new Repository<Comment>();
        Repository<Liked> rlike = new Repository<Liked>();
        public Test()
        {
            NotlarimContext db = new NotlarimContext();
            db.Categories.ToList();
            // db.Database.CreateIfNotExists();//bu çalişisrsa seed data çalışmıyor

            var test = rcat.List();
            var test1 = rcat.List(x=>x.Id>5);

        }
            
        public void InsertTest()
        {
            int result = ruser.Insert(new NotlarimUser()
            {
                Name="Çetin",
                Surname="Taş",
                Email="aseqwe@gmail.com",
                ActivateGuid=Guid.NewGuid(),
                isActive=true,
                IsAdmin=false,
                Username="abutar",
                Password="123",
                CreatedOn=DateTime.Now,
                ModifiedOn=DateTime.Now,
                ModifiedUserName="abutar"

            });
        }
        public void UpdateTest()
        {
            NotlarimUser user = ruser.Find(x => x.Username == "abutar");
            if (user != null)
            {
                user.Password = "1111111";
                ruser.Update(user);
            }
        }
        public void DeleteTest()
        {
            NotlarimUser user = ruser.Find(x => x.Username == "abutar");

            if (user != null)
            {
                ruser.Delete(user);

            }
        }
        public void CommentTest()
        {
            NotlarimUser user = ruser.Find(s => s.Id == 1);
            Note note = rnote.Find(s => s.Id == 3);
            Comment comment = new Comment()
            {
                Text="bu bir test datasidir",
                CreatedOn=DateTime.Now,
                ModifiedOn=DateTime.Now,
                ModifiedUserName="kadirala",
                Note=note,
                Owner=user,
            };
            rcom.Insert(comment);
        }
    }
}
