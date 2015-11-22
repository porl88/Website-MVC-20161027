namespace MVC.Services.Map
{
	using System.Threading.Tasks;
	using ClassLibrary.Map;

    public interface IMapService
    {
        Task<GetGeoCoordinatesResponse> GetGeoCoordinatesAsync(GetGeoCoordinatesRequest request);

		Task<double?> GetDistanceAsync(GeoCoordinate startPoint, GeoCoordinate endPoint, DistanceUnit distanceUnit);

        Task<double?> GetDistanceAsync(GetGeoCoordinatesRequest startPoint, GetGeoCoordinatesRequest endPoint, DistanceUnit distanceUnit);
    }
}
