using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using ZHPEvents.ViewModels.Account;

namespace ZHPEvents.ViewModels.Events
{
    public class IndexViewModel
    {
        public int Id { get; set; }

        [Display(Name = "Tytuł")]
        public string Title { get; set; }


        [Display(Name = "Czas dodania")]
        public DateTime AdditionTime { get; set; }

        [Display(Name = "Osoba dodająca")]
        public virtual ProfileViewModel AddingPerson { get; set; }

        [Display(Name = "Osoba potwierdzająca")]
        public virtual ProfileViewModel ConfirmingPerson { get; set; }

        public EventStatus Status { get; set; }

    }
    public enum EventStatus
    {
        Approved,
        Rejected
    }
}
