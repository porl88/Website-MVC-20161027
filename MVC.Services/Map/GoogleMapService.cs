namespace MVC.Services.Map
{
    using System;
    using System.Net.Http;
    using System.Threading.Tasks;
    using ClassLibrary.Map;
    using Core.Exceptions;
    using Newtonsoft.Json;

    public class GoogleMapService : IMapService
	{
		private const string GeolocationUrl = "http://maps.googleapis.com/maps/api/geocode/json";

		private const string DistanceUrl = "https://maps.googleapis.com/maps/api/distancematrix/json";

		private readonly IExceptionHandler exceptionHandler;

		public GoogleMapService(IExceptionHandler exceptionHandler)
		{
			this.exceptionHandler = exceptionHandler;
		}

		public async Task<GetGeoCoordinatesResponse> GetGeoCoordinatesAsync(GetGeoCoordinatesRequest request)
		{
			var response = new GetGeoCoordinatesResponse();

			if (!string.IsNullOrWhiteSpace(request.PostCode))
			{
				try
				{
					response = await this.GetCoordinatesAsync(request);
				}
				catch (Exception ex)
				{
					this.exceptionHandler.HandleException(ex);
					response.Status = StatusCode.InternalServerError;
				}
			}
			else
			{
				response.Status = StatusCode.BadRequest;
			}

			return response;
		}

		public async Task<double?> GetDistanceAsync(GeoCoordinate startPoint, GeoCoordinate endPoint, DistanceUnit distanceUnit)
		{
			var url = DistanceUrl + string.Format("?origins={0},{1}&destinations={2},{3}&language=en-gb", startPoint.Latitude, startPoint.Longitude, endPoint.Latitude, endPoint.Longitude);

			return await GetDistanceAsync(url, distanceUnit);
		}

		public async Task<double?> GetDistanceAsync(GetGeoCoordinatesRequest startPoint, GetGeoCoordinatesRequest endPoint, DistanceUnit distanceUnit)
		{
			if (startPoint != null && endPoint != null)
			{
				var url = DistanceUrl + string.Format("?origins={0}&destinations={1}&language=en-gb", Uri.EscapeUriString(startPoint.PostCode), Uri.EscapeUriString(endPoint.PostCode));

				return await GetDistanceAsync(url, distanceUnit);
			}

			return null;
		}

		private async Task<GetGeoCoordinatesResponse> GetCoordinatesAsync(GetGeoCoordinatesRequest request)
		{
			var response = new GetGeoCoordinatesResponse();

			using (var locationRequest = new HttpClient())
			{
				locationRequest.Timeout = new TimeSpan(0, 30, 0);
				var url = GeolocationUrl + "?address=" + Uri.EscapeUriString(request.PostCode);
				using (var responseJson = await locationRequest.GetAsync(url))
				{
					if (responseJson.IsSuccessStatusCode)
					{
						var content = await responseJson.Content.ReadAsStringAsync();
						dynamic location = JsonConvert.DeserializeObject(content);
						if (location.status == "OK")
						{
							var addressDetails = location.results[0];
							double longitude, latitude;
							if (double.TryParse(addressDetails.geometry.location.lat.ToString(), out latitude) & double.TryParse(addressDetails.geometry.location.lng.ToString(), out longitude))
							{
								response.Status = StatusCode.OK;
								response.GeoCoordinate = new GeoCoordinate(latitude, longitude);
							}
						}
					}
					else
					{
						this.exceptionHandler.HandleException(new Exception("GetGeoCoordinatesAsync: " + responseJson.ReasonPhrase));
						response.Status = StatusCode.InternalServerError;
					}
				}
			}

			return response;
		}

		private async Task<double?> GetDistanceAsync(string url, DistanceUnit distanceUnit)
		{
			// https://developers.google.com/maps/documentation/directions/

			using (var request = new HttpClient())
			{
				request.Timeout = new TimeSpan(0, 30, 0);
				using (var response = await request.GetAsync(url))
				{
					if (response.IsSuccessStatusCode)
					{
						string content = await response.Content.ReadAsStringAsync();
						dynamic responseJson = JsonConvert.DeserializeObject(content);
						if (responseJson.status == "OK")
						{
							var distanceDetails = responseJson.rows[0];
							double distance;
							if (double.TryParse(distanceDetails.elements[0].distance.value.ToString(), out distance))
							{
								distance = distance / 1000;
								if (distanceUnit == DistanceUnit.Miles)
								{
									distance = distance * 0.621371192;
								}

								return distance;
							}
						}
					}
				}
			}

			return null;
		}
	}
}
