using Microsoft.Win32;
using Snake.abstractions;
using Snake.commands;
using Snake.enums;
using Snake.mvvm.models;
using Snake.mvvm.viewmodels;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Navigation;

namespace Snake.mvvm.viewmodels
{
	public class SnakeViewmodel : ViewmodelBase<models.Snake>, IObject, IMovable, IKeyboardListener
	{
		#region Ctors

		[SetsRequiredMembers]
		public SnakeViewmodel()
		{
			Model = new models.Snake();

			var newPart = new SnakePartViewmodel(Model);
			SnakeParts.Add(newPart);
			Model.SnakeParts.Add(newPart.Model);
		}

		public SnakeViewmodel(models.Snake snake)
		{
			Model = snake;
			foreach (var part in Model.SnakeParts)
			{
				SnakeParts.Add(new SnakePartViewmodel(part));
			}
		}

		#endregion

		#region Properties

		public ObservableCollection<SnakePartViewmodel> SnakeParts { get; } = [];
		public Point Position
		{
			get => SnakeParts[0].Position;
			set { SnakeParts[0].Position = value; OnPropertyChanged(); }
		}

		public float Speed
		{
			get => Model.Speed;
			set { Model.Speed = value; OnPropertyChanged(); }
		}

		public Direction Direction
		{
			get => Model.Direction;
			set { Model.Direction = value; OnPropertyChanged(); }
		}

		public int Length => Model.Length;


		#endregion

		public void Grow()
		{
			//var last = SnakeParts.Last();
			//var newPart = last.Grow(Model);

			//var offset = GetOffsetFromDirection(Direction, last);
			//newPart.Position = new Point(last.Position.X - offset.X, last.Position.Y - offset.Y);

			//last.PositionChanged += (prev, current) =>
			//{
			//	newPart.Position = new Point(prev.X, prev.Y);
			//};

			//SnakeParts.Add(newPart);
			//Model.SnakeParts.Add(newPart.Model);

			var last = SnakeParts.Last();
			var newPart = last.Grow(Model);

			last.PositionChanged += (prev, current)
				=> newPart.Position = new Point(prev.X, prev.Y);

			SnakeParts.Add(newPart);
			Model.SnakeParts.Add(newPart.Model);
		}

		public void Move()
		{
			switch (Direction)
			{
				case Direction.Upwards:
					Position = new Point(Position.X, Position.Y - Speed);
					break;
				case Direction.Downwards:
					Position = new Point(Position.X, Position.Y + Speed);
					break;
				case Direction.ToLeft:
					Position = new Point(Position.X - Speed, Position.Y);
					break;
				case Direction.ToRight:
					Position = new Point(Position.X + Speed, Position.Y);
					break;

				default:
					return;
			}
		}

		public void Update()
		{

		}

		private Point GetOffsetFromDirection(Direction direction, SnakePartViewmodel parentPart)
		{
			return direction switch
			{
				Direction.Upwards => new Point(0, -parentPart.Size),
				Direction.Downwards => new Point(0, parentPart.Size),
				Direction.ToLeft => new Point(-parentPart.Size, 0),
				Direction.ToRight => new Point(parentPart.Size, 0),
				_ => new Point(0, 0)
			};
		}

		#region Commands impl

		public void OnKeyUp(Key key)
		{
			switch (key)
			{
				case Key.Up when Direction != Direction.Downwards:
					Direction = Direction.Upwards;
					break;
				case Key.Down when Direction != Direction.Upwards:
					Direction = Direction.Downwards;
					break;
				case Key.Left when Direction != Direction.ToRight:
					Direction = Direction.ToLeft;
					break;
				case Key.Right when Direction != Direction.ToLeft:
					Direction = Direction.ToRight;
					break;
			}

			#endregion
		}
	}

	public static class SnakePartViewmodelExtensions
	{
		public static SnakePartViewmodel Grow(this SnakePartViewmodel parent, models.Snake snake)
		{
			var newPart = new SnakePart(snake, parent.Model);
			var newVm = new SnakePartViewmodel(newPart)
			{
				Position = new Point(
					parent.Position.X - parent.Size,
					parent.Position.Y
				)
			};

			return newVm;
		}
	}

	public class SnakePartViewmodel : ViewmodelBase<SnakePart>
	{
		#region Ctors

		[SetsRequiredMembers]
		public SnakePartViewmodel(models.Snake snake)
		{
			Model = new(snake, null);
		}

		[SetsRequiredMembers]
		public SnakePartViewmodel(SnakePart snakePart)
		{
			Model = snakePart;
		}

		#endregion

		#region Properties

		public Action<Point, Point>? PositionChanged;

		public Point Position
		{
			get => Model.Position;
			set
			{
				var prev = Model.Position; Model.Position = value; OnPropertyChanged(); PositionChanged?.Invoke(prev, value);
			}
		}

		public Color Color
		{
			get => Model.Color;
			set { Model.Color = value; OnPropertyChanged(); }
		}

		public int Size
		{
			get => Model.Size;
			set { Model.Size = value; OnPropertyChanged(); }
		}

		#endregion
	}
}
