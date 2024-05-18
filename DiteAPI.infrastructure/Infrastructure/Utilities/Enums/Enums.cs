using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace DiteAPI.infrastructure.Infrastructures.Utilities.Enums
{
    public class Enums
    {
        public enum Gender
        {
            [Description("Male")]
            Male = 1,
            [Description("Female")]
            Female,
            [Description("Other")]
            Other
        }

        /*public enum UserRoleInAcademy
        {
            [Description("Admin")]
            Admin = 1,
            [Description("Facilitator")]
            Facilitator,
            [Description("Member")]
            Member
        }*/
    }
}
