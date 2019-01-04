using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Characters
{
	class JumperMovement : CharacterMovement
	{
        public override void SetCoordinates(Vector2Int centerCoord)
        {
            Coordinates = new Vector2Int[]
            {
                centerCoord
            };
        }
        public override Vector2Int[] GetCoordinates(Vector2Int centerCoord)
        {
            return new Vector2Int[]
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
            if (Coordinates == null)
                return null;

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
