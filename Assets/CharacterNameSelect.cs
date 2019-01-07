using Assets.Characters;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class CharacterNameSelect : MonoBehaviour {

    public GameObject TeamDraft;
    public GameObject TeamDraftButtons;
    public GameObject PlayerNameSelect;
    public InputField PlayerName;
    public Text Label;

    private List<Player> players = new List<Player>();
    
	public void ChooseName()
    {
        players.Add(new Player
        {
            ID = players.Count,
            Name = PlayerName.text,
            Resources = 100,
            Team = new List<Assets.Enums.CharacterEnum>()
        });

        if(players.Count == 2)
        {
            TeamDraft.SetActive(true);
            TeamDraftButtons.GetComponent<TeamDraft>().Players = players;
            PlayerNameSelect.SetActive(false);
        }

        Label.text = "Player 2 select your name:";

        PlayerName.Select();
        PlayerName.text = "";
    }

	public void Exit()
	{
		SceneManager.LoadScene("StartMenu");
	}
}
