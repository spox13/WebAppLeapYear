using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace EFDemo.Models
{
    public class Person
    {
        public int Id { get; set; }

        [MaxLength(100)]
        [Column(TypeName = "varchar(100)")]
        [DisplayName("Wprowadź imię")]
        public string? Name { get; set; }

        [DisplayName("Wprowadź rok")]
        [Required(ErrorMessage = "Te pole jest wymagane.")]
        [Range(1899, 2022, ErrorMessage = "Zakres 1899-2022")]
        public int? Year { get; set; }
        public string? user_id { get; set; }
        public string? login { get; set; }
        public string? Result { get; set; }
        public DateTime CreatedDate { get; set; }
    }
}