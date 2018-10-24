using System;
using System.ComponentModel.DataAnnotations;

namespace ZHPEvents.Models
{
    public class Event
    {
        public int Id { get; set; }

        [Display(Name = "Tytu³")]
        [StringLength(60, MinimumLength = 3)]
        [Required(ErrorMessage ="Proszê podaæ tytu³.")]
        public string Tittle { get; set; }

        //[Display(Name = "Kategoria")]
        //[Required(ErrorMessage = "Proszê podaæ pierwsz¹ kategoriê.")]
        //public EventType Type { get; set; }

        //[Display(Name = "Pod kategoria")]
        //[Required(ErrorMessage = "Proszê podaæ drug¹ kategoriê.")]
        //public EventCategory Category { get; set; }

        //[Display(Name = "Data wydarzenia")]
        //[Required(ErrorMessage = "Proszê podaæ datê wydarzenia.")]
        //[DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        //public DateTime Date { get; set; }

        //[Display(Name = "Data zapisów")]
        //[Required(ErrorMessage = "Proszê podaæ datê koñca zapisów.")]
        //[DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        //public DateTime LastDayOfEntries { get; set; }

        //[Display(Name = "Opis")]
        //[Required(ErrorMessage = "Proszê podaæ opis.")]
        //[StringLength(60, MinimumLength = 3)]
        //public string Description { get; set; }

        //[Display(Name = "Liczba miejsc")]
        //[Range(0, 999)]
        //[Required(ErrorMessage = "Proszê podaæ liczbe miejsc.")]
        //public int NumberOfSeats { get; set; }

        //[Display(Name = "Kontakt")]
        //[Required(ErrorMessage = "Proszê podaæ mail kontaktowy.")]
        //public string Contact { get; set; }

        //[Display(Name = "Mail rejestracji")]
        //[Required(ErrorMessage = "Proszê podaæ mail rejestarcji.")]
        //public string RegistrationMail { get; set; }

        //[Display(Name = "Organizator")]
        //[Required(ErrorMessage = "Proszê podaæ osobê/ jednostkê, która orgfanizuje wydarzenie.")]
        //public string Organizaer { get; set; }

        //[Display(Name = "Dla kogo")]
        //[Required(ErrorMessage ="Proszê podaæ dla kogo jest przeznaczone wydarzezenie")]
        //public string ForWhom { get; set; }

        //[Display(Name = "Miniaturka")]
        //public string Thumbnail { get; set; }

        //[Display(Name = "Plakat")]
        //public string Poster { get; set; }

        [Display(Name = "Czas dodania")]
        public DateTime AdditionTime { get; set; }

        [Display(Name = "Osoba dodaj¹ca")]
        public string AddingPerson { get; set; }

        [Display(Name = "Osoba potwierdzaj¹ca")]
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
        [Display(Name = "Obóz")]
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
        [Display(Name = "Dru¿ynowych Zuchowych")]
        druzynowychZuchowych
    }
}