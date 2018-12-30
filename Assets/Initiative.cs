using Assets.Characters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InitativeNamespace
{
	public class Initiative
	{
		private List<Character> characters;
		private readonly int threshold = 1000;

		public void Init(List<Character> chars)
		{
			characters = chars;
			foreach(var c in characters)
			{
				c.CharacterAttributes.turnTimer = 0;
			}
		}

		public Character Next()
		{
			while (true)
			{
				foreach (var d in characters)
				{
					if (d.CharacterAttributes.hitPoints <= 0)
						continue;

					if (d.CharacterAttributes.turnTimer >= threshold)
					{
						d.CharacterAttributes.turnTimer -= threshold;
						return d;
					}
				}

				foreach (var d in characters)
				{
					if (d.CharacterAttributes.hitPoints <= 0)
						continue;

					d.CharacterAttributes.turnTimer += d.CharacterAttributes.movementSpeed;
				}
			}
		}

	}
}
