using System;
using System.Threading.Tasks;
using FirebaseAdmin.Messaging;
using FirebaseAdmin;
using Google.Apis.Auth.OAuth2;

namespace Utils.Implementation
{
    

    public class FirebaseNotificationService
    {
        private readonly FirebaseMessaging _messaging;

        public FirebaseNotificationService()
        {
            if (FirebaseApp.DefaultInstance == null)
            {
                string filePath = Path.Combine(Directory.GetCurrentDirectory(), "Resources", "criptasservice-firebase-adminsdk.json");
                FirebaseApp.Create(new AppOptions()
                {
                    Credential = GoogleCredential.FromFile(filePath)
                });
            }

            _messaging = FirebaseMessaging.DefaultInstance;
        }

        public async Task<bool> EnviarNotificacionAsync(string? token, string titulo, string mensaje)
        {
            try
            {
                var message = new Message()
                {
                    Token = token,
                    Notification = new Notification()
                    {
                        Title = titulo,
                        Body = mensaje
                    },
                    Data = new Dictionary<string, string>()
            {
                { "tipo", "PAGO_COMPLETADO" }
            }
                };
                string response = await _messaging.SendAsync(message);
                Console.WriteLine($"Mensaje enviado correctamente: {response}");
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al enviar la notificación: {ex.Message}");
                return false;
            }
        }
    }

}
