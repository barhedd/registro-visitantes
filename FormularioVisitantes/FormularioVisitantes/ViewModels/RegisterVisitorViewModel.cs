namespace FormularioVisitantes.ViewModels
{
    public class RegisterVisitorViewModel
    {
        public string Dui { get; set; } = String.Empty;
        public string Nombre { get; set; } = String.Empty;
        public string Email { get; set; } = String.Empty;
        public DateTime FechaNacimiento { get; set; } = DateTime.Now;
        public string Telefono { get; set; } = String.Empty;

        public RegisterVisitorViewModel(string _Dui, string _Nombre, string _Email, DateTime _FechaNacimiento, string _Telefono) { 
            Dui = _Dui;
            Nombre = _Nombre;
            Email = _Email;
            FechaNacimiento = _FechaNacimiento;
            Telefono = _Telefono;
        }

    }
}
