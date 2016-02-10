using System;
using CloneDo.Mvvm.Services;
using Newtonsoft.Json;
using System.Net;

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

		public void Serialize(string content) {
			response = JsonConvert.DeserializeObject<TodoResponseMessage> (content);
		}

		class TodoResponseMessage {
			public string status { get; set; }
			public string message { get; set; }
			public string payload { get; set; }
		}
	}
}

