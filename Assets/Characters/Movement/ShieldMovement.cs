using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Characters
{
	class ShieldMovement : CharacterMovement
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
                case GridRotation.Down:
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
                case GridRotation.Left:
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
                case GridRotation.Right:
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
                case GridRotation.Down:
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
                case GridRotation.Left:
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
                case GridRotation.Right:
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
                case GridRotation.Down:
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
                case GridRotation.Left:
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
                case GridRotation.Right:
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
            }
            return null;
        }
		public override Vector2Int[] GetAttackArea()
        {
            if (Coordinates == null)
                return null;
            var coords = GetCenterCoord();
			switch (Rotation)
			{
				case GridRotation.Up:
					return new Vector2Int[]
					{
						coords + new Vector2Int(-1,1),
						coords + new Vector2Int(-1,2),
						coords + new Vector2Int(-1,3),
						coords + new Vector2Int(0,1),
						coords + new Vector2Int(0,2),
						coords + new Vector2Int(0,3),
						coords + new Vector2Int(1,1),
						coords + new Vector2Int(1,2),
						coords + new Vector2Int(1,3)
					};
				case GridRotation.Down:
					return new Vector2Int[]
					{
						coords - new Vector2Int(-1,1),
						coords - new Vector2Int(-1,2),
						coords - new Vector2Int(-1,3),
						coords - new Vector2Int(0,1),
						coords - new Vector2Int(0,2),
						coords - new Vector2Int(0,3),
						coords - new Vector2Int(1,1),
						coords - new Vector2Int(1,2),
						coords - new Vector2Int(1,3)
					};
				case GridRotation.Left:
					return new Vector2Int[]
					{
						coords - new Vector2Int(1,-1),
						coords - new Vector2Int(2,-1),
						coords - new Vector2Int(3,-1),
						coords - new Vector2Int(1,0),
						coords - new Vector2Int(2,0),
						coords - new Vector2Int(3,0),
						coords - new Vector2Int(1,1),
						coords - new Vector2Int(2,1),
						coords - new Vector2Int(3,1)
					};
				case GridRotation.Right:
					return new Vector2Int[]
					{
						coords + new Vector2Int(1,-1),
						coords + new Vector2Int(2,-1),
						coords + new Vector2Int(3,-1),
						coords + new Vector2Int(1,0),
						coords + new Vector2Int(2,0),
						coords + new Vector2Int(3,0),
						coords + new Vector2Int(1,1),
						coords + new Vector2Int(2,1),
						coords + new Vector2Int(3,1)
					};
			}
			return base.GetAttackArea();
		}
        protected override List<Tuple<Vector2Int, int>> GetDamage()
        {
            var damages = new List<Tuple<Vector2Int, int>>();
            foreach (var c in GetAttackArea())
            {
                damages.Add(new Tuple<Vector2Int, int>(c, Damage * (int)Math.Round(Vector2Int.Distance(GetCenterCoord(),c))));
            }
            return damages;
        }

        public override Vector2Int[] GetPathToNeighbourCoordIfPossible(Vector2Int coord)
        {
            var centerCoord = GetCenterCoord();
            if (Rotation == GridRotation.Left || Rotation == GridRotation.Right)
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
