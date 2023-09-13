using System.ComponentModel.DataAnnotations;

namespace Client.Model
{
    public class Client
    {
        [Key]
        public int LicenceId { get; set; }

        [Required(ErrorMessage = "Please again Enter the Licence Key ")]
        public Guid LicenceKey { get; set; }

        [Required(ErrorMessage = "Please again Enter the Name ")]
        [MaxLength(50)]
        public string? ClientName { get; set; }

        [Required(ErrorMessage = "Please again Enter the Description ")]
        [MaxLength(300)]
        public string? Description { get; set; }

        public DateTime? LicenceStartDate { get; set; }

        public DateTime? LicenceEndDate { get; set; }
    }
}
