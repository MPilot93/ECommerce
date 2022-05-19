namespace ECommerce_MVC.Models
{
    public class CustomerModel
    {
        public int IdCust { get; set; }
        public string Mail { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public DateTime Birth { get; set; }
    }
}