using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Snake.mvvm.viewmodels;
public class ViewmodelBase<T> : INotifyPropertyChanged where T : class
{
	public event PropertyChangedEventHandler? PropertyChanged;

	public void OnPropertyChanged([CallerMemberName] string? property = null)
	{
		PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(property));	
	}

	public required T Model { get; set; }
}
