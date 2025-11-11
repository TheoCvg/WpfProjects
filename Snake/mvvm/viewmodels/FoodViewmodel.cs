using Snake.abstractions;
using Snake.mvvm.models;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Snake.mvvm.viewmodels
{
	public class FoodViewmodel : ViewmodelBase<Food>, IObject
	{
		[SetsRequiredMembers]
		public FoodViewmodel(Food food)
		{
			Model = food;
		}

		public Point Position
		{
			get => Model.Position;
			set { Model.Position = value; OnPropertyChanged(); }
		}

		public void Update()
		{

		}
	}
}
