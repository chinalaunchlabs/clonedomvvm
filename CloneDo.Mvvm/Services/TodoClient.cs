using System;
using System.Threading.Tasks;
using System.Collections.Generic;
using CloneDo.Mvvm.Models;
using Newtonsoft.Json;
using CloneDo.Mvvm.ViewModels;
using Xamarin.Forms;

namespace CloneDo.Mvvm.Services
{
	public class TodoClient
	{
//		private string ApiBase = "http://10.71.34.1:3000/"; 
		// 10.0.2.2 is the loopback interface of the development machine
		// for the android emulator since connecting to 127.0.0.1 
		// will send it to its own loopback interface
		// but Xamarin Android Player is stupid and defines its own IP for these things

//		private string ApiBase = "http://localhost:3000/"; // for iOS
		private string ApiBase = "http://192.168.254.200:3001/"; // on private network
		private RestService _restClient;

		public TodoClient ()
		{
			_restClient = new RestService (ApiBase);	
		}

		/// <summary>
		/// Creates a new task on the Todo web server.
		/// </summary>
		/// <returns>Success or not.</returns>
		/// <param name="task">TaskItem instance.</param>
		public async Task<bool> NewTask(TaskItem task) {
			var json = JsonConvert.SerializeObject (task, Formatting.Indented, new JsonSerializerSettings {
				ContractResolver = new LowercaseContractResolver()	// turn property names into lowercase
			});
			var response = await _restClient.Post<TodoResponse> ("tasks/", json);

			dispDialog (response);
			return response.Ok;
		}

		/// <summary>
		/// Fetches all tasks from the web server.
		/// </summary>
		/// <returns>List of TaskItem objects.</returns>
		public async Task<List<TaskItem>> FetchAllTasks() {
			string parameters = "tasks";
			TodoResponse response = await _restClient.Get<TodoResponse>(parameters);

			List<TaskItem> tasks = new List<TaskItem>();

			if (response.Ok) {
				tasks = JsonConvert.DeserializeObject<List<TaskItem>> (response.Body);
			}
			else {
				dispDialog (response);
			} 

			return tasks;
		}

		/// <summary>
		/// Fetches the task with corresponding ID.
		/// </summary>
		/// <returns>The task.</returns>
		public async Task<TaskItem> FetchTask(int id) {
			string parameters = string.Format ("tasks/{0}", id);
			var response = await _restClient.Get<TodoResponse> (parameters);

			TaskItem task = new TaskItem();
			if (response.Ok)
				task = JsonConvert.DeserializeObject<TaskItem> (response.Body);
			else {
				dispDialog (response);
			}
			return task;
		}

		/// <summary>
		/// Creates or updates a task.
		/// </summary>
		/// <param name="task">Task.</param>
		public async Task<bool> SaveTask(TaskItem task) {
			bool b;
			if (task.ID == 0) {
				b = await NewTask (task);
			}
			else {
				b = await UpdateTask (task);
			}
			return b;
		}

		/// <summary>
		/// Deletes the task from the web server.
		/// </summary>
		/// <returns>The task.</returns>
		/// <param name="id">Identifier.</param>
		public async Task<bool> DeleteTask(int id) {
			string parameters = string.Format ("tasks/{0}", id);
			var response = await _restClient.Delete<TodoResponse> (parameters);

			dispDialog (response);

			return response.Ok;
		}

		/// <summary>
		/// Updates the task on the web server.
		/// </summary>
		/// <returns>The task.</returns>
		/// <param name="task">Task.</param>
		public async Task<bool> UpdateTask(TaskItem task) {
			var json = JsonConvert.SerializeObject (task, Formatting.Indented, new JsonSerializerSettings {
				ContractResolver = new LowercaseContractResolver()	// turn property names into lowercase
			});
			string parameters = string.Format ("tasks/{0}", task.ID);
			var response = await _restClient.Put<TodoResponse> (parameters, json);

			dispDialog (response);

			return response.Ok;
		}


		private void dispDialog(TodoResponse response) {
			if (!response.Ok) {
				System.Diagnostics.Debug.WriteLine ("{0}: {1}", response.Ok?"Success":"Error", response.Message);
				// TODO: Maybe implement this somewhere else?
				MessagingCenter.Send<TodoClient, TodoResponse> (this, "StatusMessage", response);
			}
		}

		private void dispErrDialog(string msg) {
			System.Diagnostics.Debug.WriteLine ("WHY");
			// TODO: Maybe implement this somewhere else?
			MessagingCenter.Send<TodoClient, string> (this, "ErrMessage", msg);
		}
			
	}
}

