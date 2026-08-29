using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace posbackend.Models
{
    public class PetHealthRecord
    {
        [Key]
        public int Id { get; set; }
        public int PetId { get; set; }
        public int? VisitId { get; set; }
        public decimal? WeightKg { get; set; }
        public string Diagnosis { get; set; }
        public string TreatmentNotes { get; set; }
        public DateTime? NextDueDate { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}