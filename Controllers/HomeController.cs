using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using NuGet.Protocol.Core.Types;
using System.Diagnostics;
using System.Net.Http;
using WebAPIModule4_Client.Models;
using WebAPIModule4_Client.Models.Product;
using WebApplication1.Models;

namespace WebAPIModule4_Client.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

		private readonly IHttpContextAccessor _httpContextAccessor;

		public HomeController(IHttpContextAccessor httpContextAccessor, ILogger<HomeController> logger)
        {
			_httpContextAccessor = httpContextAccessor;
            _logger = logger;
		}

		public async Task<IActionResult> Index()
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

		//public async Task<IActionResult> Index()
		//{
		//	var items = await GetHomeProductList();
		//	return View(items);
		//}

		//private async Task<Product> GetHomeProductList()
		//{
		//	string baseUrl = "http://localhost:5179/api/Product/lay-danh-sach-san-pham";

		//	using (var httpClient = new HttpClient())
		//	{

		//		HttpResponseMessage response = await httpClient.GetAsync(baseUrl);
		//		if (response.IsSuccessStatusCode)
		//		{
		//			Product productlist = new Product();
		//			productlist = response.Content.ReadAsAsync<Product>().Result;
		//			return productlist;
		//		}
		//		return null;
		//	}
		//}

		//[HttpGet("{id}")]
		//public ActionResult<Product> Get(int id)
		//{
		//    if (id == 0)
		//        return BadRequest("Value must be passed in the request body.");
		//    return Ok(repository[id]);
		//}

		//public ViewResult GetProductDetail() => View();

		[HttpGet]
		public IActionResult GetPRoductDetail() => View();

		[HttpPost]
		public async Task<IActionResult> GetProductDetail(int id)
		{
			Product product = new Product();
			using (var httpClient = new HttpClient())
			{
				using (var response = await httpClient.GetAsync("http://localhost:5179/api/Product/lay-san-pham-chi-dinh/" + id))
				{
					if (response.StatusCode == System.Net.HttpStatusCode.OK)
					{
						string apiResponse = await response.Content.ReadAsStringAsync();
						product = JsonConvert.DeserializeObject<Product>(apiResponse);
					}
					else
						ViewBag.StatusCode = response.StatusCode;
				}
			}
			return View(product);
		}

		public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}