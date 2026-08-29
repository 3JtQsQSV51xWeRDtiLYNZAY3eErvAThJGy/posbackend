using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace posbackend.Models
{
    public class CustomerPet
    {
        [Key]
        public int Id { get; set; }
        public int TenantId { get; set; }
        public int CustomerId { get; set; }
        public string PetName { get; set; }
        public string Species { get; set; }
        public string Breed { get; set; }
        public string Gender { get; set; }
        public DateTime? BirthDate { get; set; }
        public string Allergies { get; set; }
        public string ChronicDiseases { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}