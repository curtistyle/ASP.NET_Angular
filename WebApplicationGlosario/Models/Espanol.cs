using System;
using System.Collections.Generic;

namespace WebApplicationGlosario.Models;

public partial class Espanol
{
    public int IdEspanol { get; set; }

    public int? IdIngles { get; set; }

    public string? Palabra { get; set; }

    public virtual Ingles? IdInglesNavigation { get; set; }
}
