using System.Linq;

namespace ApprovalTests.Tests.Reporters
{
    using System;
    using System.IO;
    using System.Windows;
    using ApprovalTests.Namers;
    using ApprovalTests.Reporters;
    using NUnit.Framework;

    [TestFixture]
    public class ClipboardReporterTest
    {
        [UseReporter(typeof(ClipboardReporter),typeof(DiffReporter))]
        [Test]
        public void Test()
        {
            // we start by calculating the approval name
            var namer = new UnitTestFrameworkNamer();
            string approvalFileName = string.Format("{0}.approved.txt", namer.Name);
            string approvalPath = Path.Combine(namer.SourcePath, approvalFileName);

            // then we remove any valid approval
            if (File.Exists(approvalPath))
            {
                File.Delete(approvalPath);
            }
            Assert.IsFalse(File.Exists(approvalPath));

            // and clear the clipboard
            System.Windows.Clipboard.Clear();

            // then we fail an approval (note the clipboardreporter attribute on this method)
            Exception ex = null;
            try
            {
                Approvals.Verify("mal");
            }
            catch (Exception x)
            {
                ex = x;
            }
            Assert.NotNull(ex); // because we expected an approval failure

            Assert.IsTrue(Clipboard.ContainsText());

            string clipboardText = Clipboard.GetText();
            Assert.IsTrue(clipboardText.StartsWith("move"));

            //Approvals.Verify(clipboardText);

            // execute the clipboard text
            System.Diagnostics.Process.Start("cmd.exe", string.Format("{0}", clipboardText));

            // and now it should pass
            Approvals.Verify("mal");
        }
    }
}