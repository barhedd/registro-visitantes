using FormularioVisitantes.Services;
using FormularioVisitantes.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel.DataAnnotations;

namespace FormularioVisitantes.Pages
{
    public class VisitorRegisterModel : PageModel
    {

        private readonly IVisitorsService _visitorsService;

        [BindProperty]
        [Required(ErrorMessage = "El DUI es obligatorio")]
        [RegularExpression(@"^\d{9}$", ErrorMessage = "El DUI debe contener 9 caracteres")]
        public string Dui { get; set; } =  string.Empty;

        [BindProperty]
        [Required(ErrorMessage = "El Nombre es obligatorio")]
        public string Name { get; set; } = string.Empty;

        [BindProperty]
        [Required(ErrorMessage = "El Email es obligatorio")]
        [RegularExpression(@"^[\w.-]+@[a-zA-Z\d.-]+\.[a-zA-Z]{2,10}$", ErrorMessage = "Ingrese un Email válido")]
        public string Email { get; set; } = string.Empty;

        [BindProperty]
        [Required(ErrorMessage = "La Fecha de Nacimiento es obligatoria")]
        public DateTime Birthday { get; set; } = DateTime.Now;

        [BindProperty]
        [Required(ErrorMessage = "El Teléfono es obligatorio")]
        [RegularExpression(@"^\d{8}$", ErrorMessage = "El Teléfono debe contener 8 caracteres")]
        public string Phone { get; set; } = string.Empty;

        // Variable para mostrar el modal
        public bool ShowModal { get; set; } = false;

        public VisitorRegisterModel(IVisitorsService visitorsService)
        {
            _visitorsService = visitorsService;
        }

        public void OnGet()
        {
        }

        public async Task<IActionResult> OnPostAddNewVisitor()
        {
            // Si el modelo no es válido, detenemos la función
            if (!ModelState.IsValid)
            {
                return Page();
            }

            // Registramos el visitante en la base de datos
            RegisterVisitorViewModel visitor = new(Dui, Name, Email, Birthday, Phone);

            var response = await _visitorsService.RegisterVisitor( visitor );
               
            if (response != System.Net.HttpStatusCode.OK)
            {
                return Page();
            }

            ShowModal = true;

            // Limpiamos los valores del formulario
            Dui = string.Empty;
            Name = string.Empty;
            Email = string.Empty;
            Phone = string.Empty;
            Birthday = DateTime.Now;

            return Page();
        }
    }
}
