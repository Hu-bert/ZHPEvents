using System;
using System.ComponentModel.DataAnnotations;

namespace ZHPEvents.Models
{
    public class Raport
    {
        public int Id { get; set; }

        [Display(Name = "Tytu³")]
        [StringLength(60, MinimumLength = 3)]
        [Required(ErrorMessage = "Proszê podaæ tytu³.")]
        public string Title { get; set; }


        [Display(Name = "Czas dodania")]
        public DateTime AdditionTime { get; set; }

        [Display(Name = "Osoba dodaj¹ca")]
        public string AddingPerson { get; set; }

        [Display(Name = "Osoba potwierdzaj¹ca")]
        public string ConfirmingPerson { get; set; }

        public RaportStatus Status { get; set; }
    }

    public enum RaportStatus
    {
        Approved,
        Rejected
    }
}
