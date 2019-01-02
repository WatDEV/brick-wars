using Assets.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Characters
{
    public class Player
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public int Resources { get; set; }
        public List<CharacterEnum> Team { get; set; }
    }
}
