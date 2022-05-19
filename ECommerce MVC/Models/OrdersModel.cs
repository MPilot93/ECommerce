namespace ECommerce_MVC.Models
{
    public class OrdersModel
    {
        public int IdOrder { get; set; }
        //public int C8 { get; set; }
        public int IdCust { get; set; }
        public float Price { get; set; }
        public DateTime Date { get; set; }
    }
}