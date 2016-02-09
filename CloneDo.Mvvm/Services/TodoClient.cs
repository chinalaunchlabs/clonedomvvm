using System;
using System.Threading.Tasks;
using System.Collections.Generic;
using CloneDo.Mvvm.Models;
using Newtonsoft.Json;
using CloneDo.Mvvm.ViewModels;

namespace CloneDo.Mvvm.Services
{
	public class TodoClient
	{
//		private string ApiBase = "http://10.71.34.1:3000/"; 
		// 10.0.2.2 is the loopback interface of the development machine
		// for the android emulator since connecting to 127.0.0.1 
		// will send the emulator to its own loopback interface
		// but Xamarin Android Player is stupid and defines its own IP for these things

		private string ApiBase = "http://localhost:3000/"; // for iOS
		private RestService restClient;

		public TodoClient ()
		{
			restClient = new RestService (ApiBase);	
		}

		public async Task<List<TaskItem>> FetchAllTasksAsync() {
			string parameters = "tasks";
			var response = await restClient.Get(parameters);
			System.Diagnostics.Debug.WriteLine (response);
			List<TaskItem> tasks = JsonConvert.DeserializeObject<List<TaskItem>> (response);
			return tasks;
		}

		public async Task<TaskItem> FetchTask(int id) {
			string parameters = string.Format ("tasks/{0}", id);
			var response = await restClient.Get (parameters);
			System.Diagnostics.Debug.WriteLine (response);
			TaskItem task = JsonConvert.DeserializeObject<TaskItem> (response);
			return task;
		}

	}
}

