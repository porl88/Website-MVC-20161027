namespace ClassLibrary.Map
{
	using System;

	public struct GeoCoordinate : IEquatable<GeoCoordinate>
	{
		private readonly double latitude;
		private readonly double longitude;

		public GeoCoordinate(double latitude, double longitude)
		{
			this.latitude = latitude;
			this.longitude = longitude;
		}

		public double Latitude
		{
			get
			{
				return this.latitude;
			}
		}

		public double Longitude
		{
			get
			{
				return this.longitude;
			}
		}

		public override string ToString()
		{
			return string.Format("{0}, {1}", this.Latitude, this.Longitude);
		}

		public bool Equals(object other)
		{
			return other is GeoCoordinate && this.Equals((GeoCoordinate)other);
		}

		public bool Equals(GeoCoordinate other)
		{
			return this.Latitude == other.Latitude && this.Longitude == other.Longitude;
		}

		public override int GetHashCode()
		{
			return this.Latitude.GetHashCode() ^ this.Longitude.GetHashCode();
		}
	}
}
