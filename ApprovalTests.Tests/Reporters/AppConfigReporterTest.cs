using ApprovalTests.Reporters;
using NUnit.Framework;

namespace ApprovalTests.Tests.Reporters
{
	[TestFixture]
	public class AppConfigReporterTest
	{
		[Test]
		public void Test()
		{
            // because the app.config file contains this:
            // <appSettings>
            //    <add key="UseReporter" value="ApprovalTests.Reporters.DiffReporter, ApprovalTests" />
            // </appSettings>
            //
            Assert.IsInstanceOf<DiffReporter>(new AppConfigReporter().Reporter);
		}
	}
}