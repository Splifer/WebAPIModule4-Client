using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebAPIModule4_Client.Models.Product;

public class Product
{
    public Guid ProductId { get; set; }

    public string ProductName { get; set; } = null!;

    [StringLength(100)]
    public string? BrandName { get; set; }

    public decimal Price { get; set; }

    public int? Stock { get; set; }

    [StringLength(100)]
    public string? ShippingId { get; set; }

    [StringLength(100)]
    public string? PaymentId { get; set; }

    public DateTime? ManufacturingDate { get; set; }

    public DateTime? ExpiryDate { get; set; }

	public string? Icons { get; set; }


    public string? Description { get; set; }

    public string? Filter { get; set; }

    public bool? IsActive { get; set; }

    //[ForeignKey("BrandName")]
    //[InverseProperty("Products")]
    //public virtual Brand? BrandNameNavigation { get; set; }

    //[InverseProperty("Product")]
    //public virtual ICollection<Order> Orders { get; set; } = new List<Order>();

    //[ForeignKey("PaymentId")]
    //[InverseProperty("Products")]
    //public virtual Payment? Payment { get; set; }

    //[ForeignKey("ShippingId")]
    //[InverseProperty("Products")]
    //public virtual Shipping? Shipping { get; set; }
}