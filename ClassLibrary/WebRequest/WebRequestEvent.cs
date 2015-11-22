namespace ClassLibrary.WebRequest
{
	using System;
	using System.Net.Http;

	public class WebRequestEventArgs<T> : EventArgs
	{
		public readonly HttpClient Client;

		public readonly T Request;

		public WebRequestEventArgs(HttpClient client, T request)
		{
			this.Client = client;
			this.Request = request;
		}
	}
}
