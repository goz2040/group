﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace GroupProject.Models
{
    public class Preference
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        public bool IsLiked { get; set; }

        #region NavigationProperties

        #region A User who express a preference
        public string UserId { get; set; }

        public ApplicationUser User { get; set; }
        #endregion

        #region The ArtWork which is preferred by a User
        public int ArtWorkId { get; set; }

        public ArtWork ArtWork { get; set; }
        #endregion

        #endregion 
    }
}