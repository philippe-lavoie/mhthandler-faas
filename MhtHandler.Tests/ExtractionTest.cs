using Function;
using MIMER.RFC2045;
using System;
using System.IO;
using System.Net.Mail;
using System.Text;
using Xunit;

namespace MhtHandler.Tests
{
    public class ExtractionTest
    {
        [Fact]
        public void ExtractHtml()
        {
            var message = File.OpenText("encoded.txt").ReadToEnd();
            FunctionHandler handler = new FunctionHandler();
            var result = handler.Handle(message);
            Assert.Contains("Purchase Order 16487645-2", result);
        }

        [Fact]
        public void DecodeBase64()
        {
            var reader = new MailReader();

            MIMER.IEndCriteriaStrategy endofmessage = new BasicEndOfMessageStrategy();
            var stream = (Stream)File.Open("purchase-order.mhtml", FileMode.Open);

            var message = reader.ReadMimeMessage(ref stream, endofmessage);

            stream.Close();

            Assert.Equal(2, message.Attachments.Count);
            Assert.Equal("html", message.Attachments[0].SubType);
        }
    }
}
