﻿using ZHPEvents.Core.Identity;

namespace ZHPEvents.ViewModels.Identities
{
    public class ZHPEventsUserViewModel
    {
        public string Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }

    }

    public static class ZHPEventsUserExtension
    {
        public static ZHPEventsUserViewModel GetViewModel(this AppUser user)
        {
            return new ZHPEventsUserViewModel()
            {
                Id = user.Id,
                FirstName = user.FristName,
                LastName = user.LastName,
                Email = user.Email
            };
        }
    }
}