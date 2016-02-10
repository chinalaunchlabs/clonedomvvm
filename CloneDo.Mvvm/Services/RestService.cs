using System;
using CloneDo.Mvvm.Services;
using System.Net.Http;
using System.Threading.Tasks;
using System.Text;

namespace CloneDo.Mvvm.Services
{
	/// <summary>
	/// Wrapper for the Microsoft.HttpClient library.
	/// </summary>
	public class RestService : IRestService
	{
		private HttpClient _client; 

		/// <summary>
		/// Initializes a new instance of the <see cref="CloneDo.Mvvm.Services.RestService"/> class.
		/// </summary>
		public RestService ()
		{
			_client = new HttpClient ();
			_client.DefaultRequestHeaders.Accept.Clear ();
			_client.DefaultRequestHeaders.Accept.Add (new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue ("application/json"));
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="CloneDo.Mvvm.Services.RestService"/> class.
		/// </summary>
		/// <param name="url">Base URI of the API.</param>
		public RestService (string url)
		{
			_client = new HttpClient ();
			_client.BaseAddress = new Uri (url);
			_client.DefaultRequestHeaders.Accept.Clear ();
			_client.DefaultRequestHeaders.Accept.Add (new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue ("application/json"));
		}

		/// <summary>
		/// Asynchronously performs an HTTP POST request.
		/// </summary>
		/// <param name="uri">Create URI.</param>
		/// <param name="json">Serialized JSON object.</param>
		/// <typeparam name="T">IResponseObject.</typeparam>
		public async Task<T> Post<T>(string uri, string json) where T: IResponseObject {
			StringContent body = new StringContent (json, Encoding.UTF8, "application/json");
			HttpResponseMessage response = await _client.PostAsync (uri, body);

			IResponseObject instance = ((IResponseObject)Activator.CreateInstance<T> ());
			instance.Serialize (await response.Content.ReadAsStringAsync());

			return (T)instance;
		}

		/// <summary>
		/// Asynchronously performs an HTTP GET request.
		/// </summary>
		/// <param name="parameters">Fetch URI.</param>
		/// <typeparam name="T">The 1st type parameter.</typeparam>
		public async Task<T> Get<T>(string parameters) where T: IResponseObject {
			HttpResponseMessage response = await _client.GetAsync (parameters);

			IResponseObject instance = ((IResponseObject)Activator.CreateInstance<T>());
			instance.Serialize (await response.Content.ReadAsStringAsync ());

			return (T) instance;
		}

		/// <summary>
		/// Asynchronously performs an HTTP PUT request.
		/// </summary>
		/// <param name="uri">Update URI.</param>
		/// <param name="json">Serialized JSON object.</param>
		/// <typeparam name="T">The 1st type parameter.</typeparam>
		public async Task<T> Put<T> (string uri, string json) where T : IResponseObject {
			StringContent body = new StringContent (json, Encoding.UTF8, "application/json");
			HttpResponseMessage response = await _client.PutAsync(uri, body);

			IResponseObject instance = ((IResponseObject)Activator.CreateInstance<T>());
			instance.Serialize(await response.Content.ReadAsStringAsync());

			return (T)instance;
		}

		/// <summary>
		/// Asynchronously performs an HTTP DELETE request.
		/// </summary>
		/// <param name="parameters">Destroy URI.</param>
		/// <typeparam name="T">The 1st type parameter.</typeparam>
		public async Task<T> Delete<T> (string parameters) where T : IResponseObject {
			HttpResponseMessage response = await _client.DeleteAsync(parameters);

			IResponseObject instance = ((IResponseObject)Activator.CreateInstance<T>());
			instance.Serialize(await response.Content.ReadAsStringAsync());

			return (T)instance;
		}
	}
}

