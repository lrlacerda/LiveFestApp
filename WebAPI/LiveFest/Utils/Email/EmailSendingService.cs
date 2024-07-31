﻿namespace LiveFest.Utils.Email
{
    public class EmailSendingService
    {
        private readonly IEmailService _emailService;

        public EmailSendingService(IEmailService service)
        {
            _emailService = service;
        }

        public async Task SendWelcomeEmail(string email, string username)
        {
            try
            {
                MailRequest request = new MailRequest
                {
                    ToEmail = email,
                    Subject = "Seja Muito Bem-Vindo ao LiveFest",
                    Body = GetHtmlContent(username)
                };
                await _emailService.SendEmailAsync(request);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task SendRecoveryPassword(int codigo, string email)
        {
            try
            {
                MailRequest request = new MailRequest
                {
                    ToEmail = email,
                    Subject = "Código de Recuperação",
                    Body = GetHtmlContentRecovery(codigo)
                };

                await _emailService.SendEmailAsync(request);
            }
            catch (Exception)
            {
                throw;
            }
        }
        private string GetHtmlContent(string userName)
        {
            string Response = @"
<div style=""width:100%; padding: 20px;"">
    <div style=""max-width: 600px; margin: 0 auto; border-radius: 10px; padding: 20px; background-color:#FFFFFF;"">
        <div style=""background: url('https://livefest.blob.core.windows.net/bloblivefestcontainer/Splash%20screen.png') no-repeat center center; background-size: cover; padding: 20px;"">
            <img src=""https://livefest.blob.core.windows.net/bloblivefestcontainer/NewLogoI%20-%20LiveFest.png"" alt=""Logotipo do LiveFest"" style=""display: block; margin: 0 auto; max-width: 200px;"" />
            <h1 style=""color: #ffffff; text-align: center;"">Bem-vindo ao LiveFest!</h1>
            <p style=""color: #ffffff; text-align: center;"">Olá <strong>" + userName + @"</strong>,</p>
            <p style=""color: #ffffff; text-align: center;"">Estamos muito felizes por você ter se inscrito na plataforma LiveFest.</p>
            <p style=""color: #ffffff; text-align: center;"">Explore todas as funcionalidades que oferecemos e encontre os melhores eventos.</p>
            <p style=""color: #ffffff; text-align: center;"">Se tiver alguma dúvida ou precisar de assistência, nossa equipe de suporte está sempre pronta para ajudar.</p>
            <p style=""color: #ffffff; text-align: center;"">Aproveite sua experiência conosco!</p>
            <p style=""color: #ffffff; text-align: center;"">Atenciosamente,<br>Equipe LiveFest</p>
        </div>
    </div>
</div>";

            return Response;
        }

        private string GetHtmlContentRecovery(int codigo)
        {
            string Response = @"
<div style=""width:100%; padding: 20px;"">
    <div style=""max-width: 600px; margin: 0 auto; border-radius: 10px; padding: 20px; background-color:#FFFFFF;"">
        <div style=""background: url('https://livefest.blob.core.windows.net/bloblivefestcontainer/Splash%20screen.png') no-repeat center center; background-size: cover; padding: 20px;"">
            <img src=""https://livefest.blob.core.windows.net/bloblivefestcontainer/NewLogoI%20-%20LiveFest.png"" alt=""Logotipo do LiveFest"" style=""display: block; margin: 0 auto; max-width: 200px;"" />
            <h1 style=""color: #ffffff; text-align: center;"">Recuperação de senha</h1>
            <p style=""color: #ffffff; font-size: 24px; text-align: center;"">Código de confirmação <strong>" + codigo + @"</strong></p>
        </div>
    </div>
</div>";

            return Response;
        }

    }
}
