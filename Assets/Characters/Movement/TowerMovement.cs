using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Characters
{
	class TowerMovement : CharacterMovement
    {
        public override void SetCoorinates(Vector2Int centerCoord)
        {
            Coordinates = new Vector2Int[]
            {
                centerCoord
            };
        }

        public override Vector2Int[] GetFutureCoordinates(Vector2Int futureCoords)
        {
            return new Vector2Int[] { futureCoords };
        }
		public override Vector2Int[] GetAttackArea()
		{
			var coords = Coordinates[0];
			switch (Rotation)
			{
				case GridRotation.Up:
					return new Vector2Int[]
					{
						coords + new Vector2Int(0,1),
						coords + new Vector2Int(0,2),
						coords + new Vector2Int(0,3),
						coords + new Vector2Int(0,4),
						coords + new Vector2Int(0,5)
					};
				case GridRotation.Down:
					return new Vector2Int[]
					{
						coords - new Vector2Int(0,1),
						coords - new Vector2Int(0,2),
						coords - new Vector2Int(0,3),
						coords - new Vector2Int(0,4),
						coords - new Vector2Int(0,5)
					};
				case GridRotation.Left:
					return new Vector2Int[]
					{
						coords - new Vector2Int(1,0),
						coords - new Vector2Int(2,0),
						coords - new Vector2Int(3,0),
						coords - new Vector2Int(4,0),
						coords - new Vector2Int(5,0)
					};
				case GridRotation.Right:
					return new Vector2Int[]
					{
						coords + new Vector2Int(1,0),
						coords + new Vector2Int(2,0),
						coords + new Vector2Int(3,0),
						coords + new Vector2Int(4,0),
						coords + new Vector2Int(5,0)
					};
			}
			return base.GetAttackArea();
		}
	}
}
