using System;

namespace AntSim
{
    namespace Common
    {
        public struct Vec2
        {
            public int x;
            public int y;

            public Vec2(int i)
            {
                this.x = i;
                this.y = i;
            }

			public Vec2(int x, int y)
			{
				this.x = x;
				this.y = y;
			}

            public int X
            {
                get { return this.x; }
                set { this.x = value; }
            }

            public int Y
            {
                get { return this.y; }
                set { this.y = value; }
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

			public static Vec2 operator %(Vec2 vec0, Vec2 vec1)
			{
				return new Vec2(vec0.x % vec1.x, vec0.y % vec1.y);
			}
        }
    }
}