using Assets.Characters;
using Assets.Extensions;
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

        public LinkedList<Character> Queue;

		public void Init(List<Character> chars)
		{
			characters = chars;
            Queue = new LinkedList<Character>();
			foreach(var c in characters)
			{
				c.CharacterAttributes.turnTimer = 0;
                c.CharacterAttributes.RemoveFromQueue += (character) =>
                {
                    Queue.Remove(c);
                    characters.Remove(c);
                };
			}

            CalculateQueue();
		}

		public Character Next()
		{
            var first = Queue.First();
            Queue.RemoveFirst();

            CalculateQueue();

            return first;
		}

        private void CalculateQueue()
        {
            /*Queue.RemoveAll(x => x.CharacterAttributes == null || x.CharacterAttributes.hitPoints <= 0);
            characters.RemoveAll(x => x.CharacterAttributes.hitPoints <= 0);*/

            while (Queue.Count < 11)
            {
                foreach (var d in characters)
                {
                    if (d.CharacterAttributes.turnTimer >= threshold)
                    {
                        d.CharacterAttributes.turnTimer -= threshold;
                        Queue.AddLast(d);
                    }
                }

                foreach (var d in characters)
                {
                    d.CharacterAttributes.turnTimer += d.CharacterAttributes.movementSpeed;
                }
            }
        }
	}
}
