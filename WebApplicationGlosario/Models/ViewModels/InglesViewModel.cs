using System.ComponentModel.DataAnnotations;

namespace WebApplicationGlosario.Models.ViewModels
{
	public class InglesViewModel
	{
		public int Id { get; set; }

		[Required]
		[Display(Name ="P. en Ingles")]
		public string? Ingles { get; set; }

		[Required]
		[Display(Name ="Categoria Gramatical")]
		public string? CategoriaGramatical { get; set; }

		[Required]
		[Display(Name ="Significado")]
		public string? Espanol {  get; set; }
	}
}
