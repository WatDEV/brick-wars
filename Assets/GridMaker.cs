using Assets.Characters;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridMaker : MonoBehaviour
{
	[SerializeField]
	public GameObject TileGameObject;
	public int Scale = 10;

	public GameObject[,] TileGrid;
	public TileHighlight[,] TileHighlighters;

	public Action<LinkedList<Vector2Int>> MoveCharacter;

    public Func<Character> GetSelectedCharacter;
    public Func<Character[]> GetCharacters;

    public LinkedList<Vector2Int> Path { get; set; } = new LinkedList<Vector2Int>();

	[SerializeField]
	public int Size { get; set; } = 20;

	public int Offset
	{
		get
		{
			return (Size * Scale / 2);
		}
	}

	void Awake ()
	{
		if(TileGameObject == null)
			TileGameObject = Resources.Load<GameObject>("Prefabs/TilePrefab");

		TileGrid = new GameObject[Size,Size];
		TileHighlighters = new TileHighlight[Size, Size];
		for (var i = 0; i < Size; i++)
		{
			for (var j = 0; j < Size; j++)
			{
				var position = new Vector3((Scale * i ) - Offset, 0, (Scale * j) - Offset);
				var cube = Instantiate(TileGameObject, position, Quaternion.identity);

				var tile = cube.GetComponentInChildren<Pathfinder>();
				var highlight = cube.GetComponentInChildren<TileHighlight>();
				TileHighlighters[i, j] = highlight;

                tile.Coordinates = new Vector2Int(i, j);
				tile.FinishPath += FinishPath;
				tile.AddPath += AddPath;

				TileGrid[i, j] = cube;
			}
		}
	}

	private bool AddPath(Vector2Int coords)
	{
        var selectedCharLocations = GetSelectedCharacter();
        if (selectedCharLocations == null)
            return false;

		if (Path.Contains(coords))
			return false;

        if (Path.Last != null && Vector2Int.Distance(Path.Last.Value, coords) > 1)
            return false;

        //TODO move to function it is too big (if you can move with big brick)
        if (Path == null || Path.Count == 0)
        {
            var canAddPath = false;
            foreach (var charactersLocation in GetSelectedCharacter().CharacterMovement.Coordinates)
            {
                if (Vector2Int.Distance(charactersLocation, coords) <= 1)
                {
                    canAddPath = true;
                }
            }
            if (!canAddPath)
                return false;
        }

		Path.AddLast(coords);
        return true;
	}
	private void FinishPath()
	{
		if (Path == null || Path.Count == 0)
			return;

		var selectedCharacter = GetSelectedCharacter();
		if (selectedCharacter == null || selectedCharacter.CharacterMovement.IsMoving)
            return;

		foreach (var h in TileHighlighters)
		{
			h.StopHighlightingAsPath();
		}

		MoveCharacter(Path);

		Path = new LinkedList<Vector2Int>();
	}

	public void HighlighAttackArea(Vector2Int[] coords)
	{
		foreach(var h in TileHighlighters)
		{
			h.StopAllHighlightsExceptHoverHighlight();
		}

		foreach(var c in coords)
		{
			if (c.x >= 0 && c.x < Size && c.y >= 0 && c.y < Size)
			{
				TileHighlighters[c.x, c.y].HighlightAsAttackArea();
			}
		}
	}

	// Update is called once per frame
	void Update ()
    {
		
	}
}
