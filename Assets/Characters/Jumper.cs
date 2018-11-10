using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Characters
{
	class Jumper : CharacterHandler
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
					coords.y += 1;
					return new Vector2Int[]
					{
						coords
					};
				case GridRotation.Down:
					coords.y -= 1;
					return new Vector2Int[]
					{
						coords
					};
				case GridRotation.Left:
					coords.x -= 1;
					return new Vector2Int[]
					{
						coords
					};
				case GridRotation.Right:
					coords.x += 1;
					return new Vector2Int[]
					{
						coords
					};
			}
			return base.GetAttackArea();
		}
	}
}
