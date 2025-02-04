using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RegistroVisitantesApi.Models;

public partial class Visitante
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public decimal Id { get; set; }

    public string Dui { get; set; } = null!;

    public string Nombre { get; set; } = null!;

    public string Email { get; set; } = null!;

    public DateTime FechaNacimiento { get; set; }

    public string Telefono { get; set; } = null!;

    public string? Generacion { get; set; }
}
