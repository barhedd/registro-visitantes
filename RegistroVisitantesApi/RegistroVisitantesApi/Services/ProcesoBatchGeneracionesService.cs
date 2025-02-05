using Microsoft.EntityFrameworkCore;
using RegistroVisitantesApi.Models;

namespace RegistroVisitantesApi.Services
{
    public class ProcesoBatchGeneracionesService : BackgroundService
    {
        private readonly IServiceProvider _serviceProvider;

        public ProcesoBatchGeneracionesService(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            // Intervalo de ejecución del proceso (por ejemplo, cada 24 horas)
            var intervalo = TimeSpan.FromMinutes(2);

            while (!stoppingToken.IsCancellationRequested)
            {
                // Ejecutar el proceso batch
                await EjecutarProcesoBatch();

                // Esperar antes de ejecutar nuevamente
                await Task.Delay(intervalo, stoppingToken);
            }
        }

        private async Task EjecutarProcesoBatch()
        {
            using var scope = _serviceProvider.CreateScope();
            var context = scope.ServiceProvider.GetRequiredService<ModelContext>();

            var visitantesSinGeneracion = context.Visitantes
                .Where(v => string.IsNullOrEmpty(v.Generacion))
                .ToList();

            foreach (var visitante in visitantesSinGeneracion)
            {
                visitante.Generacion = DeterminarGeneracion(visitante.FechaNacimiento);
            }

            await context.SaveChangesAsync();
            Console.WriteLine("Proceso batch ejecutado.");
        }

        private string DeterminarGeneracion(DateTime fechaNacimiento)
        {
            int año = fechaNacimiento.Year;

            if (año >= 1949 && año <= 1968) return "Baby Boomers";
            if (año >= 1969 && año <= 1980) return "Generación X";
            if (año >= 1981 && año <= 1993) return "Millennials";
            if (año >= 1994 && año <= 2009) return "Generación Z";
            if (año >= 2010) return "Generación Alpha";

            return "Desconocida";
        }
    }
}
