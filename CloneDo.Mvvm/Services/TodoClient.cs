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

//		private string ApiBase = "http://localhost:3000/"; // for iOS
		private string ApiBase = "http://192.168.254.200:3000/"; // on private network
		private RestService _restClient;

		public TodoClient ()
		{
			_restClient = new RestService (ApiBase);	
		}

		public async Task<List<TaskItem>> FetchAllTasksAsync() {
			string parameters = "tasks";
			var response = await _restClient.Get(parameters);
			System.Diagnostics.Debug.WriteLine (response);
			List<TaskItem> tasks = JsonConvert.DeserializeObject<List<TaskItem>> (response);
			return tasks;
		}

		public async Task<TaskItem> FetchTask(int id) {
			string parameters = string.Format ("tasks/{0}", id);
			var response = await _restClient.Get (parameters);
			System.Diagnostics.Debug.WriteLine (response);
			TaskItem task = JsonConvert.DeserializeObject<TaskItem> (response);
			return task;
		}

		public void SaveTask(TaskItem task) {
			if (task.ID == 0) {
				NewTask (task);
			} else {
				UpdateTask (task);
			}
		}

		public async Task<bool> NewTask(TaskItem task) {
			var json = JsonConvert.SerializeObject (task, Formatting.Indented, new JsonSerializerSettings {
				ContractResolver = new LowercaseContractResolver()	// turn property names into lowercase
			});
			bool success = await _restClient.Post ("tasks/", json);
			if (success)
				System.Diagnostics.Debug.WriteLine ("Posted successfully.");
			else
				System.Diagnostics.Debug.WriteLine ("Post not successful.");
			return success;
		}

		public async Task<bool> DeleteTask(int id) {
			string parameters = string.Format ("tasks/{0}", id);
			bool success = await _restClient.Delete (parameters);
			if (success)
				System.Diagnostics.Debug.WriteLine ("Deleted successfully.");
			else
				System.Diagnostics.Debug.WriteLine ("Delete not successful.");
			return success;

		}

		public async Task<bool> UpdateTask(TaskItem task) {
			var json = JsonConvert.SerializeObject (task, Formatting.Indented, new JsonSerializerSettings {
				ContractResolver = new LowercaseContractResolver()	// turn property names into lowercase
			});
			string parameters = string.Format ("tasks/{0}", task.ID);
			bool success = await _restClient.Put (parameters, json);
			if (success)
				System.Diagnostics.Debug.WriteLine ("Updated successfully.");
			else
				System.Diagnostics.Debug.WriteLine ("Update not successful.");
			return success;
		}
			
	}
}

