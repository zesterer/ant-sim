using System;

namespace anti_sim
{
	public struct Vec2
	{
		public int x;
		public int y;

		public Vec2(int x, int y)
		{
			this.x = x;
			this.y = y;
		}

		public static Vec2 operator +(Vec2 vec0, Vec2 vec1)
		{
			return new Vec2(vec0.x + vec1.x, vec0.y + vec1.y);
		}

		public static Vec2 operator -(Vec2 vec0, Vec2 vec1)
		{
			return new Vec2(vec0.x - vec1.x, vec0.y - vec1.y);
		}

		public static Vec2 operator *(Vec2 vec0, Vec2 vec1)
		{
			return new Vec2(vec0.x * vec1.x, vec0.y * vec1.y);
		}

		public static Vec2 operator /(Vec2 vec0, Vec2 vec1)
		{
			return new Vec2(vec0.x / vec1.x, vec0.y / vec1.y);
		}
	}
}

