using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Text;
using WebAPIModule4_Client.Controllers;
using WebAPIModule4_Client.Models.Product;

namespace WebAPIModule4_Client.Areas.Admin.Controllers
{
	[Area("Admin")]
	[Route("admin/product")]
	public class ProductController : Controller
	{
		private readonly ILogger<HomeController> _logger;

		private readonly IHttpContextAccessor _httpContextAccessor;

		public ProductController(IHttpContextAccessor httpContextAccessor, ILogger<HomeController> logger)
		{
			_httpContextAccessor = httpContextAccessor;
			_logger = logger;
		}

		//[Route("danh-sach")]
		//public async Task<IActionResult> Product()
		//{
		//	List<Product> productList = new List<Product>();
		//	using (var httpClient = new HttpClient())
		//	{
		//		using (var response = await httpClient.GetAsync("http://localhost:5179/api/Product/lay-danh-sach-san-pham"))
		//		{
		//			string apiResponse = await response.Content.ReadAsStringAsync();
		//			productList = JsonConvert.DeserializeObject<List<Product>>(apiResponse);
		//		}
		//	}
		//	return View(productList);
		//}

		//[Route("list-product")]
		//public async Task<IActionResult> Index()
		//{
		//	List<Product> productList = new List<Product>();
		//	using (var httpClient = new HttpClient())
		//	{
		//		using (var response = await httpClient.GetAsync("http://localhost:5179/api/Product/lay-danh-sach-san-pham"))
		//		{
		//			string apiResponse = await response.Content.ReadAsStringAsync();
		//			productList = JsonConvert.DeserializeObject<List<Product>>(apiResponse);
		//		}
		//	}
		//	return View(productList);
		//}

		[Route("list-product")]
		public async Task<IActionResult> Index()
		{
			var items = await ProductList();
			return View(items);
		}

		private async Task<List<OutputProduct>> ProductList()
		{
			string baseUrl = "http://localhost:5179/api/Product/lay-danh-sach-san-pham";

			using (var httpClient = new HttpClient())
			{
				HttpResponseMessage response = await httpClient.GetAsync(baseUrl);
				if (response.IsSuccessStatusCode)
				{
					List<Product> productlist = new List<Product>(); //lop ao~ hung du~ lieu tu API
					List<OutputProduct> output = new List<OutputProduct>(); //lop xu ly du~ lieu khi goi API thanh cong
					productlist = response.Content.ReadAsAsync<List<Product>>().Result;
					foreach (var item in productlist)
					{
						OutputProduct outputproduct = new OutputProduct();
						outputproduct.ProductId = item.ProductId;
						outputproduct.ProductName = item.ProductName;
						outputproduct.Price = item.Price;
						outputproduct.Icons = item.Icons;
						output.Add(outputproduct);
					}
					return output;
				}
				return null;
			}
		}

		[Route("create-product")]
		public IActionResult AddProduct() => View();

        [Route("create-product")]
        [HttpPost]
		public async Task<IActionResult> AddProduct(OutputProduct input)
		{
			string baseUrl = "http://localhost:5179/api/Product/tao-san-pham";
			OutputProduct outputProduct = new OutputProduct();
			input.Icons= string.Empty;
			using (var httpClient = new HttpClient())
			{
				StringContent content = new StringContent(JsonConvert.SerializeObject(input), Encoding.UTF8, "application/json");
				var a = JsonConvert.SerializeObject(input);

				using (var response = await httpClient.PostAsync(baseUrl, content))
				{
					string apiResponse = await response.Content.ReadAsStringAsync();
					outputProduct = JsonConvert.DeserializeObject<OutputProduct>(apiResponse);
				}
			}
			return RedirectToAction("Index", "Product", new {Areas="Admin"});
		}

		[Route("update-product")]
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
				content.Add(new StringContent(product.Price.ToString()), "Price");
				content.Add(new StringContent(product.Icons), "Icon");

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
