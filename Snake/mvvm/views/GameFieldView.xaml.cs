using Snake.mvvm.viewmodels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Snake.mvvm.views
{
	public partial class GameFieldView : UserControl
	{
		public GameFieldView()
		{
			InitializeComponent();
			Loaded += (_, _) =>
			{
				GameArea.Focus(); 
			};
		}
	}
}
