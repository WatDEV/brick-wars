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
		private List<CharacterAttributes> characters = new List<CharacterAttributes>();
		private readonly int threshold = 1000;

		public void Init(List<Character> chars)
		{			
			foreach(var c in chars)
			{
				c.CharacterAttributes.turnTimer = 0;
				characters.Add(c.CharacterAttributes);
			}
		}

		public CharacterAttributes Next()
		{
			while (true)
			{
				foreach (var d in characters)
				{
					if (d.turnTimer >= threshold)
					{
						d.turnTimer -= threshold;
						return d;
					}
				}

				foreach (var d in characters)
				{
					d.turnTimer += d.movementSpeed;
				}
			}
		}

	}
}
