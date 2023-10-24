using System.ComponentModel.DataAnnotations;

namespace WebAPIModule4_Client.Models.Product
{
	public class OutputProduct
	{
		public Guid ProductId { get; set; }

		public string ProductName { get; set; } = null!;

		public string? BrandName { get; set; }

		public decimal Price { get; set; }

		public int? Stock { get; set; }

		public string? ShippingId { get; set; }

		public string? PaymentId { get; set; }

		public DateTime? ManufacturingDate { get; set; }

		public DateTime? ExpiryDate { get; set; }

		public string Icons { get; set; }

		public string? Description { get; set; }

		public string? Filter { get; set; }

		public bool? IsActive { get; set; }
	}
}
