using Drivers.Models;
using System.Net;
using System.Text.Json;

namespace Drivers.Utils
{
	public class ApiCommunications
	{
		private readonly HttpClient _client;

		public ApiCommunications(string apiSection)
		{
			_client = new HttpClient();
			_client.BaseAddress = new Uri($@"http://localhost:5027/api/{apiSection}");
		}

		public Uri CreateDriver(Driver driver)
		{
			var options = new JsonSerializerOptions
			{
				PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
				PropertyNameCaseInsensitive = true
			};
			HttpResponseMessage response = _client.PostAsJsonAsync(_client.BaseAddress, driver, options).Result;
			return response.Headers.Location;
		}

		public bool DeleteDriver(int id)
		{
			HttpResponseMessage response = _client.DeleteAsync($"{_client.BaseAddress}/{id}").Result;
			return response.StatusCode == HttpStatusCode.OK;
		}

		public Driver UpdateDriver(Driver driver)
		{
			Driver updatedDriver = null;
			HttpResponseMessage response = _client.PutAsJsonAsync(_client.BaseAddress, driver).Result;
			if (response.IsSuccessStatusCode)
			{
				var content = response.Content.ReadAsStringAsync().Result;
				var options = new JsonSerializerOptions
				{
					PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
					PropertyNameCaseInsensitive = true
				};
				updatedDriver = JsonSerializer.Deserialize<Driver>(content, options);
			}

			return updatedDriver;
		}

		public List<Driver> GetDrivers()
		{
			var drivers = new List<Driver>();
			var response = _client.GetAsync(_client.BaseAddress).Result;
			if (response.IsSuccessStatusCode)
			{
				var content = response.Content.ReadAsStringAsync().Result;
				var options = new JsonSerializerOptions
				{
					PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
					PropertyNameCaseInsensitive = true
				};
				drivers = JsonSerializer.Deserialize<List<Driver>>(content, options);
			}

			return drivers;
		}

		public Driver GetDriverById(int id)
		{
			Driver driver = null;
			var response = _client.GetAsync($"{_client.BaseAddress}/{id}").Result;
			if (response.IsSuccessStatusCode)
			{
				var content = response.Content.ReadAsStringAsync().Result;
				var options = new JsonSerializerOptions
				{
					PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
					PropertyNameCaseInsensitive = true
				};
				driver = JsonSerializer.Deserialize<Driver>(content, options);
			}

			return driver;
		}

		public List<Car> GetCars()
		{
			var cars = new List<Car>();
			var response = _client.GetAsync(_client.BaseAddress).Result;
			if (response.IsSuccessStatusCode)
			{
				var content = response.Content.ReadAsStringAsync().Result;
				var options = new JsonSerializerOptions
				{
					PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
					PropertyNameCaseInsensitive = true
				};
				cars = JsonSerializer.Deserialize<List<Car>>(content, options);
			}

			return cars;
		}

		public bool BatchDeleteCars()
		{
			HttpResponseMessage response = _client.DeleteAsync($"{_client.BaseAddress}").Result;
			return response.StatusCode == HttpStatusCode.OK;
		}
	}
}
