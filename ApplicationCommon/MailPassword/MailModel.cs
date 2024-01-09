namespace ApplicationCommon.MailPassword
{
    public class MailModel
    {
        public MailModel(string toEmail, string subject,string body)
        {
            ToEmail = toEmail;
            Subject = subject;
            Body = body;

        }
        public string ToEmail { get; private set; }
        public string Subject { get; private set; }
        public string Body { get; private set; }
    }
}
