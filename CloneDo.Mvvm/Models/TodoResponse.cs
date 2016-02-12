using System;
using CloneDo.Mvvm.Services;
using Newtonsoft.Json;
using System.Net;
using System.Threading.Tasks;
using System.Threading;

namespace CloneDo.Mvvm.Models
{
	public class TodoResponse: IResponseObject
	{
		private TodoResponseMessage response;
		public bool Ok { 
			get {
				return response.status == "success" ? true : false;
			}
		}
		public string Message {
			get {
				return response.message;
			}
		}
		public string Body {
			get {
				return response.payload;
			}
		}
		public Exception Error {
			get;
			set;
		}

		public void Serialize(string content) {
			response = JsonConvert.DeserializeObject<TodoResponseMessage> (content);
		}

		public void HandleException(Exception e, CancellationTokenSource source) {
			Type exceptionType = e.GetType ();
			if (exceptionType == typeof(TaskCanceledException)) {
				TaskCanceledException tce = (TaskCanceledException)e;
				if (tce.CancellationToken == source.Token) {
					System.Diagnostics.Debug.WriteLine ("TodoResponse::This is a real cancellation triggered by the caller.");
				} else {
					System.Diagnostics.Debug.WriteLine ("TodoResponse::This is a web request time out.");
				}
			}

			response = new TodoResponseMessage ();
			response.status = "error";
			response.message = "Something bad is happening in Oz.";
			response.payload = null;
		}

		class TodoResponseMessage {
			public string status { get; set; }
			public string message { get; set; }
			public string payload { get; set; }
		}
	}
}

