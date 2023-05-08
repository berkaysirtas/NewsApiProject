using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NewsApiProject.ViewModel
{
    public class MembersModel
    {
        public int MemberId { get; set; }
        public string UsersName { get; set; }
        public string Email { get; set; }
        public string NameSurname { get; set; }
        public string MemberAdmin { get; set; }
        public string Pasword { get; set; }

    }
}