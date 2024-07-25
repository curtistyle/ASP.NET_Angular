using System.ComponentModel.DataAnnotations;
using System;
using System.Collections.Generic;

namespace WebApplicationGlosario.Models.ViewModels
{
    public sealed class GlosarioViewModel
    {
        [Required]
        [Display(Name ="P. en Ingles")]
        public string? palabraIngles {  get; set; }

        [Required]
        [Display(Name ="Categoria Gramatical")]
        public string? categoriaGramatical { get;set; }

        [Required]
        [Display(Name ="Significado en Español")]
        public string? palabrasEspanol {  get; set; }

        public List<Ingles>? inglesTable { get; set; }

    }
}
