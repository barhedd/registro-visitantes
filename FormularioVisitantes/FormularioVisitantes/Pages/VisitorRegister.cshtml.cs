using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel.DataAnnotations;
using System.Text.Json;

namespace FormularioVisitantes.Pages
{
    public class VisitorRegisterModel : PageModel
    {
        [BindProperty]
        [Required(ErrorMessage = "El DUI es obligatorio.")]
        [RegularExpression(@"^\d{9}$", ErrorMessage = "El DUI debe contener 9 caracteres")]
        public string Dui { get; set; } =  string.Empty;

        [BindProperty]
        [Required(ErrorMessage = "El Nombre es obligatorio.")]
        public string Name { get; set; } = string.Empty;

        [BindProperty]
        [Required(ErrorMessage = "El Email es obligatorio.")]
        [RegularExpression(@"^[\w.-]+@[a-zA-Z\d.-]+\.[a-zA-Z]{2,10}$", ErrorMessage = "Ingrese un Email válido")]
        public string Email { get; set; } = string.Empty;

        [BindProperty]
        [Required(ErrorMessage = "La Fecha de Nacimiento es obligatoria.")]
        public DateTime Birthday { get; set; } = DateTime.Now;

        [BindProperty]
        [Required(ErrorMessage = "El Teléfono es obligatorio.")]
        [RegularExpression(@"^\d{8}$", ErrorMessage = "El Teléfono debe contener 8 caracteres")]
        public string Phone { get; set; } = string.Empty;

        // Variable para mostrar el modal
        public bool ShowModal { get; set; } = false;

        private readonly HttpClient _httpClient = new();


        public void OnGet()
        {
        }

        public async Task<IActionResult> OnPostAddNewVisitor()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }


            var visitorRegister = new
            {
                Dui,
                Nombre = Name,
                Email,
                FechaNacimiento = Birthday,
                Telefono = Phone
            };

            var json = JsonSerializer.Serialize(visitorRegister);

            var content = new StringContent(json, System.Text.Encoding.UTF8, "application/json");

            var response = await _httpClient.PostAsync("http://localhost:5215/visitantes", content);

            if (!response.IsSuccessStatusCode)
            {
                return Page();
            }

            // Simulación de guardado en base de datos
            ShowModal = true;

            // Limpiar los valores del formulario
            Dui = string.Empty;
            Name = string.Empty;
            Email = string.Empty;
            Phone = string.Empty;
            Birthday = DateTime.Now; // O establecerlo en vacío si prefieres

            return Page();
        }
    }
}
