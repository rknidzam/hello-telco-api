using System.ComponentModel.DataAnnotations;

namespace hello_telco_api.Models
{
    public class Customer
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(50)]
        public string Name { get; set; }

        public List<PhoneNumber> PhoneNumbers { get; set; }
    }
}