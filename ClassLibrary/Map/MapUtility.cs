namespace ClassLibrary.Map
{
	using System;

	public static class MapUtility
	{
		public static double GetDistance(GeoCoordinate startPoint, GeoCoordinate endPoint, DistanceUnit distanceUnit)
		{
			var distanceType = (distanceUnit == DistanceUnit.Miles) ? 3960 : 6371;

			var distanceLat = ToRadian(endPoint.Latitude - startPoint.Latitude);
			var distanceLon = ToRadian(endPoint.Longitude - endPoint.Longitude);

			var a = (Math.Sin(distanceLat / 2) * Math.Sin(distanceLat / 2)) + (Math.Cos(ToRadian(startPoint.Latitude)) * Math.Cos(ToRadian(endPoint.Latitude)) * Math.Sin(distanceLon / 2) * Math.Sin(distanceLon / 2));
			var c = 2 * Math.Asin(Math.Min(1, Math.Sqrt(a)));
			var d = distanceType * c;

			return d;
		}

		/// <summary>
		/// Converts to Radians.
		/// </summary>
		private static double ToRadian(double val)
		{
			return (Math.PI / 180) * val;
		}
	}
}
