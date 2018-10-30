using System;
using System.Collections.Generic;
using System.Text;

namespace ZHPEvents.ViewModels.Identities.AppUser
{
    public class AppUserViewModel
    {
        public string Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }

    }

    public static class AppUserExtension
    {
        public static AppUserViewModel GetViewModel(this Core.Identity.AppUser user)
        {
            return new AppUserViewModel()
            {
                Id = user.Id,
                FirstName = user.FristName,
                LastName = user.LastName,
                Email = user.Email
            };
        }
    }
}
