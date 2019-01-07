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

            Vector2Int coords1, coords2;
			switch (Rotation)
			{
				case GridRotation.Up:
					coords1 = Coordinates.FirstOrDefault(c => c.y == GetMaxY());
                    coords2 = coords1;
					coords1.y += 1;
                    coords2.y += 2;
					return new Vector2Int[]
					{
						coords1,
                        coords2
					};
				case GridRotation.Down:
					coords1 = Coordinates.FirstOrDefault(c => c.y == GetMinY());
                    coords2 = coords1;
                    coords1.y -= 1;
                    coords2.y -= 2;
                    return new Vector2Int[]
                    {
                        coords1,
                        coords2
                    };
				case GridRotation.Left:
					coords1 = Coordinates.FirstOrDefault(c => c.x == GetMinX());
                    coords2 = coords1;
                    coords1.x -= 1;
                    coords2.x -= 2;
                    return new Vector2Int[]
                    {
                        coords1,
                        coords2
                    };
				case GridRotation.Right:
					coords1 = Coordinates.FirstOrDefault(c => c.x == GetMaxX());
                    coords2 = coords1;
                    coords1.x += 1;
                    coords2.x += 2;
                    return new Vector2Int[]
                    {
                        coords1,
                        coords2
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
            var attackArea = GetAttackArea();
            attackArea.OrderByDescending(x => Vector2Int.Distance(x, GetCenterCoord()));

            damages.Add(new Tuple<Vector2Int, int>(attackArea[0], Damage));
            damages.Add(new Tuple<Vector2Int, int>(attackArea[1], Damage / 3));
            return damages;
        }

        public override Vector2Int[] GetPathToNeighbourCoordIfPossible(Vector2Int coord)
        {
            var centerCoord = GetCenterCoord();
            if (Rotation == GridRotation.Up || Rotation == GridRotation.Down)
            {
                if (Vector2Int.Distance(centerCoord, coord) == 1 && (coord.x == centerCoord.x + 1 || coord.x == centerCoord.x - 1))
                    return new Vector2Int[] { coord };
                else if (Vector2Int.Distance(centerCoord, coord) == 2 && coord.y == centerCoord.y + 2)
                    return new Vector2Int[]
                    {
                            new Vector2Int(centerCoord.x, centerCoord.y + 1),
                            coord
                    };
                else if (Vector2Int.Distance(centerCoord, coord) == 2 && coord.y == centerCoord.y - 2)
                    return new Vector2Int[]
                    {
                            new Vector2Int(centerCoord.x, centerCoord.y - 1),
                            coord
                    };
            }
            else
            {
                if (Vector2Int.Distance(centerCoord, coord) == 1 && (coord.y == centerCoord.y + 1 || coord.y == centerCoord.y - 1))
                    return new Vector2Int[] { coord };
                else if (Vector2Int.Distance(centerCoord, coord) == 2 && coord.x == centerCoord.x + 2)
                    return new Vector2Int[]
                    {
                            new Vector2Int(centerCoord.x + 1, centerCoord.y),
                            coord
                    };
                else if (Vector2Int.Distance(centerCoord, coord) == 2 && coord.x == centerCoord.x - 2)
                    return new Vector2Int[]
                    {
                            new Vector2Int(centerCoord.x - 1, centerCoord.y),
                            coord
                    };
            }

            return null;
        }
    }
}
