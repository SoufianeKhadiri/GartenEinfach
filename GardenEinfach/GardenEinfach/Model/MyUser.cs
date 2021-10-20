using System;
using System.Collections.Generic;
using System.Text;

namespace GardenEinfach.Model
{
    public class MyUser
    {
        public string Key { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }
        public Adress adress { get; set; }

        public string FullyAdress { get; set; }
        public string Email { get; set; }

        public string Gender { get; set; }
        //public string Password { get; set; }

        public string Phone { get; set; }

        public string Image { get; set; }
    }
}
