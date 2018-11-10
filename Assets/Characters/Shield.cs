using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Characters
{
	class Shield : CharacterHandler
	{
        public override void SetCoorinates(Vector2Int centerCoord)
        {
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
        }

        public override Vector2Int[] GetFutureCoordinates(Vector2Int futureCoords)
        {
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
		public override Vector2Int[] GetAttackArea()
		{
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
		private Vector2Int GetCenterCoord()
		{
			var totalX = 0;
			var totalY = 0;

			foreach (var c in Coordinates)
			{
				totalX += c.x;
				totalY += c.y;
			}
			return new Vector2Int(totalX / 3, totalY / 3);
		}
	}
}
