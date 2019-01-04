using Assets.Characters;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BattleUIScript : MonoBehaviour
{
    //public GameObject AttackButton;
    public Text MobilityLeft;
    public Button AttackButton;
    
    public void UpdateCharacterOnTurnState(Character character)
    {
        AttackButton.interactable = !character.CharacterAttributes.hasAttacked;
        MobilityLeft.text = $"Tiles left: {character.CharacterAttributes.mobilityLeft}";
    }

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
