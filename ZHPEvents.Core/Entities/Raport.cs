using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace ZHPEvents.Core.Entities
{
    public class Raport
    {
        public int Id { get; set; }

        [Display(Name = "Tytuł")]
        [StringLength(60, MinimumLength = 3)]
        [Required(ErrorMessage = "Proszę podać tytuł.")]
        public string Title { get; set; }


        [Display(Name = "Czas dodania")]
        public DateTime AdditionTime { get; set; }

        [Display(Name = "Osoba dodająca")]
        public string AddingPerson { get; set; }

        [Display(Name = "Osoba potwierdzająca")]
        public string ConfirmingPerson { get; set; }

        public RaportStatus Status { get; set; }
    }

    public enum RaportStatus
    {
        Approved,
        Rejected
    }
}

