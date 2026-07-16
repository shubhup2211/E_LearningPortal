using MailKit.Net.Smtp;
using MailKit.Security;
using MimeKit;

namespace ELearningPortal.Helpers
{
    // Simple email helper using MailKit. Reads settings from appsettings.json (Smtp section).
    public class EmailHelper
    {
        private readonly IConfiguration _config;

        public EmailHelper(IConfiguration config)
        {
            _config = config;
        }

        // Send a plain HTML email. Safe: if SMTP is not configured we just log and skip so app never crashes.
        public void SendEmail(string toEmail, string subject, string htmlBody)
        {
            try
            {
                var smtp = _config.GetSection("Smtp");
                var host = smtp["Host"];
                var port = int.Parse(smtp["Port"] ?? "587");
                var user = smtp["Username"];
                var pass = smtp["Password"];
                var fromEmail = smtp["FromEmail"] ?? user;
                var fromName = smtp["FromName"] ?? "ELearning Portal";

                if (string.IsNullOrWhiteSpace(host) || string.IsNullOrWhiteSpace(user) || string.IsNullOrWhiteSpace(pass))
                {
                    // SMTP not configured yet – skip silently (dev-friendly).
                    Console.WriteLine($"[EmailHelper] SMTP not configured. Would send to {toEmail}: {subject}");
                    return;
                }

                var message = new MimeMessage();
                message.From.Add(new MailboxAddress(fromName, fromEmail));
                message.To.Add(MailboxAddress.Parse(toEmail));
                message.Subject = subject;

                var builder = new BodyBuilder { HtmlBody = htmlBody };
                message.Body = builder.ToMessageBody();

                using var client = new SmtpClient();
                client.Connect(host, port, SecureSocketOptions.StartTls);
                client.Authenticate(user, pass);
                client.Send(message);
                client.Disconnect(true);
            }
            catch (Exception ex)
            {
                // Don't blow up the request just because email failed
                Console.WriteLine($"[EmailHelper] Failed to send email: {ex.Message}");
            }
        }

        // Ready-made template for sending login credentials to a newly created user.
        public void SendCredentials(string toEmail, string fullName, string password, string role, string branchName)
        {
            var subject = "Your ELearning Portal account is ready";
            var body = $@"
                <div style='font-family:Arial,sans-serif;line-height:1.5'>
                    <h2>Welcome, {fullName}!</h2>
                    <p>Your account has been created on the ELearning Portal.</p>
                    <table style='border-collapse:collapse'>
                        <tr><td><b>Role</b></td><td>{role}</td></tr>
                        <tr><td><b>Branch</b></td><td>{branchName}</td></tr>
                        <tr><td><b>Email</b></td><td>{toEmail}</td></tr>
                        <tr><td><b>Password</b></td><td>{password}</td></tr>
                    </table>
                    <p>Please login and change your password as soon as possible.</p>
                    <p>Regards,<br/>ELearning Team</p>
                </div>";
            SendEmail(toEmail, subject, body);
        }
    }
}
