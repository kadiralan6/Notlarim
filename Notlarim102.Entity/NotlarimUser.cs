﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Notlarim102.Entity
{
    [Table("tblNotlarimUsers")]
    public class NotlarimUser:MyEntityBase
    {   
        [StringLength(30)]
        public string Name { get; set; }

        [StringLength(30)]
        public string Surname { get; set; }

        [StringLength(30)]
        public string Username { get; set; }

        [StringLength(100),Required]
        public string Email { get; set; }

        [StringLength(100),Required]
        public string Password { get; set; }
        public bool isActive { get; set; }

        [Required]
        public Guid ActivateGuid { get; set; } //global user ıd 16 haneli benzersiz bir ıd cıkartıyor
        public bool  IsAdmin { get; set; }

        public virtual List<Note>   Notes { get; set; }
        public virtual List<Comment> Comments { get; set; }
        public virtual List<Liked> Likes { get; set; }
    }
}
