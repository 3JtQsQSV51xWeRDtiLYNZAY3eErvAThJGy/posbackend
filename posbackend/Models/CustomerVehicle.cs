using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace posbackend.Models
{
    public class CustomerVehicle
    {
        [Key]
        public int Id { get; set; }
        public int TenantId { get; set; }
        public int CustomerId { get; set; }
        public string LicensePlate { get; set; }
        public string Province { get; set; }
        public string Brand { get; set; }
        public string Model { get; set; }
        public string VinNumber { get; set; }
        public int? CurrentMileage { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}