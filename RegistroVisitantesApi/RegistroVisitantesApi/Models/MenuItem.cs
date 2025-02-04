using System;
using System.Collections.Generic;

namespace RegistroVisitantesApi.Models;

public partial class MenuItem
{
    public decimal Id { get; set; }

    public string Nombre { get; set; } = null!;

    public string Url { get; set; } = null!;

    public decimal? PadreId { get; set; }

    public virtual ICollection<MenuItem> InversePadre { get; set; } = new List<MenuItem>();

    public virtual MenuItem? Padre { get; set; }
}
