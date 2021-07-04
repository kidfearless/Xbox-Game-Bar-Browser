using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App1.Models
{
	public class URLBuilder
	{
		public static Uri Parse(string source)
		{
			var trimmed = source.Trim();

			if (trimmed.Contains(" ") || !trimmed.Contains("."))
			{
				return null;
			}

			if (source.Contains(':'))
			{
				return new Uri(source);
			}

			return new Uri("http://" + source);
		}
	}
}
