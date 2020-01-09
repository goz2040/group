﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace GroupProject.Models
{
    public class Commission
    {
        [key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        #region Fields Data

        [Required]
        public double Price { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:d/M/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime DateOfCommission { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:d/M/yyyy}", ApplyFormatInEditMode = true)]
        [FutureDate]
        public DateTime Deadline { get; set; }

        #endregion

        #region Navigation Properties

        #region A User that make a Commission
        public string UserId { get; set; }

        public virtual ApplicationUser User { get; set; }
        #endregion

        #region An Artist that accept the Commission
        public string ArtistId { get; set; }

        public virtual ApplicationUser Artist { get; set; }
        #endregion

        #endregion

    }
}