using System;
using System.Collections.Generic;
using System.Text;

namespace ZHPEvents.ViewModels.Dashboard
{
    public class IndexViewModel
    {

        public int AllUsers { get; set; }
        public int Administrators { get; set; }
        public int Editors { get; set; }
        public int Authors { get; set; }
        public int EventEditors { get; set; }
        public int EventAuthors { get; set; }
        public int RaportEditors { get; set; }
        public int RaportAuthors { get; set; }
        public int Users { get; set; }

        public string AdministratorsPercent { get; set; }
        public string EditorsPercent { get; set; }
        public string AuthorsPercent { get; set; }
        public string EventEditorsPercent { get; set; }
        public string EventAuthorsPercent { get; set; }
        public string RaportEditorsPercent { get; set; }
        public string RaportAuthorsPercent { get; set; }
        public string UsersPercent { get; set; }

        public int Events { get; set; }
        public int NewEvents { get; set; }
        public int ApprovedEvents { get; set; }
        public int RejectedEvents { get; set; }
        public int ArchivedEvents { get; set; }

        public string NewEventsPercent { get; set; }
        public string ApprovedEventsPercent { get; set; }
        public string RejectedEventsPercent { get; set; }
        public string ArchivedEventsPercent { get; set; }

        public int Raports { get; set; }
        public int NewRaports { get; set; }
        public int ApprovedRaports { get; set; }
        public int RejectedRaports { get; set; }
        public int ArchivedRaports { get; set; }

        public string NewRaportsPercent { get; set; }
        public string ApprovedRaportsPercent { get; set; }
        public string RejectedRaportsPercent { get; set; }
        public string ArchivedRaportsPercent { get; set; }

        public int UserEvents { get; set; }
        public int UserNewEvents { get; set; }
        public int UserApprovedEvents { get; set; }
        public int UserRejectedEvents { get; set; }
        public int UserArchivedEvents { get; set; }

        public string UserNewEventsPercent { get; set; }
        public string UserApprovedEventsPercent { get; set; }
        public string UserRejectedEventsPercent { get; set; }
        public string UserArchivedEventsPercent { get; set; }

        public int UserRaports { get; set; }
        public int UserNewRaports { get; set; }
        public int UserApprovedRaports { get; set; }
        public int UserRejectedRaports { get; set; }
        public int UserArchivedRaports { get; set; }

        public string UserNewRaportsPercent { get; set; }
        public string UserApprovedRaportsPercent { get; set; }
        public string UserRejectedRaportsPercent { get; set; }
        public string UserArchivedRaportsPercent { get; set; }

        public bool UserEmailConfirmed { get; set; }
        public string UserRole { get; set; }
        public bool UserProfilFull { get; set; }
    }
}
