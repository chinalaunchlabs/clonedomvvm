using System;

namespace CloneDo.Mvvm.Models
{
	public class TaskItem
	{
		public TaskItem ()
		{
			Date = DateTime.Today;	// default date in database
			Name = "";
			Description = "";
			Done = false;
		}

//		[PrimaryKey, AutoIncrement]
		public int ID { get; set; }
		public string Name { get; set; }
		public string Description { get; set; }
		public bool Done { get; set; }
		public DateTime Date { get; set; }
	}
}

