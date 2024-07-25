using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DB
{
	public class Brand
	{
		[Key] // Le da una referencia a BranID que va a ser Primary Key, y luego ...
		[DatabaseGenerated(DatabaseGeneratedOption.Identity)] // .. esto le dice que va a hacer autoincrementable
		public int BrandID { get; set; }

		public string Name { get; set; }

		public virtual ICollection<Beer> Beers { get; set; }
	}
}
