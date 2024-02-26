using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace hello_telco_api.Models
{
    public class PhoneNumber
    {        
        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(15)]
        public string Number { get; set; }

        [Required]
        public bool Status { get; set; }

        [ForeignKey("Customer")]
        //[JsonIgnore]
        public int CustomerId { get; set; }

        [JsonIgnore]        
        public Customer Customer { get; set; }
    }
}