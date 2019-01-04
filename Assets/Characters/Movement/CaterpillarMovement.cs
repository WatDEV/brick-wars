using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Assets.Characters
{
    class CaterpillarMovement : CharacterMovement
	{
        public override void SetCoordinates(Vector2Int centerCoord)
        {
            switch (Rotation)
            {
                case GridRotation.Up:
                    Coordinates = new Vector2Int[]
                    {
                        new Vector2Int
                        {
                            x=centerCoord.x,
                            y=centerCoord.y-1
                        },
                        centerCoord,
                        new Vector2Int
                        {
                            x=centerCoord.x,
                            y=centerCoord.y+1
                        }
                    };
                    break;
                case GridRotation.Down:
                    Coordinates = new Vector2Int[]
                    {
                        new Vector2Int
                        {
                            x=centerCoord.x,
                            y=centerCoord.y-1
                        },
                        centerCoord,
                        new Vector2Int
                        {
                            x=centerCoord.x,
                            y=centerCoord.y+1
                        }
                    };
                    break;
                case GridRotation.Left:
                    Coordinates = new Vector2Int[]
                    {
                       new Vector2Int
                        {
                            x=centerCoord.x-1,
                            y=centerCoord.y
                        },
                        centerCoord,
                        new Vector2Int
                        {
                            x=centerCoord.x+1,
                            y=centerCoord.y
                        }
                    };
                    break;
                case GridRotation.Right:
                    Coordinates = new Vector2Int[]
                    {
                       new Vector2Int
                        {
                            x=centerCoord.x-1,
                            y=centerCoord.y
                        },
                        centerCoord,
                        new Vector2Int
                        {
                            x=centerCoord.x+1,
                            y=centerCoord.y
                        }
                    };
                    break;
            }
        }

        public override Vector2Int[] GetCoordinates(Vector2Int centerCoord)
        {
            switch (Rotation)
            {
                case GridRotation.Up:
                    return new Vector2Int[]
                    {
                        new Vector2Int
                        {
                            x=centerCoord.x,
                            y=centerCoord.y-1
                        },
                        centerCoord,
                        new Vector2Int
                        {
                            x=centerCoord.x,
                            y=centerCoord.y+1
                        }
                    };
                case GridRotation.Down:
                    return new Vector2Int[]
                    {
                        new Vector2Int
                        {
                            x=centerCoord.x,
                            y=centerCoord.y-1
                        },
                        centerCoord,
                        new Vector2Int
                        {
                            x=centerCoord.x,
                            y=centerCoord.y+1
                        }
                    };
                case GridRotation.Left:
                    return new Vector2Int[]
                    {
                       new Vector2Int
                        {
                            x=centerCoord.x-1,
                            y=centerCoord.y
                        },
                        centerCoord,
                        new Vector2Int
                        {
                            x=centerCoord.x+1,
                            y=centerCoord.y
                        }
                    };
                case GridRotation.Right:
                    return new Vector2Int[]
                    {
                       new Vector2Int
                        {
                            x=centerCoord.x-1,
                            y=centerCoord.y
                        },
                        centerCoord,
                        new Vector2Int
                        {
                            x=centerCoord.x+1,
                            y=centerCoord.y
                        }
                    };
            }
            return null;
        }

        public override Vector2Int[] GetFutureCoordinates(Vector2Int futureCoords)
        {
            switch (Rotation)
            {
                case GridRotation.Up:
                    return new Vector2Int[]
                    {
                        new Vector2Int
                        {
                            x=futureCoords.x,
                            y=futureCoords.y-1
                        },
                        futureCoords,
                        new Vector2Int
                        {
                            x=futureCoords.x,
                            y=futureCoords.y+1
                        }
                    };
                case GridRotation.Down:
                    return new Vector2Int[]
                    {
                        new Vector2Int
                        {
                            x=futureCoords.x,
                            y=futureCoords.y-1
                        },
                        futureCoords,
                        new Vector2Int
                        {
                            x=futureCoords.x,
                            y=futureCoords.y+1
                        }
                    };
                case GridRotation.Left:
                    return new Vector2Int[]
                    {
                        new Vector2Int
                        {
                            x=futureCoords.x-1,
                            y=futureCoords.y
                        },
                        futureCoords,
                        new Vector2Int
                        {
                            x=futureCoords.x+1,
                            y=futureCoords.y
                        }
                    };
                case GridRotation.Right:
                    return new Vector2Int[]
                    {
                        new Vector2Int
                        {
                            x=futureCoords.x-1,
                            y=futureCoords.y
                        },
                        futureCoords,
                        new Vector2Int
                        {
                            x=futureCoords.x+1,
                            y=futureCoords.y
                        }
                    };
            }

            return null;
        }
		public override Vector2Int[] GetAttackArea()
        {
            if (Coordinates == null)
                return null;

            Vector2Int coords;
			switch (Rotation)
			{
				case GridRotation.Up:
					coords = Coordinates.FirstOrDefault(c => c.y == GetMaxY());
					coords.y += 1;
					return new Vector2Int[]
					{
						coords
					};
				case GridRotation.Down:
					coords = Coordinates.FirstOrDefault(c => c.y == GetMinY());
					coords.y -= 1;
					return new Vector2Int[]
					{
						coords
					};
				case GridRotation.Left:
					coords = Coordinates.FirstOrDefault(c => c.x == GetMinX());
					coords.x -= 1;
					return new Vector2Int[]
					{
						coords
					};
				case GridRotation.Right:
					coords = Coordinates.FirstOrDefault(c => c.x == GetMaxX());
					coords.x += 1;
					return new Vector2Int[]
					{
						coords
					};
			}
			return base.GetAttackArea();
		}
		private int GetMaxX()
		{
			var max = -1;
			foreach (var c in Coordinates)
			{
				max = max < c.x ? c.x : max;
			}
			return max;
		}
		private int GetMinX()
		{
			var min = 9999;
			foreach (var c in Coordinates)
			{
				min = min > c.x ? c.x : min;
			}
			return min;
		}
		private int GetMaxY()
		{
			var max = -1;
			foreach (var c in Coordinates)
			{
				max = max < c.y ? c.y : max;
			}
			return max;
		}
		private int GetMinY()
		{
			var min = 9999;
			foreach (var c in Coordinates)
			{
				min = min > c.y ? c.y : min;
			}
			return min;
        }
        protected override List<Tuple<Vector2Int, int>> GetDamage()
        {
            var damages = new List<Tuple<Vector2Int, int>>();
            foreach (var c in GetAttackArea())
            {
                damages.Add(new Tuple<Vector2Int, int>(c, Damage));
            }
            return damages;
        }
    }
}
