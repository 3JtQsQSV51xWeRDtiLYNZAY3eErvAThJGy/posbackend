using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace posbackend.Models
{
    public class PackageUsage
    {
        [Key]
        public int Id { get; set; }
        public int CustomerPackageId { get; set; }
        public int? VisitId { get; set; }
        public decimal UnitsDeducted { get; set; }
        public DateTime UsedAt { get; set; }
        public int RecordedBy { get; set; }
    }
}