using UnityEngine;

namespace Assets.Characters
{
    public class Character
    {
        public readonly CharacterHandler CharacterHandler;
        public readonly CharacterHighlight CharacterHighlight;
        public readonly GameObject CharacterActor;

        public Character(CharacterHandler characterHandler, CharacterHighlight characterHighlight, GameObject characterActor)
        {
            CharacterActor = characterActor;
			CharacterHighlight = characterHighlight;
			CharacterHandler = characterHandler;
			CharacterHandler.IsSelected = () => CharacterHighlight.State == CharacterHighlightEnum.Selected;
		}
	}
}
