using Assets.Characters;
using Assets.Enums;
using RTS_Cam;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class TeamDraft : MonoBehaviour
{
    public GameObject Camera;
    public GameObject BattleUI;
    public GameObject BattleButtons;
    public GameObject QueueUI;

    public Text PlayerName;
    public Text Resources;
    public GameObject Combat;
    public GameObject TeamDraftObject;
    private List<Player> players;

    public Text RhinoCounter;
    public Text JumperCounter;
    public Text ShieldCounter;
    public Text TowerCounter;
    public Text CatterpillarCounter;

    public int RhinoCost = 10;
    public int JumperCost = 10;
    public int TowerCost = 25;
    public int CatterpillarCost = 30;
    public int ShieldCost = 25;

    public List<Player> Players
    {
        get
        {
            return players;
        }
        set
        {
            players = value;
            UpdateLabels();
        }
    }

    private void TogglePlayers()
    {
        var p = Players[0];
        Players[0] = Players[1];
        Players[1] = p;

        UpdateLabels();
        SetCounters();
    }

    private void UpdateLabels()
    {
        PlayerName.text = $"Player {Players[0].Name} is choosing";
        Resources.text = $"Resources: {Players[0].Resources}";
    }

    private void SetCounters()
    {
        RhinoCounter.text = Players[0].Team.Where(x => x == CharacterEnum.Rhino).Count().ToString();
        JumperCounter.text = Players[0].Team.Where(x => x == CharacterEnum.Jumper).Count().ToString();
        ShieldCounter.text = Players[0].Team.Where(x => x == CharacterEnum.Shield).Count().ToString();
        TowerCounter.text = Players[0].Team.Where(x => x == CharacterEnum.Tower).Count().ToString();
        CatterpillarCounter.text = Players[0].Team.Where(x => x == CharacterEnum.Worm).Count().ToString();
    }

    public void OnAddRhino()
    {
        if (Players[0].Resources >= RhinoCost)
        {
            Players[0].Team.Add(CharacterEnum.Rhino);
            Players[0].Resources -= RhinoCost;
            TogglePlayers();
        }
    }
    
    public void OnAddTower()
    {
        if (Players[0].Resources >= TowerCost)
        {
            Players[0].Team.Add(CharacterEnum.Tower);
            Players[0].Resources -= TowerCost;
            TogglePlayers();
        }
    }

    public void OnAddCatterpillar()
    {
        if (Players[0].Resources >= CatterpillarCost)
        {
            Players[0].Team.Add(CharacterEnum.Worm);
            Players[0].Resources -= CatterpillarCost;
            TogglePlayers();
        }
    }

    public void OnAddJumper()
    {
        if (Players[0].Resources >= JumperCost)
        {
            Players[0].Team.Add(CharacterEnum.Jumper);
            Players[0].Resources -= JumperCost;
            TogglePlayers();
        }
    }
    public void OnAddShield()
    {
        if (Players[0].Resources >= ShieldCost)
        {
            Players[0].Team.Add(CharacterEnum.Shield);
            Players[0].Resources -= ShieldCost;
            TogglePlayers();
        }
    }

    public void OnSkip()
    {
        TogglePlayers();
    }

    public void OnConfirm()
    {
        var c = Instantiate(Combat);
        var combat = c.GetComponent<Combat>();
        combat.Player1 = Players.FirstOrDefault(x => x.ID == 0);
        combat.Player2 = Players.FirstOrDefault(x => x.ID == 1);
        combat.Queue = QueueUI.GetComponent<QueueScript>();
        combat.BattleUIScript = BattleUI.GetComponent<BattleUIScript>();

        TeamDraftObject.SetActive(false);

        Camera.GetComponent<RTS_Camera>().enabled = true;
        BattleUI.SetActive(true);
        BattleButtons.GetComponent<BattleButtons>().Combat = combat;
    }
}