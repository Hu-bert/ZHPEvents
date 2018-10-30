using Microsoft.AspNetCore.Identity;
using System;
using System.ComponentModel.DataAnnotations;

namespace ZHPEvents.Core.Identity
{
    public class AppUser : IdentityUser
    {
        [PersonalData]
        public string FristName { get; set; }
        [PersonalData]
        public string LastName { get; set; }
        [PersonalData]
        public StopinieHarcerskie StopienHarcerski { get; set; }
        [PersonalData]
        public StopnieInstruktorskie StopienInstruktorski { get; set; }
        [PersonalData]
        public Hufce Hufiec { get; set; }
        [PersonalData]
        public Choragwie Choragiew { get; set; }
    }

    public enum StopinieHarcerskie
    {
        [Display(Name = "Nie dotyczy")]
        none,
        [Display(Name = "mł,")]
        ml,
        [Display(Name = "och.")]
        och,
        [Display(Name = "wyw.")]
        wyw,
        [Display(Name = "trop.")]
        trop,
        [Display(Name = "odkr.")]
        odkr,
        [Display(Name = "pion.")]
        pion,
        [Display(Name = "ćw.")]
        ćw,
        [Display(Name = "sam.")]
        sam,
        [Display(Name = "HO.")]
        HO,
        [Display(Name = "HR.")]
        HR
    }

    public enum StopnieInstruktorskie
    {
        [Display(Name = "Nie dotyczy")]
        none,
        [Display(Name = "pwd.")]
        pwd,
        [Display(Name = "phm.")]
        phm,
        [Display(Name = "hm.")]
        hm
    }
    public enum Choragwie
    {
        [Display(Name = "Nie dotyczy")]
        none,
        [Display(Name = "Białostocka")]
        Bialostocka,
        [Display(Name = "Dolnoslaska")]
        Dolnośląska,
        [Display(Name = "Gdanska")]
        Gdanska,
        [Display(Name = "Kielecka")]
        Kielecka,
        [Display(Name = "Krakowska")]
        Krakowska,
        [Display(Name = "Kujawsko Pomorska")]
        KujawskoPomorska,
        [Display(Name = "Lubelska")]
        Lubelska,
        [Display(Name = "Łódzka")]
        Lodzka,
        [Display(Name = "Mazowiecka")]
        Mazowiecka,
        [Display(Name = "Opolska")]
        Opolska,
        [Display(Name = "Podkarpacka")]
        Podkarpacka,
        [Display(Name = "Stołeczna")]
        Stoleczna,
        [Display(Name = "Śląska")]
        Slaska,
        [Display(Name = "Warmińsko Mazurska")]
        WarminskoMazurska,
        [Display(Name = "Wilekopolska")]
        Wilekopolska,
        [Display(Name = "Zachodnio Pomorska")]
        ZachodnioPomorska,
        [Display(Name = "Ziemi Lubuskiej")]
        ZiemiLubuskiej
    }
    public enum Hufce
    {
        [Display(Name = "Nie dotyczy")]
        none,
        [Display(Name = "Oleśnica")]
        Olesnica,
        [Display(Name = "Wrocław")]
        Wroclaw
    }

}
