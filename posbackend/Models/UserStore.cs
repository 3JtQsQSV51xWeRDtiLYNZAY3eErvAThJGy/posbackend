using System;
using System.ComponentModel.DataAnnotations;

namespace posbackend.Models
{
    public class UserStore
    {
        [Key]
        public int Id { get; set; }
        public int UserId { get; set; }
        public int StoreId { get; set; }
        public int RoleId { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
