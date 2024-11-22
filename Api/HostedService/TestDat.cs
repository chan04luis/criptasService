namespace Api.HostedService
{
    public class TestDat : BackgroundService
    {
        private readonly IServiceProvider _serviceProvider;

        public TestDat(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            using (var scope = _serviceProvider.CreateScope())
            {
                var clientesRepositorio = scope.ServiceProvider.GetRequiredService<IClientesRepositorio>();
                while (!stoppingToken.IsCancellationRequested)
                {
                    try
                    {
                        // Llama al repositorio para obtener la lista de clientes
                        var response = await clientesRepositorio.DGetList();

                        if (!response.HasError)
                        {
                            Console.WriteLine($"Clientes obtenidos: {response.Result.Count}");
                        }
                        else
                        {
                            Console.WriteLine($"Error: {response.Message}");
                        }
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine($"Error en la tarea de fondo: {ex.Message}");
                    }

                    // Espera antes de la siguiente ejecución (puedes cambiar el intervalo)
                    await Task.Delay(TimeSpan.FromSeconds(15), stoppingToken);
                }
            }
                
        }
    }
}
