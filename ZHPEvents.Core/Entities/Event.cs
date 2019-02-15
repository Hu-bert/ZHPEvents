using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using ZHPEvents.Core.Identity;

namespace ZHPEvents.Core.Entities
{
    public class Event
    {
        public int Id { get; set; }

        [Display(Name = "Tytuł")]
        [StringLength(60, MinimumLength = 3)]
        [Required(ErrorMessage = "Proszę podać tytuł.")]
        public string Title { get; set; }

        [Display(Name = "Kategoria")]
        //[Required(ErrorMessage = "Proszę podać pierwszą kategorię.")]
        public EventType Type { get; set; }

        [Display(Name = "Pod kategoria")]
        //[Required(ErrorMessage = "Proszę podać drugą kategorię.")]
        public EventCategory Category { get; set; }

        [Display(Name = "Data wydarzenia")]
        //[Required(ErrorMessage = "Proszę podać datę wydarzenia.")]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime Date { get; set; }

        [Display(Name = "Data zapisów")]
        //[Required(ErrorMessage = "Proszę podać datę końca zapisów.")]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime LastDayOfEntries { get; set; }

        [Display(Name = "Opis")]
        //[Required(ErrorMessage = "Proszę podać opis.")]
        [StringLength(500, MinimumLength = 3)]
        public string Description { get; set; }

        [Display(Name = "Miejsce wydarzenia")]
        public string Place { get; set; }

        [Display(Name = "Liczba miejsc")]
        [Range(0, 999)]
        //[Required(ErrorMessage = "Proszę podać liczbe miejsc.")]
        public int NumberOfSeats { get; set; }

        [Display(Name = "Kontakt")]
        //[Required(ErrorMessage = "Proszę podać mail kontaktowy.")]
        public string Contact { get; set; }

        [Display(Name = "Mail rejestracji")]
        //[Required(ErrorMessage = "Proszę podać mail rejestarcji.")]
        public string RegistrationMail { get; set; }

        [Display(Name = "Organizator")]
        //[Required(ErrorMessage = "Proszę podać osobę/ jednostkę, która orgfanizuje wydarzenie.")]
        public string Organizaer { get; set; }

        [Display(Name = "Dla kogo")]
        //[Required(ErrorMessage = "Proszę podać dla kogo jest przeznaczone wydarzezenie")]
        public string ForWhom { get; set; }

        [Display(Name = "Miniaturka")]
        public string Thumbnail { get; set; }

        [Display(Name = "Plakat")]
        public string Poster { get; set; }

        [Display(Name = "Czas dodania")]
        public DateTime AdditionTime { get; set; }

        [Display(Name = "Osoba dodająca")]
        public string AddingPersonId { get; set; }

        [Display(Name = "Osoba potwierdzająca")]
        public string ConfirmingPersonId { get; set; }

        public EventStatus Status { get; set; }

        public Deleted IsDeleted { get; set; }

        #region Relations

        [ForeignKey("AddingPersonId")]
        public AppUser AddingPerson { get; set; }

        [ForeignKey("ConfirmingPersonId")]
        public AppUser ConfirmingPerson { get; set; }

        #endregion
    }

    public enum Deleted
    {
        No,
        Yes
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
        [Display(Name = "Drużynowych Zuchowych")]
        druzynowychZuchowych
    }
}
