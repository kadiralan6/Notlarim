using Notlarim102.Entity;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Notlarim102.DataAccessLayer.EntityFramework
{
    //bunu Context classı oluşturduktan sonra oluşturduk
    public class MyInitializer : CreateDatabaseIfNotExists<NotlarimContext>
    {

        protected override void Seed(NotlarimContext context)
        {
            //Adding admin user..
            NotlarimUser admin = new NotlarimUser()
            {
                Name = "Abdulkadir",
                Surname = "Alan",
                Email = "bzkbey@gmail.com",
                ActivateGuid = Guid.NewGuid(),
                isActive = true,
                IsAdmin = true,
                Username = "kadiralan",
                Password = "123456789",
                CreatedOn = DateTime.Now,
                ModifiedOn = DateTime.Now,
                ModifiedUserName = "kadiralan"
            };

            NotlarimUser standartUser = new NotlarimUser()
            {
                Name = "Candan",
                Surname = "Alan",
                Email = "candan@gmail.com",
                ActivateGuid = Guid.NewGuid(),
                isActive = true,
                IsAdmin = false,
                Username = "candanalan",
                Password = "123456789123",
                CreatedOn = DateTime.Now.AddHours(1),
                ModifiedOn = DateTime.Now.AddMinutes(65),
                ModifiedUserName = "kadiralan"
            };
            context.NotlarimUsers.Add(admin);
            context.NotlarimUsers.Add(standartUser);

            for (int i = 0; i < 8; i++)
            {
                NotlarimUser user = new NotlarimUser()
                {
                    Name = FakeData.NameData.GetFirstName(),
                    Surname = FakeData.NameData.GetSurname(),
                    Email = FakeData.NetworkData.GetEmail(),
                    ActivateGuid = Guid.NewGuid(),
                    isActive = true,
                    IsAdmin = false,
                    Username = $"user-{i}", //süslü parantezdek i yazabilmek için dolar yazdık
                    Password = "123",
                    CreatedOn = FakeData.DateTimeData.GetDatetime(DateTime.Now.AddYears(-1), DateTime.Now),
                    ModifiedOn = FakeData.DateTimeData.GetDatetime(DateTime.Now.AddMinutes(-1), DateTime.Now),
                    ModifiedUserName = "kadiralan"
                };
                context.NotlarimUsers.Add(user);

            }
            context.SaveChanges();

            //User list for using

            List<NotlarimUser> userList = context.NotlarimUsers.ToList();

            //adding fake category
            for (int i = 0; i < 10; i++)
            {
                Category cat = new Category()
                {
                    Title = FakeData.PlaceData.GetStreetName(),
                    Description = FakeData.PlaceData.GetAddress(),
                    CreatedOn = DateTime.Now,
                    ModifiedOn = DateTime.Now,
                    ModifiedUserName = "kadiralan"
                };

                context.Categories.Add(cat); //categories notlarımcontext te tanımlı

                //adding fake Notes..
                for (int k = 0; k < FakeData.NumberData.GetNumber(5, 9); k++)
                {
                    NotlarimUser owner = userList[FakeData.NumberData.GetNumber(0, userList.Count - 1)];
                    Note note = new Note()
                    {
                        Title = FakeData.TextData.GetAlphabetical(FakeData.NumberData.GetNumber(5, 25)),
                        Text = FakeData.TextData.GetSentences(FakeData.NumberData.GetNumber(3,5)),
                        IsDraft = false,
                        LikeCount = FakeData.NumberData.GetNumber(1, 9),
                        Owner = owner,
                        CreatedOn = FakeData.DateTimeData.GetDatetime(DateTime.Now.AddYears(-1), DateTime.Now),
                        ModifiedOn = FakeData.DateTimeData.GetDatetime(DateTime.Now.AddYears(-1), DateTime.Now),
                        ModifiedUserName = owner.Username
                    };
                    cat.Notes.Add(note);

                    //Adding Fake commetns
                    for (int j = 0; j < FakeData.NumberData.GetNumber(3, 5); j++)
                    {
                        NotlarimUser comment_owner = userList[FakeData.NumberData.GetNumber(0, userList.Count - 1)];
                        Comment comment = new Comment()
                        {
                            Text = FakeData.TextData.GetSentence(),
                            Owner = comment_owner,
                            CreatedOn = FakeData.DateTimeData.GetDatetime(DateTime.Now.AddYears(-1), DateTime.Now),
                            ModifiedOn = FakeData.DateTimeData.GetDatetime(DateTime.Now.AddYears(-1), DateTime.Now),
                            ModifiedUserName = comment_owner.Username

                        };

                        note.Comments.Add(comment); //note içine comment ekledık
                    }
                    //Adding fake likes
                    for (int m = 0; m < note.LikeCount; m++)
                    {
                        Liked liked = new Liked()
                        {
                            LikedUser = userList[m]
                        };
                        note.Likes.Add(liked);
                    }

                }

            }
            context.SaveChanges();
        }
    }
}
