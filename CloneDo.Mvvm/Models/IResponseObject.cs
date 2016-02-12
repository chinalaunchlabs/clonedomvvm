using System;
using System.Net;
using System.Threading;

namespace CloneDo.Mvvm.Services
{
	public interface IResponseObject
	{
		void Serialize(string content);	
		void HandleException(Exception e, CancellationTokenSource source);
	}
}

