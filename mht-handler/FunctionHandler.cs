using MIMER.RFC2045;
using System;
using System.IO;
using System.Text;

namespace Function
{
    public class FunctionHandler
    {
        public string Handle(string input)
        {
            // Console.Out.WriteLine(input);

            if (string.IsNullOrEmpty(input))
            {
                return "<html><body>Null input</body></html>";
            }

            if(!input.Contains("\r"))
            {
                var lines = input.Split(new char[] { '\n' });
                input = string.Join("\r\n", lines);
            }

            var result = ExtractHtml(Encoding.UTF8.GetBytes(input));
            if(result == null)
            {
                result = ExtractHtml(Encoding.Unicode.GetBytes(input));
            }

            return result ?? "<html><body>There is no HTML message in this message.</body></html>";
        }

        string ExtractHtml(byte[] input)
        {
            var reader = new MailReader();
            MIMER.IEndCriteriaStrategy endofmessage = new BasicEndOfMessageStrategy();
            var stream = (Stream)new MemoryStream(input);

            var message = reader.ReadMimeMessage(ref stream, endofmessage);
            stream.Close();

            foreach (var attachment in message.Attachments)
            {
                if (attachment.Type == "text" && attachment.SubType.StartsWith("htm"))
                {
                    return Encoding.UTF8.GetString(attachment.Data);
                }
            }

            return null;
        }
    }
}
