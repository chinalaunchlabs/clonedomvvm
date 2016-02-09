using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace CloneDo.Mvvm.Services
{
	/// <summary>
	/// REST wrapper around the Microsoft.HttpClient client.
	/// </summary>
	public class RestService
	{
		private HttpClient _client; 

		/// <summary>
		/// Initializes a new instance of the <see cref="CloneDo.Mvvm.Services.RestClient"/> class.
		/// </summary>
		/// <param name="url">Base URL for the web service API.</param>
		public RestService (string url)
		{
			_client = new HttpClient ();
			_client.BaseAddress = new Uri (url);
		}

		/// <summary>
		/// Performs an HTTP GET request.
		/// </summary>
		/// <param name="parameters">Parameters.</param>
		public async Task<string> Get(string parameters) {
			string rawResponse;

			_client.DefaultRequestHeaders.Accept.Clear ();
			_client.DefaultRequestHeaders.Accept.Add (new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue ("application/json"));

			HttpResponseMessage response = await _client.GetAsync (parameters);
			if (response.IsSuccessStatusCode) {
				rawResponse = await response.Content.ReadAsStringAsync ();
			} else {
				rawResponse = "";
				// TODO: throw exception?
			}

			return rawResponse;
		}

		/// <summary>
		/// Performs an HTTP POST Request.
		/// </summary>
		/// <returns>true on success, false otherwise</returns>
		/// <param name="uri">URI.</param>
		/// <param name="content">Url-encoded content (formatted via FormUrlEncodedContent() for example).</param>
		public async Task<bool> Post(string uri, string content) {

			HttpResponseMessage response = await _client.PostAsync (uri, new StringContent(content));

			if (response.IsSuccessStatusCode) {
				return true;
			} else {
				return false;
			}

		}

	}
}

