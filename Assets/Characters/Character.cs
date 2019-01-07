using Assets.Enums;
using UnityEngine;

namespace Assets.Characters
{
    public class Character
    {
        public readonly CharacterMovement CharacterMovement;
        public readonly CharacterHighlight CharacterHighlight;
        public readonly CharacterAttributes CharacterAttributes;
        public readonly GameObject CharacterActor;
        public readonly CharacterEnum Type;
        public readonly int Team;

        public Character(CharacterMovement characterMovement, CharacterHighlight characterHighlight, GameObject characterActor, CharacterAttributes characterAttributes, CharacterEnum type, int team)
        {
            CharacterActor = characterActor;
			CharacterHighlight = characterHighlight;
            CharacterMovement = characterMovement;
            CharacterAttributes = characterAttributes;
            CharacterMovement.IsSelected = () => CharacterHighlight.State == CharacterHighlightEnum.Selected;
			CharacterMovement.attributes = CharacterAttributes;
            Type = type;
            Team = team;
		}
	}
}
