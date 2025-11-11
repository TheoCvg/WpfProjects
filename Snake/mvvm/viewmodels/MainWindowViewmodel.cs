using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Snake.mvvm.viewmodels
{
	public class MainWindowViewmodel
	{
		public GameFieldViewmodel GameFiledViewmodel { get; }
		public MainWindowViewmodel()
		{
			GameFiledViewmodel = new GameFieldViewmodel();
		}
	}
}
