using Snake.abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Snake.mvvm.models
{
	public class GameField
	{
		public List<IObject> GameObjects { get; set; } = [];
	}
}
