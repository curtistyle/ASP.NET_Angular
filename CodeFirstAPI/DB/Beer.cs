using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DB
{
	public class Beer
	{
		[Key]
		[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
		public int BeerID {  get; set; }

		public string Name { get; set;}

		public int BrandID { get; set;}

		[ForeignKey(nameof(BrandID))]
		public virtual Brand Brand { get; set; }
	}
}
