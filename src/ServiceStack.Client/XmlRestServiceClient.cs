using System;
using ServiceStack.Service;
using ServiceStack.Text;

namespace ServiceStack.ServiceClient.Web
{
	public class XmlRestServiceClient : IRestClientAsync
	{
		public const string ContentType = "application/xml";

		public XmlRestServiceClient()
		{
			this.client = new AsyncServiceClient {
				ContentType = ContentType,
				StreamSerializer = XmlSerializer.SerializeToStream,
				StreamDeserializer = XmlSerializer.DeserializeFromStream
			};
		}

		public XmlRestServiceClient(string baseUri) : this()
		{
			this.BaseUri = baseUri;
		}

		private readonly AsyncServiceClient client;

		public string BaseUri { get; set; }

		private string GetUrl(string relativeOrAbsoluteUrl)
		{
			return relativeOrAbsoluteUrl.StartsWith("http")
				 ? relativeOrAbsoluteUrl
				 : this.BaseUri + relativeOrAbsoluteUrl;
		}

		public void GetAsync<TResponse>(string relativeOrAbsoluteUrl, Action<TResponse> onSuccess, Action<TResponse, Exception> onError)
		{
			this.client.SendAsync("GET", GetUrl(relativeOrAbsoluteUrl), null, onSuccess, onError);
		}

		public void DeleteAsync<TResponse>(string relativeOrAbsoluteUrl, Action<TResponse> onSuccess, Action<TResponse, Exception> onError)
		{
			this.client.SendAsync("DELETE", GetUrl(relativeOrAbsoluteUrl), null, onSuccess, onError);
		}

		public void PostAsync<TResponse>(string relativeOrAbsoluteUrl, object request, Action<TResponse> onSuccess, Action<TResponse, Exception> onError)
		{

			this.client.SendAsync("POST", GetUrl(relativeOrAbsoluteUrl), request, onSuccess, onError);
		}

		public void PutAsync<TResponse>(string relativeOrAbsoluteUrl, object request, Action<TResponse> onSuccess, Action<TResponse, Exception> onError)
		{
			this.client.SendAsync("PUT", GetUrl(relativeOrAbsoluteUrl), request, onSuccess, onError);
		}

		public void Dispose()
		{
		}
	}
}