using Assets.Characters;
using InitativeNamespace;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Combat : MonoBehaviour
{
    [SerializeField]
    public GameObject GridPrefab;

    [SerializeField]
    public GameObject[] Team1Actors;
    [SerializeField]
    public Vector2Int[] Team1Positions;

    [SerializeField]
    public GameObject[] Team2Actors;
    [SerializeField]
    public Vector2Int[] Team2Positions;

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

	// Use this for initialization
	void Start()
	{
		var g = Instantiate(GridPrefab, new Vector3(0, 0, 0), Quaternion.identity);
		grid = g.GetComponent<GridMaker>();
		grid.MoveCharacter += MoveCharacter;
		grid.GetSelectedCharacter += () => { return SelectedCharacter; };
		grid.GetCharacters += GetCharacters;

		team1 = new List<Character>();
		team2 = new List<Character>();

		SpawnTeams();

		var teams = new List<Character>();
		teams.AddRange(team1);
		teams.AddRange(team2);
		initative = new Initiative();
		initative.Init(teams);

		Next();
    }

	public void Next()
	{		
		initative.Next().CharacterHighlight.Select();

		if (team1.Count == 0)
		{
			Debug.Log("Team 2 won!");
		}
		if (team2.Count == 0)
		{
			Debug.Log("Team 1 won!");
		}

	}

    private void SpawnTeams()
    {
        var i = 0;
        foreach (var teamMember in Team1Actors)
        {
            var position = new Vector3
            {
                x = Team1Positions[i].x * grid.Scale - grid.Offset,
                z = Team1Positions[i].y * grid.Scale - grid.Offset,
                y = teamMember.transform.localScale.y / 2
            };

            var m = Instantiate(teamMember, position, Quaternion.identity);

            team1.Add(new Character(m.GetComponent<CharacterMovement>(), m.GetComponentInChildren<CharacterHighlight>(), m, m.GetComponent<CharacterAttributes>()));
            team1[i].CharacterHighlight.TeamNumber = 1;
            team1[i].CharacterHighlight.DeselectAllOther += DeselectAllOther;
			team1[i].CharacterHighlight.UpdateAttackArea += UpdateAttackArea;

			team1[i].CharacterMovement.SetCoorinates(new Vector2Int(Team1Positions[i].x, Team1Positions[i].y));
            team1[i].CharacterMovement.CanMove += CanMove;
			team1[i].CharacterMovement.SetRotation(Assets.GridRotation.Up);
			team1[i].CharacterMovement.UpdateAttackArea += UpdateAttackArea;
			team1[i].CharacterMovement.ApplyDamage += ApplyDamage;
			team1[i].CharacterMovement.EndTurn += Next;

			team1[i].CharacterAttributes.RemoveFromArray += RemoveFromArray;
			i++;
        }

        i = 0;
        foreach (var teamMember in Team2Actors)
        {
            var position = new Vector3
            {
                x = Team2Positions[i].x * grid.Scale - grid.Offset,
                z = Team2Positions[i].y * grid.Scale - grid.Offset,
                y = teamMember.transform.localScale.y / 2
            };

            var m = Instantiate(teamMember, position, Quaternion.identity);

            team2.Add(new Character(m.GetComponent<CharacterMovement>(), m.GetComponentInChildren<CharacterHighlight>(), m, m.GetComponent< CharacterAttributes>()));
            team2[i].CharacterHighlight.TeamNumber = 2;
            team2[i].CharacterHighlight.DeselectAllOther += DeselectAllOther;
			team2[i].CharacterHighlight.UpdateAttackArea += UpdateAttackArea;

			team2[i].CharacterMovement.SetCoorinates(new Vector2Int(Team2Positions[i].x, Team2Positions[i].y));
            team2[i].CharacterMovement.CanMove += CanMove;
            team2[i].CharacterMovement.SetRotation(Assets.GridRotation.Down);
            team2[i].CharacterMovement.UpdateAttackArea += UpdateAttackArea;
            team2[i].CharacterMovement.ApplyDamage += ApplyDamage;
			team2[i].CharacterMovement.EndTurn += Next;

			team2[i].CharacterAttributes.RemoveFromArray += RemoveFromArray;
            i++;
        }
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

		team1.Remove(character);
		team2.Remove(character);
	}

    private void ApplyDamage(Vector2Int[] damageArea, int damage)
    {
        GetCharactersInDamageArea(damageArea)?.ForEach(x => x.Hurt(damage));
    }

    private List<CharacterAttributes> GetCharactersInDamageArea(Vector2Int[] damageArea)
    {
        var characters = new List<CharacterAttributes>();
        foreach(var t in team1)
        {
            foreach(var pos in damageArea)
            {
                if(t.CharacterMovement.Coordinates.Contains(pos) && !characters.Contains(t.CharacterAttributes))
                {
                    characters.Add(t.CharacterAttributes);
                }
            }
        }
        foreach (var t in team2)
        {
            foreach (var pos in damageArea)
            {
                if (t.CharacterMovement.Coordinates.Contains(pos) && !characters.Contains(t.CharacterAttributes))
                {
                    characters.Add(t.CharacterAttributes);
                }
            }
        }
        return characters;
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

        SelectedCharacter.CharacterMovement.InitializeMovement(pathVec3, path);
    }

    public Character[] GetCharacters()
    {
        return team1.Concat(team2).ToArray();
    }

    private bool CanMove(Vector2Int[] coords)
    {
        foreach(var character in GetCharacters())
        {
            if (character.CharacterHighlight.State == CharacterHighlightEnum.Selected)
                continue;

            foreach(var c1 in coords)
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
	}
}
