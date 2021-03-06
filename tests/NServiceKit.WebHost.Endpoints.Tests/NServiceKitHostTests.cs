using System;
using System.Reflection;
using System.Runtime.Serialization;
using NUnit.Framework;
using NServiceKit.ServiceHost;
using NServiceKit.WebHost.Endpoints.Tests.Support.Host;
using NServiceKit.WebHost.Endpoints.Tests.Support.Services;

namespace NServiceKit.WebHost.Endpoints.Tests
{
    /// <summary>A service kit host tests.</summary>
	[TestFixture]
	public class NServiceKitHostTests
	{
        /// <summary>Can run nested service.</summary>
		[Test]
		public void Can_run_nested_service()
		{
			var host = new TestAppHost();
			host.Init();

			var request = new Nested();
			var response = host.ExecuteService(request) as NestedResponse;

			Assert.That(response, Is.Not.Null);
		}

        /// <summary>Can run test service.</summary>
		[Test]
		public void Can_run_test_service()
		{
			var host = new TestAppHost();
			host.Init();

			var request = new Test();
			var response = host.ExecuteService(request) as TestResponse;

			Assert.That(response, Is.Not.Null);
			Assert.That(response.Foo, Is.Not.Null);
		}

        /// <summary>Call asynchronous one way endpoint on test service calls execute.</summary>
		[Test]
		public void Call_AsyncOneWay_endpoint_on_TestService_calls_Execute()
		{
			var host = new TestAppHost();
			host.Init();

			TestService.ResetStats();

			var request = new Test();
			var response = host.ExecuteService(request, EndpointAttributes.OneWay) as TestResponse;

			Assert.That(response, Is.Not.Null);
			Assert.That(response.ExecuteTimes, Is.EqualTo(1));
			Assert.That(response.ExecuteAsyncTimes, Is.EqualTo(0));
		}

        /// <summary>Call asynchronous one way endpoint on asynchronous test service calls execute asynchronous.</summary>
		[Test]
		public void Call_AsyncOneWay_endpoint_on_AsyncTestService_calls_ExecuteAsync()
		{
			var host = new TestAppHost();
			host.Init();

			TestAsyncService.ResetStats();

			var request = new TestAsync();
			var response = host.ExecuteService(request, EndpointAttributes.OneWay) as TestAsyncResponse;

			Assert.That(response, Is.Not.Null);
			Assert.That(response.ExecuteTimes, Is.EqualTo(0));
			Assert.That(response.ExecuteAsyncTimes, Is.EqualTo(1));
		}
	}
}