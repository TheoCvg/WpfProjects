using Snake.enums;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Snake.abstractions
{
	public interface IMovable
	{
		public float Speed { get; set; }
		Direction Direction { get; set; }
		void Move();
	}
}
