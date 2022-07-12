using System;

namespace com.tictactoe.game
{
	[Serializable]
	public class TileData : IEquatable<TileData>, IComparable<TileData>
	{
		public enum Value
		{
			O = -1,
			Empty = 0,
			X = 1
		}

		public Value value;

		public TileData()
		{
			value = Value.Empty;
		}

		public static bool operator ==(TileData t1, TileData t2)
		{
			return t1.Equals(t2);
		}

		public static bool operator !=(TileData t1, TileData t2)
		{
			return !t1.Equals(t2);
		}

		public override bool Equals(object obj)
		{
			//Check for null and compare run-time types.
			if ((obj == null) || !GetType().Equals(obj.GetType()))
			{
				return false;
			}
			else
			{
				TileData t = (TileData)obj;
				return value == t.value;
			}
		}

		public bool Equals(TileData other)
		{
			return value == other.value;
		}

		public override int GetHashCode()
		{
			return value.GetHashCode();
		}

		public int CompareTo(TileData other)
		{
			return value - other.value;
		}

		public bool IsEmpty => value == Value.Empty;
		public bool IsO => value == Value.O;
		public bool IsX => value == Value.X;
	}

	public class TilePosition
	{
		public int col;
		public int row;

		public TilePosition(int col, int row)
		{
			this.col = col;
			this.row = row;
		}

		public TilePosition SetDefault()
		{
			col = -1;
			row = -1;
			return this;
		}
	};
}
