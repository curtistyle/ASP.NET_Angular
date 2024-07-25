using System;
using System.Collections.Generic;

namespace WebApplicationGlosario.Models;

public partial class Ingles
{
    public int IdIngles { get; set; }

    public string? Palabra { get; set; }

    public string? CategoriaGramatical { get; set; }

    public virtual ICollection<Espanol> Espanols { get; set; } = new List<Espanol>();
}
