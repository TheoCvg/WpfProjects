using Snake.abstractions;
using Snake.enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;

namespace Snake.mvvm.models
{
	public class Snake
	{

		#region Properties

		public Point Position
		{
			get => SnakeParts[0].Position;
			set { SnakeParts[0].Position = value; }
		}

		public float Speed { get; set; } = 5;
		public Direction Direction { get; set; } = Direction.ToRight;
		public List<SnakePart> SnakeParts { get; set; } = [];
		public int Length => SnakeParts.Count;

		#endregion
	}

	public class SnakePart
	{
		public SnakePart(Snake snake, SnakePart? parentSnakePart)
		{
			Snake = snake;
			ParentSnakePart = parentSnakePart;
		}

		#region Properties

		public readonly Snake Snake;
		public readonly SnakePart? ParentSnakePart;

		public Point Position { get; set; }
		public Color Color { get; set; } = Color.FromRgb(100, 100, 100);
		public int Size { get; set; } = 5;

		#endregion
	}
}
