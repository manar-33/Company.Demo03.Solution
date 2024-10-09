using Company.Demo03.DAL.Models;
using Microsoft.Identity.Client.Platforms.Features.DesktopOs.Kerberos;
using System.Net;
using System.Net.Mail;

namespace Company.Demo03.PL.Helper
{
	public static class EmailSettings
	{
		public static void SendEmail(Email email)
		{
			//Mail Server : gmail.com
			//smtp
			var client = new SmtpClient("smtp.gmail.com",587);
			client.EnableSsl=true;
			client.Credentials = new NetworkCredential("manarmahmoud696@gmail.com","hxhaszpdoaramkcx");
			client.Send("manarmahmoud696@gmail.com",email.To, email.Subject,email.Body);

		}
	}
}
