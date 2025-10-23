using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LIFT_System
{
    public static class Session
    {
        public static string LoggedInID { get; set; }      // Stores StudentNo / DonorID / AdministratorID
        public static string LoggedInRole { get; set; }    // Stores "Student", "Donor", "Administrator"
        public static string LoggedInName { get; set; }    // (Optional) Store full name if needed
    }
}
