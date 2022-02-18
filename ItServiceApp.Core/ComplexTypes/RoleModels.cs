using System.Collections.Generic;

namespace ItServiceApp.Core.ComplexTypes
{
    public class RoleModels
    {
        public static string Admin = "Admin";
        public static string User = "User";
        public static string Passive = "Passive";

        public static ICollection<string> Roles => new List<string>() { Admin, User, Passive };
    }
}
