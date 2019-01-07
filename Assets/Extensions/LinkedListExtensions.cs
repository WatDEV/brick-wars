using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Extensions
{
    public static class LinkedListExtensions
	{
		private static Random rng = new Random();
		public static void RemoveAll<T>(this LinkedList<T> linkedList,
                                        Func<T, bool> predicate)
        {
            for (LinkedListNode<T> node = linkedList.First; node != null;)
            {
                LinkedListNode<T> next = node.Next;
                if (predicate(node.Value))
                    linkedList.Remove(node);
                node = next;
            }
        }

		public static void Shuffle<T>(this IList<T> list)
		{
			int n = list.Count;
			while (n > 1)
			{
				n--;
				int k = rng.Next(n + 1);
				T value = list[k];
				list[k] = list[n];
				list[n] = value;
			}
		}
	}
}
