using System.ComponentModel.DataAnnotations;

namespace MvcCore.Data.Entities
{
    public class UserRoles
    {
        [Key]
        public int UserID { get; set; }
        public int RoleID { get; set; }
    }
}