using Notlarim102.Common.Helper;
using Notlarim102.DataAccessLayer.EntityFramework;
using Notlarim102.Entity;
using Notlarim102.Entity.ValueObject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Notlarim102.BusinessLayer
{
    public class NotlarimUserManager
    {
        Repository<NotlarimUser> ruser = new Repository<NotlarimUser>();

        public BusinessLayerResult<NotlarimUser> RegisterUser(RegisterViewModel data)
        {
            NotlarimUser user = ruser.Find(s => s.Username == data.UserName || s.Email == data.Email);
            BusinessLayerResult<NotlarimUser> layerResult = new BusinessLayerResult<NotlarimUser>();

            //21,12//3

            if (user != null)
            {
                if (user.Username == data.UserName)
                {       //dün alttakı kod du 22 sinde alltaki kod

                    // layerResult.Errors.Add("Kullanici Adi  daha önce kayit edilmiş.");
                    //22,12//3
                    layerResult.AddError(Entity.Messages.ErrorMessageCode.UsernameAlreadyExist, "Kullanici Adi  daha önce kayit edilmiş.");
                }
                if (user.Email == data.Email)
                {
                    // layerResult.Errors.Add("E mail daha önce kullanilmiş.");
                    //22,12//3
                    layerResult.AddError(Entity.Messages.ErrorMessageCode.EmailAlreadyExist, "Email daha önce kullanilmiş.");
                    //3
                }                
                // throw new Exception("Bu bilgiler daha önce kullanılmış.");
            }
            else
            {
                DateTime now = DateTime.Now;
                int dbResult = ruser.Insert(new NotlarimUser()
                {
                    Username = data.UserName,
                    Email = data.Email,
                    Password = data.Password,
                    ActivateGuid = Guid.NewGuid(),
                    isActive = false,
                    IsAdmin = false,
                    //kapatılanlar22.12 de kapandı. Repositoryde otomatik eklenecek orda düzenliyeceğiz.
                    /* ModifiedOn=now,
                       CreatedOn=now,
                       ModifiedUserName="system"*/
                });

                if (dbResult > 0)
                {
                    layerResult.Result = ruser.Find(s => s.Email == data.Email && s.Username == data.UserName);

                    // 22,12- 18
                    string siteUri = ConfigHelper.Get<string>("SiteRootUri");
                    string activateUri = $"{siteUri}/Home/UserActive/{layerResult.Result.ActivateGuid}";
                    string body = $"Merhaba{layerResult.Result.Username};<br> <br> Hesabinizi Aktifleştirmek için <a href='{activateUri}' targe='_blank'> Tıklayın </a>.";
                    MailHelper.SendMail(body, layerResult.Result.Email, "Notlarim102 hesap aktifleştirme");
                }
            }

            return layerResult;
        }
        //21/12/ 5 ara başkayınca ilk
        public BusinessLayerResult<NotlarimUser> LoginUser(LoginViewModel data)
        {
            //giriş kontrol,  hesap aktif mi,
            //View de yapılcak yonlendirme , Session a kullanici bilgilerini gonderme


            BusinessLayerResult<NotlarimUser> res = new BusinessLayerResult<NotlarimUser>();

            res.Result = ruser.Find(s => s.Username == data.Username && s.Password == data.Password);

            if (res.Result != null)
            {
                if (!res.Result.isActive)
                {
                    res.AddError(
                        Entity.Messages.ErrorMessageCode.UserIsNotActive, "Kullanici Aktifleştirilmemiş.");
                    res.AddError(Entity.Messages.ErrorMessageCode.CheckYourEmail, " Lütfen mailinizi kontrol ediniz.");
                }
            }
            else
            {
                // res.Errors.Add("Kullanici adi veya şifre yanliş.");
                //22.12/ 6
                res.AddError(Entity.Messages.ErrorMessageCode.UsernameOrPassWrong, "Kullanici adi veya şifre yanliş.");
            }
            return res;

        }

        // 22,12- 19 -- home contorlede 20
        public BusinessLayerResult<NotlarimUser> ActivateUser(Guid id)
        {
            BusinessLayerResult<NotlarimUser> res = new BusinessLayerResult<NotlarimUser>();
            res.Result = ruser.Find(x => x.ActivateGuid == id);
            if (res.Result!=null)
            {
                if (res.Result.isActive)
                {
                    res.AddError(Entity.Messages.ErrorMessageCode.UserAlreadyActive, "Bu Hesap daha once aktif edilmiştir");
                    return res;
                }
                res.Result.isActive = true;
                ruser.Update(res.Result);
            }
            else
            {
                res.AddError(Entity.Messages.ErrorMessageCode.ActivateIdDoesNotExist, "Siteyi raht birak");
            }
            return res;

          }
    }
}
