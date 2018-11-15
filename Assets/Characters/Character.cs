using UnityEngine;

namespace Assets.Characters
{
    public class Character
    {
        public readonly CharacterMovement CharacterMovement;
        public readonly CharacterHighlight CharacterHighlight;
        public readonly CharacterAttributes CharacterAttributes;
        public readonly GameObject CharacterActor;

        public Character(CharacterMovement characterMovement, CharacterHighlight characterHighlight, GameObject characterActor, CharacterAttributes characterAttributes)
        {
            CharacterActor = characterActor;
			CharacterHighlight = characterHighlight;
            CharacterMovement = characterMovement;
            CharacterAttributes = characterAttributes;
            CharacterMovement.IsSelected = () => CharacterHighlight.State == CharacterHighlightEnum.Selected;
		}
	}
}
