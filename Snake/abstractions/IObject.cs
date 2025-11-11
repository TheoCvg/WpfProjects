using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Snake.abstractions
{
	public interface IObject
	{
		Point Position { get; set; }
		void Update();
	}
}
