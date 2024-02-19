using Drivers.Models;
using System.Net;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Drivers.Utils
{
	public class ApiCommunications
	{
		public string ApiName { get; set; }
		public Uri ApiFullUri => new Uri($@"{_client.BaseAddress}{ApiName}");
		private readonly HttpClient _client;

		public ApiCommunications(string apiName)
		{
			_client = new HttpClient();
			_client.BaseAddress = new Uri($@"http://localhost:5027/api/");
			ApiName = apiName;
		}

		public Driver CreateDriver(Driver driver)
		{
			Driver newDriver = null;
			var options = new JsonSerializerOptions
			{
				PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
				PropertyNameCaseInsensitive = true
			};
			HttpResponseMessage response = _client.PostAsJsonAsync(ApiFullUri.OriginalString, driver, options).Result;
			if (response.IsSuccessStatusCode)
			{
				var content = response.Content.ReadAsStringAsync().Result;
				newDriver = JsonSerializer.Deserialize<Driver>(content, options);
			}

			return newDriver;
		}

		public bool DeleteDriver(int id)
		{
			HttpResponseMessage response = _client.DeleteAsync($"{ApiFullUri}/{id}").Result;
			return response.StatusCode == HttpStatusCode.OK;
		}

		public Driver UpdateDriver(Driver driver)
		{
			Driver updatedDriver = null;
			HttpResponseMessage response = _client.PutAsJsonAsync(ApiFullUri, driver).Result;
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
			var response = _client.GetAsync(ApiFullUri).Result;
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
			var response = _client.GetAsync($"{ApiFullUri}/{id}").Result;
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
			var response = _client.GetAsync(ApiFullUri).Result;
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
			HttpResponseMessage response = _client.DeleteAsync(ApiFullUri).Result;
			return response.StatusCode == HttpStatusCode.OK;
		}

		public Car AssignCar(int driverId, int carId)
		{
			Car assignedCar = null;
			var apiAdditionalPath = $"/{driverId}/cars/{carId}";
			HttpResponseMessage response = _client.PutAsync(ApiFullUri + apiAdditionalPath, null).Result;
			if (response.IsSuccessStatusCode)
			{
				var content = response.Content.ReadAsStringAsync().Result;
				var options = new JsonSerializerOptions
				{
					PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
					PropertyNameCaseInsensitive = true
				};
				assignedCar = JsonSerializer.Deserialize<Car>(content, options);
			}

			return assignedCar;
		}

		public bool UnssignCar(int driverId, int carId)
		{
			var apiAdditionalPath = $"/{driverId}/cars/{carId}";
			HttpResponseMessage response = _client.DeleteAsync(ApiFullUri + apiAdditionalPath).Result;
			return response.StatusCode == HttpStatusCode.OK;
		}
	}
}
