using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Notlarim102.Entity
{
    [Table("tblCategories")]
    public class Category:MyEntityBase
    {
        [StringLength(50),Required]
        public string  Title { get; set; }

        [StringLength(50)]
        public string Description { get; set; }
        
        //foreign key 
        public virtual List<Note> Notes { get; set; }
        public Category()
        {
            Notes = new List<Note>(); // Note kısmı boş gelıyor. ilk bu çalışşsın sonra usttekiler çalışssn

        }
    }
}
