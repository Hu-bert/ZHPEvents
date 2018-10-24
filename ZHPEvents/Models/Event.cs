using System;
using System.ComponentModel.DataAnnotations;

namespace ZHPEvents.Models
{
    public class Event
    {
        public int Id { get; set; }

        [Display(Name = "Tytu�")]
        [StringLength(60, MinimumLength = 3)]
        [Required(ErrorMessage ="Prosz� poda� tytu�.")]
        public string Tittle { get; set; }

        //[Display(Name = "Kategoria")]
        //[Required(ErrorMessage = "Prosz� poda� pierwsz� kategori�.")]
        //public EventType Type { get; set; }

        //[Display(Name = "Pod kategoria")]
        //[Required(ErrorMessage = "Prosz� poda� drug� kategori�.")]
        //public EventCategory Category { get; set; }

        //[Display(Name = "Data wydarzenia")]
        //[Required(ErrorMessage = "Prosz� poda� dat� wydarzenia.")]
        //[DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        //public DateTime Date { get; set; }

        //[Display(Name = "Data zapis�w")]
        //[Required(ErrorMessage = "Prosz� poda� dat� ko�ca zapis�w.")]
        //[DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        //public DateTime LastDayOfEntries { get; set; }

        //[Display(Name = "Opis")]
        //[Required(ErrorMessage = "Prosz� poda� opis.")]
        //[StringLength(60, MinimumLength = 3)]
        //public string Description { get; set; }

        //[Display(Name = "Liczba miejsc")]
        //[Range(0, 999)]
        //[Required(ErrorMessage = "Prosz� poda� liczbe miejsc.")]
        //public int NumberOfSeats { get; set; }

        //[Display(Name = "Kontakt")]
        //[Required(ErrorMessage = "Prosz� poda� mail kontaktowy.")]
        //public string Contact { get; set; }

        //[Display(Name = "Mail rejestracji")]
        //[Required(ErrorMessage = "Prosz� poda� mail rejestarcji.")]
        //public string RegistrationMail { get; set; }

        //[Display(Name = "Organizator")]
        //[Required(ErrorMessage = "Prosz� poda� osob�/ jednostk�, kt�ra orgfanizuje wydarzenie.")]
        //public string Organizaer { get; set; }

        //[Display(Name = "Dla kogo")]
        //[Required(ErrorMessage ="Prosz� poda� dla kogo jest przeznaczone wydarzezenie")]
        //public string ForWhom { get; set; }

        //[Display(Name = "Miniaturka")]
        //public string Thumbnail { get; set; }

        //[Display(Name = "Plakat")]
        //public string Poster { get; set; }

        [Display(Name = "Czas dodania")]
        public DateTime AdditionTime { get; set; }

        [Display(Name = "Osoba dodaj�ca")]
        public string AddingPerson { get; set; }

        [Display(Name = "Osoba potwierdzaj�ca")]
        public string ConfirmingPerson { get; set; }

        public EventStatus Status { get; set; }
    }

    public enum EventStatus
    {
        Approved,
        Rejected
    }

    public enum EventType
    {
        [Display(Name = "Ob�z")]
        oboz,
        [Display(Name = "Kurs")]
        kurs,
        [Display(Name = "Warsztat")]
        warsztat,
        [Display(Name = "Seminarium")]
        seminaria,
        [Display(Name = "Biwak")]
        biwak,
        [Display(Name = "Rajd")]
        rajd
    }

    public enum EventCategory
    {
        [Display(Name = "Przewodnikowski")]
        przewodnikowski,
        [Display(Name = "Dru�ynowych Zuchowych")]
        druzynowychZuchowych
    }
}