namespace MVC.Core.Entities.Culture
{
	using System;
	using System.Collections.Generic;
	using System.Linq;
	using System.Text;
	using System.Threading.Tasks;

	public class Language : BaseEntity
	{
        public string Name { get; set; }

        public string LanguageCode { get; set; }
	}
}
