namespace ClassLibrary.WebRequest
{
	using System.Collections.Concurrent;
	using System.Collections.Generic;
	using System.Linq;
	using System.Net.Http;
	using System.Threading.Tasks;

	public class WebRequest<T, U>
		where T : class
		where U : class
	{
		private const int BatchSize = 50;

		public delegate Task<U> WebRequestHandler(object sender, WebRequestEventArgs<T> e);

		public event WebRequestHandler OnWebRequest;

		public async Task<List<U>> RunBatchWebRequestAsync(List<T> request)
		{
			var requests = new ConcurrentQueue<T>(request);
			var responses = new List<U>();

			using (var client = new HttpClient())
			{
				while (requests.Count > 0)
				{
					responses.AddRange(await this.RunBatchWebRequestAsync(client, requests));
				}
			}

			return responses;
		}

		public async Task<List<U>> RunBatchWebRequestAsync(ConcurrentQueue<T> requests)
		{
			var responses = new List<U>();

			using (var client = new HttpClient())
			{
				while (requests.Count > 0)
				{
					responses.AddRange(await this.RunBatchWebRequestAsync(client, requests));
				}
			}

			return responses;
		}

		private async Task<List<U>> RunBatchWebRequestAsync(HttpClient client, ConcurrentQueue<T> requests)
		{
			var tasks = new List<Task<U>>();

			for (var i = 0; i < BatchSize; i++)
			{
				T request;
				if (requests.TryDequeue(out request))
				{
					tasks.Add(this.ProcessRequestAsync(client, request));
				}
				else
				{
					break;
				}
			}

			var responses = await Task.WhenAll(tasks);

			return responses.ToList();
		}

		private async Task<U> ProcessRequestAsync(HttpClient client, T request)
		{
			var args = new WebRequestEventArgs<T>(client, request);
			return await this.OnWebRequest(this, args);
		}
	}
}
