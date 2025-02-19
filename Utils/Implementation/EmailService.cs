
using MailKit.Net.Smtp;
using MailKit.Security;
using MimeKit;
using Microsoft.Extensions.Options;
using Models.Models;
using Models;
namespace Utils.Implementation
{
    public class EmailService
    {
        private readonly EmailSettings _emailSettings;

        public EmailService(IOptions<EmailSettings> emailSettings)
        {
            _emailSettings = emailSettings.Value;
        }

        /// <summary>
        /// Función genérica para enviar correos electrónicos.
        /// </summary>
        private async Task EnviarCorreoAsync(string destinatario, string nombreDestinatario, string asunto, string htmlBody)
        {
            try
            {
                var mensaje = new MimeMessage();
                mensaje.From.Add(new MailboxAddress(_emailSettings.FromName, _emailSettings.FromEmail));
                mensaje.To.Add(new MailboxAddress(nombreDestinatario, destinatario));
                mensaje.Subject = asunto;

                var bodyBuilder = new BodyBuilder { HtmlBody = htmlBody };
                mensaje.Body = bodyBuilder.ToMessageBody();

                using var smtp = new SmtpClient();
                await smtp.ConnectAsync(_emailSettings.SmtpServer, _emailSettings.SmtpPort, SecureSocketOptions.Auto);
                await smtp.AuthenticateAsync(_emailSettings.SmtpUser, _emailSettings.SmtpPassword);
                await smtp.SendAsync(mensaje);
                await smtp.DisconnectAsync(true);

                Console.WriteLine($"Correo enviado a {destinatario}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al enviar correo a {destinatario}: {ex.Message}");
            }
        }

        /// <summary>
        /// Envía un correo de bienvenida al cliente.
        /// </summary>
        public async Task EnviarCorreoBienvenida(EntClientes cliente)
        {
            string htmlBody = EmailTemplateService.GetBienvenidaTemplate(cliente);
            await EnviarCorreoAsync(cliente.sEmail, $"{cliente.sNombre} {cliente.sApellidos}", "¡Bienvenido a Criptas!", htmlBody);
        }

        /// <summary>
        /// Envía un correo de notificación de pago.
        /// </summary>
        public async Task EnviarCorreoPago(EntClientes cliente, string estadoPago, decimal monto, string referencia)
        {
            string htmlBody = EmailTemplateService.GetPagoTemplate(cliente, estadoPago, monto, referencia);
            string asunto = estadoPago switch
            {
                "CREADO" => "Pago Registrado",
                "APLICADO" => "Pago Aplicado",
                "LIQUIDADO" => "Pago Liquidado",
                _ => "Notificación de Pago"
            };

            await EnviarCorreoAsync(cliente.sEmail, $"{cliente.sNombre} {cliente.sApellidos}", asunto, htmlBody);
        }

        /// <summary>
        /// Envía un correo notificando una visita a la cripta de un fallecido.
        /// </summary>
        public async Task EnviarCorreoVisita(string emailDestinatario, string nombreVisitante, string nombreFallecido, DateTime horaVisita, string mensaje)
        {
            string htmlBody = EmailTemplateService.GetVisitaTemplate(nombreFallecido, horaVisita, nombreVisitante, mensaje);
            await EnviarCorreoAsync(emailDestinatario, nombreVisitante, $"Visita realizada a {nombreFallecido}", htmlBody);
        }

    }

}
