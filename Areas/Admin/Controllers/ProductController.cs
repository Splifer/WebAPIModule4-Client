using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using WebAPIModule4_Client.Controllers;
using WebAPIModule4_Client.Models.Product;

namespace WebAPIModule4_Client.Areas.Admin.Controllers
{
	[Area("Admin")]
	[Route("Product")]
	public class ProductController : Controller
	{
		private readonly ILogger<HomeController> _logger;

		private readonly IHttpContextAccessor _httpContextAccessor;

		public ProductController(IHttpContextAccessor httpContextAccessor, ILogger<HomeController> logger)
		{
			_httpContextAccessor = httpContextAccessor;
			_logger = logger;
		}

		public async Task<IActionResult> Product()
		{
			List<Product> productList = new List<Product>();
			using (var httpClient = new HttpClient())
			{
				using (var response = await httpClient.GetAsync("http://localhost:5179/api/Product/lay-danh-sach-san-pham"))
				{
					string apiResponse = await response.Content.ReadAsStringAsync();
					productList = JsonConvert.DeserializeObject<List<Product>>(apiResponse);
				}
			}
			return View(productList);
		}

		public async Task<IActionResult> UpdateProduct(int id)
		{
			Product product = new Product();
			using (var httpClient = new HttpClient())
			{
				using (var response = await httpClient.GetAsync("http://localhost:5179/api/Account/cap-nhat-tai-khoan/" + id))
				{
					string apiResponse = await response.Content.ReadAsStringAsync();
					product = JsonConvert.DeserializeObject<Product>(apiResponse);
				}
			}
			return View(product);
		}

		[HttpPost]
		public async Task<IActionResult> UpdateProduct(Product product)
		{
			Product receivedReservation = new Product();
			using (var httpClient = new HttpClient())
			{
				var content = new MultipartFormDataContent();
				content.Add(new StringContent(product.ProductId.ToString()), "Id");
				content.Add(new StringContent(product.ProductName), "Name");
				content.Add(new StringContent(product.Price.ToString()), "StartLocation");
				content.Add(new StringContent(product.Icon), "EndLocation");

				using (var response = await httpClient.PutAsync("http://localhost:5179/api/Account/cap-nhat-tai-khoan/", content))
				{
					string apiResponse = await response.Content.ReadAsStringAsync();
					ViewBag.Result = "Success";
					receivedReservation = JsonConvert.DeserializeObject<Product>(apiResponse);
				}
			}
			return View(receivedReservation);
		}
	}
}
