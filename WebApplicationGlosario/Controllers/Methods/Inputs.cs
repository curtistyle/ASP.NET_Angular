using WebApplicationGlosario.Models.ViewModels;

namespace WebApplicationGlosario.Controllers.Methods
{
	public static class Inputs
	{
		public static string[] Divide(this string[] values)
		{
			string[] result =
			values.Select(value =>
			{
				if (value.StartsWith(' '))
				{
					return value.Remove(0, 1);
				}
				if (value.EndsWith(' '))
				{
					return value.Remove(0, value.Length);
				}
				return value;
			}).ToArray();
			return result;
		}

		public static void Display(this GlosarioViewModel model)
		{
			Console.WriteLine($"P. en Ingles: {model.palabraIngles}");
			Console.WriteLine($"Categoria gramatical: {model.categoriaGramatical}");
			Console.WriteLine($"P. en Espanol: {model.palabrasEspanol}");
		}
	}
}
