using System.Threading.Tasks;

namespace ApplicationCommon.MailPassword
{
    public interface IMailService
    {
        Task SendEmailAsync(MailModel mail);

    }
}
