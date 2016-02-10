using System;
using Newtonsoft.Json.Serialization;

namespace CloneDo.Mvvm
{
	/// <summary>
	/// Convert all keys to lowercase when serializing JSON object.
	/// </summary>
	public class LowercaseContractResolver: DefaultContractResolver
	{
		protected override string ResolvePropertyName(string propertyName) {
			return propertyName.ToLower ();
		}
	}
}

