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
            if (string.IsNullOrEmpty(input))
            {
                return "<html><body>Null input</body></html>";
            }

            var reader = new MailReader();
            MIMER.IEndCriteriaStrategy endofmessage = new BasicEndOfMessageStrategy();
            var stream = (Stream)new MemoryStream(Encoding.UTF8.GetBytes(input));

            var message = reader.ReadMimeMessage(ref stream, endofmessage);
            stream.Close();

            foreach(var attachment in message.Attachments)
            {
                if(attachment.Type == "text" && attachment.SubType.StartsWith("htm"))
                {
                    return Encoding.UTF8.GetString(attachment.Data);
                }
            }

            return "<html><body>There is no HTML message in this message.</body></html>";
        }
    }
}
