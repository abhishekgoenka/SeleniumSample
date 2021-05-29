using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SeleniumSample.Models
{
    [Table("AvailableSlots")]
    class AvailableSlot
    {
        [Key]
        [Required]
        public int Id { get; set; }

        [Required]
        public string Center { get; set; }

        [Required]
        public string Vaccine { get; set; }

        [Required]
        public int Qty { get; set; }

        [Required]
        public DateTime DTTM { get; set; }
    }
}
