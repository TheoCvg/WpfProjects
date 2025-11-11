using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace Snake.behaviors
{
	public static class KeyUpBehavior
	{
		public static readonly DependencyProperty CommandProperty =
			DependencyProperty.RegisterAttached(
				"Command",
				typeof(ICommand),
				typeof(KeyUpBehavior),
				new PropertyMetadata(null, OnCommandChanged));

		public static void SetCommand(DependencyObject obj, ICommand value) =>
			obj.SetValue(CommandProperty, value);

		public static ICommand GetCommand(DependencyObject obj) =>
			(ICommand)obj.GetValue(CommandProperty);

		private static void OnCommandChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
		{
			if (d is UIElement element)
			{
				element.KeyUp -= OnKeyUp;
				if (e.NewValue != null)
					element.KeyUp += OnKeyUp;
			}
		}

		private static void OnKeyUp(object sender, KeyEventArgs e)
		{
			if (sender is UIElement element)
			{
				var command = GetCommand(element);
				if (command?.CanExecute(e.Key) == true)
					command.Execute(e.Key);
			}
		}
	}
}
