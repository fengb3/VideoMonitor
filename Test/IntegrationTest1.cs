using System.Net;

namespace VideoMonitor.Test
{
	public class IntegrationTest1
	{
		[Test]
		public async Task GetWebResourceRootReturnsOkStatusCode()
		{
			// Arrange
			var appHost = await DistributedApplicationTestingBuilder.CreateAsync<Projects.VideoMonitor_AppHost>();
			await using var app = await appHost.BuildAsync();
			await app.StartAsync();

			// Act
			var httpClient = app.CreateHttpClient("Worker");
			var response   = await httpClient.GetAsync("/");

			// Assert
			Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.OK));
		}
	}
}