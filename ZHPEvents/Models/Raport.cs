using System;
using System.ComponentModel.DataAnnotations;

namespace ZHPEvents.Models
{
    public class Raport
    {
        public int Id { get; set; }

        [Display(Name = "Tytu�")]
        [StringLength(60, MinimumLength = 3)]
        [Required(ErrorMessage = "Prosz� poda� tytu�.")]
        public string Title { get; set; }


        [Display(Name = "Czas dodania")]
        public DateTime AdditionTime { get; set; }

        [Display(Name = "Osoba dodaj�ca")]
        public string AddingPerson { get; set; }

        [Display(Name = "Osoba potwierdzaj�ca")]
        public string ConfirmingPerson { get; set; }

        public RaportStatus Status { get; set; }
    }

    public enum RaportStatus
    {
        Approved,
        Rejected
    }
}
