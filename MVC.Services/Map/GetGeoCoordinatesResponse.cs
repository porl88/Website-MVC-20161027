namespace MVC.Services.Map
{
	using ClassLibrary.Map;

	public class GetGeoCoordinatesResponse : BaseResponse
	{
		public GeoCoordinate GeoCoordinate { get; set; }
	}
}
