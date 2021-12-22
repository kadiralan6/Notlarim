using Notlarim102.Entity.Messages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Notlarim102.BusinessLayer
{
    public class BusinessLayerResult<T> where T : class
    {
        // public List<string> Errors { get; set; }

        //22,12/2
        //public List<KeyValuePair<ErrorMessageCode, string>> Errors { get; set; } bunu da kapadık
        //yeni kodu messagecode diye class oluşturduktan sonra yaptık //6. işten sonra
     public  List<ErrorMessageObject> Errors { get; set; }

       //
      //  public List<string> Errors { get; set; }
        public T Result { get; set; }

        public BusinessLayerResult()
        {
            // Errors = new List<string>();
            // 22,12 / 2 classdan sonra aşagıyı kapadık
            //  Errors = new List<KeyValuePair<ErrorMessageCode, string>>();
            Errors = new List<ErrorMessageObject>();
        }
        // 22,12 / 2
        public void AddError(ErrorMessageCode code,string message)
        {
           // Errors.Add(new KeyValuePair<ErrorMessageCode, string>(code, message));

            Errors.Add(new ErrorMessageObject { Code = code, Message = message });
        }
    }
}
