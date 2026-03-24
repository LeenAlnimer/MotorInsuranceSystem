namespace MotorInsuranceApp.Models
{
    public class Policy
    {
        public int Id { get; set; }

        public string PolicyNumber { get; set; }

        public decimal Price { get; set; }

        public string Status { get; set; }

        
        public int CustomerId { get; set; }
        public Customer Customer { get; set; }

        public int CarId { get; set; }
        public Car Car { get; set; }
    }
}