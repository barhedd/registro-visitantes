using FormularioVisitantes.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using System.Net.Http;
using System.Text.Json;
using System.Text;

namespace FormularioVisitantes.Services
{
    public interface IVisitorsService
    {
        Task<HttpStatusCode> RegisterVisitor(RegisterVisitorViewModel visitor);
    }
    public class VisitorsService : IVisitorsService
    {
        private readonly HttpClient _httpClient;

        public VisitorsService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        [HttpPost]
        public async Task<HttpStatusCode> RegisterVisitor(RegisterVisitorViewModel visitor)
        {
            var json = JsonSerializer.Serialize(visitor);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            var response = await _httpClient.PostAsync("http://localhost:5215/visitantes", content);

            return response.StatusCode;
        }
    }
}
