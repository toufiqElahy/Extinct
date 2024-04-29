
namespace interwebz.Models
{
	public class Product
	{
        public int ProductId { get; set; }
		public string Name { get; set; }
		public string Title { get; set; }
		public string Description { get; set; }
        public string Status { get; set; }
		public string Image { get; set; }
		public DateTime CreationDate { get; set; }= DateTime.UtcNow;
		public string RedeemtionCode { get; set; }
	}
}
