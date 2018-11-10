using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Characters
{
	class Caterpillar : CharacterHandler
	{
        public override void SetCoorinates(Vector2Int centerCoord)
        {
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
        }

        public override Vector2Int[] GetFutureCoordinates(Vector2Int futureCoords)
        {
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
		public override Vector2Int[] GetAttackArea()
		{
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
					coords.x -= 2;
					coords.y += 1;
					return new Vector2Int[]
					{
						coords
					};
				case GridRotation.Right:
					coords = Coordinates.FirstOrDefault(c => c.x == GetMaxX());
					coords.x += 2;
					coords.y += 1;
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
	}
}
