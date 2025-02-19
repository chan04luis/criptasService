
using Models.Models;

namespace Utils.Implementation
{
    public class EmailTemplateService
    {
        public static string GetBienvenidaTemplate(EntClientes cliente)
        {
            return $@"
        <!DOCTYPE html>
        <html>
        <head>
            <meta charset='UTF-8'>
            <meta name='viewport' content='width=device-width, initial-scale=1.0'>
            <title>Bienvenido a Criptas</title>
            <style>
                body {{
                    font-family: Arial, sans-serif;
                    background-color: #f4f4f4;
                    margin: 0;
                    padding: 0;
                }}
                .container {{
                    max-width: 600px;
                    margin: 20px auto;
                    background: white;
                    padding: 20px;
                    border-radius: 8px;
                    box-shadow: 0px 0px 10px rgba(0, 0, 0, 0.1);
                }}
                .header {{
                    background: #007bff;
                    color: white;
                    padding: 15px;
                    text-align: center;
                    border-top-left-radius: 8px;
                    border-top-right-radius: 8px;
                }}
                .content {{
                    padding: 20px;
                    font-size: 16px;
                    color: #333;
                }}
                .footer {{
                    text-align: center;
                    padding: 10px;
                    font-size: 14px;
                    color: #777;
                }}
                .button {{
                    display: inline-block;
                    padding: 10px 20px;
                    margin: 20px 0;
                    text-decoration: none;
                    color: white;
                    background-color: #28a745;
                    border-radius: 5px;
                }}
            </style>
        </head>
        <body>
            <div class='container'>
                <div class='header'>
                    <h2>¡Bienvenido, {cliente.sNombre}!</h2>
                </div>
                <div class='content'>
                    <p>Hola {cliente.sNombre},</p>
                    <p>Nos alegra que formes parte de nuestra comunidad. Estamos aquí para ofrecerte el mejor servicio.</p>
                    <p>Si tienes alguna duda, no dudes en contactarnos.</p>
                </div>
                <div class='footer'>
                    <p>&copy; {DateTime.Now.Year} Gownetwork | Todos los derechos reservados</p>
                </div>
            </div>
        </body>
        </html>";
        }

        public static string GetPagoTemplate(EntClientes cliente, string estadoPago, decimal monto, string referencia)
        {
            string mensajeEstado = estadoPago switch
            {
                "CREADO" => $"Tu pago ha sido registrado correctamente. Aún está pendiente de confirmación.",
                "APLICADO" => $"Tu pago ha sido procesado con éxito y ha sido aplicado a tu cuenta.",
                "LIQUIDADO" => $"Felicidades, tu cuenta ha sido liquidada completamente.",
                _ => "Estado de pago desconocido."
            };

            string colorEstado = estadoPago switch
            {
                "CREADO" => "#ffc107", // Amarillo
                "APLICADO" => "#17a2b8", // Azul
                "LIQUIDADO" => "#28a745", // Verde
                _ => "#6c757d" // Gris
            };

            return $@"
            <!DOCTYPE html>
            <html>
            <head>
                <meta charset='UTF-8'>
                <meta name='viewport' content='width=device-width, initial-scale=1.0'>
                <title>Notificación de Pago</title>
                <style>
                    body {{
                        font-family: Arial, sans-serif;
                        background-color: #f4f4f4;
                        margin: 0;
                        padding: 0;
                    }}
                    .container {{
                        max-width: 600px;
                        margin: 20px auto;
                        background: white;
                        padding: 20px;
                        border-radius: 8px;
                        box-shadow: 0px 0px 10px rgba(0, 0, 0, 0.1);
                    }}
                    .header {{
                        background: {colorEstado};
                        color: white;
                        padding: 15px;
                        text-align: center;
                        border-top-left-radius: 8px;
                        border-top-right-radius: 8px;
                    }}
                    .content {{
                        padding: 20px;
                        font-size: 16px;
                        color: #333;
                    }}
                    .footer {{
                        text-align: center;
                        padding: 10px;
                        font-size: 14px;
                        color: #777;
                    }}
                    .button {{
                        display: inline-block;
                        padding: 10px 20px;
                        margin: 20px 0;
                        text-decoration: none;
                        color: white;
                        background-color: {colorEstado};
                        border-radius: 5px;
                    }}
                </style>
            </head>
            <body>
                <div class='container'>
                    <div class='header'>
                        <h2>Notificación de Pago</h2>
                    </div>
                    <div class='content'>
                        <p>Hola {cliente.sNombre} {cliente.sApellidos},</p>
                        <p>{mensajeEstado}</p>
                        <p><strong>Monto:</strong> {monto:C}</p>
                        <p><strong>Referencia:</strong> {referencia}</p>
                        <p>Si tienes alguna duda, no dudes en contactarnos.</p>
                    </div>
                    <div class='footer'>
                        <p>&copy; {DateTime.Now.Year} Gownetwork | Todos los derechos reservados</p>
                    </div>
                </div>
            </body>
            </html>";
        }

        public static string GetVisitaTemplate(string nombreFallecido, DateTime horaVisita, string nombreVisitante, string mensaje)
        {
            return $@"
                <!DOCTYPE html>
                <html>
                <head>
                    <meta charset='UTF-8'>
                    <meta name='viewport' content='width=device-width, initial-scale=1.0'>
                    <title>Registro de Visita a Cripta</title>
                    <style>
                        body {{
                            font-family: Arial, sans-serif;
                            background-color: #f4f4f4;
                            margin: 0;
                            padding: 0;
                        }}
                        .container {{
                            max-width: 600px;
                            margin: 20px auto;
                            background: white;
                            padding: 20px;
                            border-radius: 8px;
                            box-shadow: 0px 0px 10px rgba(0, 0, 0, 0.1);
                        }}
                        .header {{
                            background: #007bff;
                            color: white;
                            padding: 15px;
                            text-align: center;
                            border-top-left-radius: 8px;
                            border-top-right-radius: 8px;
                        }}
                        .content {{
                            padding: 20px;
                            font-size: 16px;
                            color: #333;
                        }}
                        .footer {{
                            text-align: center;
                            padding: 10px;
                            font-size: 14px;
                            color: #777;
                        }}
                        .highlight {{
                            font-weight: bold;
                            color: #0056b3;
                        }}
                    </style>
                </head>
                <body>
                    <div class='container'>
                        <div class='header'>
                            <h2>Registro de Visita</h2>
                        </div>
                        <div class='content'>
                            <p>Se ha registrado una nueva visita a la cripta de:</p>
                            <p class='highlight'>{nombreFallecido}</p>
                            <p><strong>Hora de Visita:</strong> {horaVisita.ToString("HH:mm dd/MM/yyyy")}</p>
                            <p><strong>Visitante:</strong> {nombreVisitante}</p>
                            <p><strong>Mensaje dejado:</strong></p>
                            <blockquote style='background: #f9f9f9; padding: 10px; border-left: 4px solid #007bff;'>
                                {mensaje}
                            </blockquote>
                        </div>
                        <div class='footer'>
                            <p>&copy; {DateTime.Now.Year} Gownetwork | Todos los derechos reservados</p>
                        </div>
                    </div>
                </body>
                </html>";
        }

    }

}
