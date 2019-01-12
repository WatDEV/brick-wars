using Assets.Characters;
using Assets.Enums;
using InitativeNamespace;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class Combat : MonoBehaviour
{
    public Player Player1;
    public Player Player2;

    public GameObject EndGameScreen;
    public GameObject BattleUI;
    public Text WinnerText;

    public GameObject GridPrefab;

    public GameObject RhinoPrefab;
    public GameObject TowerPrefab;
    public GameObject ShieldPrefab;
    public GameObject CatterpillarPrefab;
    public GameObject JumperPrefab;
    public QueueScript Queue;
    public BattleUIScript BattleUIScript;

    private GridMaker grid;
    private List<Character> team1;
    private List<Character> team2;

    private Initiative initative;

    public Character SelectedCharacter
    {
        get
        {
            foreach (var c in team1)
                if (c.CharacterHighlight.State == CharacterHighlightEnum.Selected)
                    return c;

            foreach (var c in team2)
                if (c.CharacterHighlight.State == CharacterHighlightEnum.Selected)
                    return c;
            return null;
        }
    }

    void Start()
    {
        var g = Instantiate(GridPrefab, new Vector3(0, 0, 0), Quaternion.identity);
        grid = g.GetComponent<GridMaker>();
        grid.MoveCharacter += MoveCharacter;
        grid.GetSelectedCharacter += () => { return SelectedCharacter; };
        grid.GetCharacters += GetCharacters;
        grid.IsFirstTurn += IsFirstTurn;
        grid.SetFirstPosition += SetInitialCharacterPosition;

        team1 = new List<Character>();
        team2 = new List<Character>();

        InitializeTeams();

        var teams = new List<Character>();
        teams.AddRange(team1);
        teams.AddRange(team2);
        initative = new Initiative();
        initative.Init(teams);

        Next();
    }

    public void Next()
    {
        while (true)
        {
            Queue.UpdateSprites(initative.Queue);

            var characterOnTurn = initative.Next();

            if (characterOnTurn.CharacterAttributes.isStunned == false)
            {
                characterOnTurn.CharacterAttributes.NewTurn();
                characterOnTurn.CharacterHighlight.Select();

                BattleUIScript.UpdateCharacterOnTurnState(characterOnTurn);
                break;
            }
            else
            {
                characterOnTurn.CharacterAttributes.isStunned = false;
            }
        }

       
    }

    private void FinishGame(string winnerName)
    {
        WinnerText.text = $"Player {winnerName} has won!";
        BattleUI.SetActive(false);
        EndGameScreen.SetActive(true);

        SelectedCharacter.CharacterAttributes.mobilityLeft = 0;
        foreach(var c in GetCharacters())
            c.CharacterMovement.IsGameOver = true;
    }

    private void InitializeTeams()
    {
        var i = 0;
        foreach (var teamMember in Player1.Team)
        {
            var m = Instantiate(GetBrickPrefab(teamMember), new Vector3(9999, 9999, 9999), Quaternion.identity);

            team1.Add(new Character(m.GetComponent<CharacterMovement>(), m.GetComponentInChildren<CharacterHighlight>(), m, m.GetComponent<CharacterAttributes>(), teamMember, 1));
            team1[i].CharacterHighlight.TeamNumber = 1;
            team1[i].CharacterHighlight.DeselectAllOther += DeselectAllOther;
            team1[i].CharacterHighlight.UpdateAttackArea += UpdateAttackArea;

            team1[i].CharacterMovement.CanMove += CanMove;
            team1[i].CharacterMovement.SetRotation(Assets.GridRotation.Up);
            team1[i].CharacterMovement.UpdateAttackArea += UpdateAttackArea;
			team1[i].CharacterMovement.ApplyDamage += ApplyDamage;
			team1[i].CharacterMovement.UpdateMobility += () => BattleUIScript.UpdateCharacterOnTurnState(SelectedCharacter);

			team1[i].CharacterAttributes.RemoveFromArray += RemoveFromArray;
            i++;
        }

        i = 0;
        foreach (var teamMember in Player2.Team)
        {
            var m = Instantiate(GetBrickPrefab(teamMember), new Vector3(9999, 9999, 9999), Quaternion.identity);

            team2.Add(new Character(m.GetComponent<CharacterMovement>(), m.GetComponentInChildren<CharacterHighlight>(), m, m.GetComponent<CharacterAttributes>(), teamMember, 2));
            team2[i].CharacterHighlight.TeamNumber = 2;
            team2[i].CharacterHighlight.DeselectAllOther += DeselectAllOther;
            team2[i].CharacterHighlight.UpdateAttackArea += UpdateAttackArea;

            team2[i].CharacterMovement.CanMove += CanMove;
            team2[i].CharacterMovement.SetRotation(Assets.GridRotation.Down);
            team2[i].CharacterMovement.UpdateAttackArea += UpdateAttackArea;
            team2[i].CharacterMovement.ApplyDamage += ApplyDamage;
			team2[i].CharacterMovement.UpdateMobility += () => BattleUIScript.UpdateCharacterOnTurnState(SelectedCharacter);

			team2[i].CharacterAttributes.RemoveFromArray += RemoveFromArray;
            i++;
        }
    }

    private GameObject GetBrickPrefab(CharacterEnum c)
    {
        switch (c)
        {
            case CharacterEnum.Jumper:
                return JumperPrefab;
            case CharacterEnum.Tower:
                return TowerPrefab;
            case CharacterEnum.Shield:
                return ShieldPrefab;
            case CharacterEnum.Rhino:
                return RhinoPrefab;
            case CharacterEnum.Worm:
                return CatterpillarPrefab;
        }
        return null;
    }

    private void DeselectAllOther(CharacterHighlight characterHighlight)
    {
        foreach (var tm in team1)
        {
            if (tm.CharacterHighlight != characterHighlight)
                tm.CharacterHighlight.Deselect();
        }
        foreach (var tm in team2)
        {
            if (tm.CharacterHighlight != characterHighlight)
                tm.CharacterHighlight.Deselect();
        }
    }

    private void RemoveFromArray(CharacterAttributes c)
    {
        var character = team1.FirstOrDefault(x => x.CharacterAttributes == c) ?? team2.FirstOrDefault(x => x.CharacterAttributes == c);

		if(character.Team == 1)
		{
			team1.Remove(character);

			if (team1.Count <= 0)
			{
				FinishGame(Player2.Name);
			}

			int hpToAdd = character.CharacterAttributes.maxHitPoints / team1.Count;
			foreach(var ch in team1)
			{
				ch.CharacterAttributes.maxHitPoints += hpToAdd;
				ch.CharacterAttributes.hitPoints += hpToAdd;
				ch.CharacterAttributes.UpdateHPBar();
			}
		}
		else
		{
			team2.Remove(character);

			if(team2.Count <= 0)
			{
				FinishGame(Player1.Name);
			}

			int hpToAdd = character.CharacterAttributes.maxHitPoints / team2.Count;
			foreach (var ch in team2)
			{
				ch.CharacterAttributes.maxHitPoints += hpToAdd;
				ch.CharacterAttributes.hitPoints += hpToAdd;
				ch.CharacterAttributes.UpdateHPBar();
			}
		}
    }

    private void ApplyDamage(List<Tuple<Vector2Int, int>> damages, bool isStunAttack)
    {
        foreach (var t in GetCharacters())
        {
            foreach (var d in damages)
            {
                if (t.CharacterMovement.Coordinates == null)
                    continue;

                if (t.CharacterMovement.Coordinates.Contains(d.Item1))
                {
                    t.CharacterAttributes.Hurt(d.Item2);
                    if(!t.CharacterAttributes.isStunned && isStunAttack)
                        t.CharacterAttributes.isStunned = true;
                }
            }
        }
    }

    private void MoveCharacter(LinkedList<Vector2Int> path)
    {
        var pathVec3 = new LinkedList<Vector3>();
        if (SelectedCharacter == null)
            return;

        foreach (var p in path)
        {
            pathVec3.AddLast(new Vector3
            {
                x = p.x * grid.Scale - grid.Offset,
                z = p.y * grid.Scale - grid.Offset,
                y = SelectedCharacter.CharacterActor.transform.localScale.y / 2
            });
        }

        if (SelectedCharacter.CharacterMovement.InitializeMovement(pathVec3, path))
        {
            //SelectedCharacter.CharacterAttributes.mobilityLeft -= path.Count;
            BattleUIScript.UpdateCharacterOnTurnState(SelectedCharacter);
        }
    }

    public Character[] GetCharacters()
    {
        return team1.Concat(team2).ToArray();
    }

    private bool CanMove(Vector2Int[] coords)
    {
        foreach (var character in GetCharacters())
        {
            if (character.CharacterHighlight.State == CharacterHighlightEnum.Selected || character.CharacterMovement.Coordinates == null)
                continue;

            foreach (var c1 in coords)
            {
                foreach (var c2 in character.CharacterMovement.Coordinates)
                {
                    if (c1.Equals(c2))
                        return false;
                }
            }
        }
		return true;
    }
    private void UpdateAttackArea()
    {
        var selectedCharacter = SelectedCharacter;
        if (selectedCharacter == null)
            return;

        var coordsToHighlight = selectedCharacter.CharacterMovement.GetAttackArea();
        if (coordsToHighlight == null)
            return;

        grid.HighlighAttackArea(coordsToHighlight);
		BattleUIScript.UpdateCharacterOnTurnState(SelectedCharacter);
	}

    private bool IsFirstTurn()
    {
        var selectedCharacter = SelectedCharacter;
        if (selectedCharacter == null)
            return false;

        if (selectedCharacter.CharacterMovement.Coordinates == null)
            return true;

        return false;
    }

    private void SetInitialCharacterPosition(Vector2Int pos)
    {
        var selectedCharacter = SelectedCharacter;
        if (selectedCharacter == null || !CanMove(selectedCharacter.CharacterMovement.GetCoordinates(pos)))
            return;


        selectedCharacter.CharacterMovement.SetCoordinates(pos);

        var position = new Vector3
        {
            x = pos.x * grid.Scale - grid.Offset,
            z = pos.y * grid.Scale - grid.Offset,
            y = selectedCharacter.CharacterActor.transform.localScale.y / 2
        };

        selectedCharacter.CharacterActor.transform.position = position;

        Next();
    }

    public void Attack()
    {
        var selectedCharacter = SelectedCharacter;
        if (selectedCharacter == null || selectedCharacter.CharacterAttributes.hasAttacked == true)
            return;

		if(selectedCharacter.CharacterMovement.InitializeAttack())
		{
			selectedCharacter.CharacterAttributes.remainingNumberOfAttacks--;
			if (selectedCharacter.CharacterAttributes.remainingNumberOfAttacks <= 0)
			{
				selectedCharacter.CharacterAttributes.mobilityLeft -= selectedCharacter.CharacterAttributes.mobility / 2;
				selectedCharacter.CharacterAttributes.mobilityLeft = selectedCharacter.CharacterAttributes.mobilityLeft < 0 
					? 0 
					: selectedCharacter.CharacterAttributes.mobilityLeft;
			}
		}        
        BattleUIScript.UpdateCharacterOnTurnState(SelectedCharacter);
    }
}
