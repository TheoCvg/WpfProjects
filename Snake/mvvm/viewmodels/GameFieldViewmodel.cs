using Snake.abstractions;
using Snake.commands;
using Snake.mvvm.models;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using System.Windows.Threading;

namespace Snake.mvvm.viewmodels
{
	public class GameFieldViewmodel : ViewmodelBase<GameField>
	{
		public List<IObject> GameObjects
		{
			get => Model.GameObjects;
			set { Model.GameObjects = value; OnPropertyChanged(); }
		}

		private DispatcherTimer _timer;

		#region Commands

		public ICommand KeyUpCommand { get; }

		#endregion

		[SetsRequiredMembers]
		public GameFieldViewmodel()
		{
			Model = new GameField();
			var snake = new SnakeViewmodel() { Position = new Point(10, 10) };
			GameObjects.Add(snake);

			_timer = new DispatcherTimer
			{
				Interval = TimeSpan.FromSeconds(1)
			};
			_timer.Tick += TimeUpdate;
			_timer.Start();

			KeyUpCommand = new RelayCommand(OnKeyUp, _ => true);

			//SpawnFood();
		}

		private void OnKeyUp(object? parameter)
		{
			if (parameter is not Key key)
				return;

			foreach (var listener in GameObjects.OfType<IKeyboardListener>())
			{
				listener.OnKeyUp(key);
			}
		}

		private void SpawnFood()
		{
			Food food = new() { Position = new Point(20, 20) };
			GameObjects.Add(new FoodViewmodel(food));
		}

		private void TimeUpdate(object? sender, EventArgs e)
		{
			foreach (var gameObject in GameObjects)
			{
				gameObject.Update();

				if(gameObject is IMovable movable)
				{
					movable.Move();
				}
			}

			GameObjects.OfType<SnakeViewmodel>().FirstOrDefault()?.Grow();
		}
	}
}
