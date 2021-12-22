﻿using Notlarim102.DataAccessLayer;
using Notlarim102.DataAccessLayer.EntityFramework;
using Notlarim102.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

//14.12// 1 
//class yazmasında,, referans typlar newlenirler
namespace Notlarim102.BusinessLayer
{
    public class CategoryControl
    {
        NotlarimContext db = new NotlarimContext();

        public void Insert(Category cat)
        {
            db.Categories.Add(cat);
            db.SaveChanges();
        }
    }

}
