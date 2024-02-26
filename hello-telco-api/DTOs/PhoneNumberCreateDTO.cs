namespace hello_telco_api.DTOs
{
    public class PhoneNumberCreateDTO
    {
        public string Number { get; set; }
        public bool Status { get; set; }
        public int CustomerId { get; set; }
    }
}
