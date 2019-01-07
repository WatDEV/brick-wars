using Assets.Enums;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pathfinder : MonoBehaviour
{
	public Vector2Int Coordinates { get; set; }
    public Func<bool> IsFirstTurn;
    public Action<Vector2Int> SetFirstPosition;
	public Func<Vector2Int, bool> AddPath;
	public Action FinishPath;
	public Func<Vector2Int, bool> IsLastPath;
	public Action RemoveLastPath;

	private TileHighlight Highlighter { get { return GetComponent<TileHighlight>(); } }

	void OnMouseEnter()
	{
        if (!Input.GetMouseButton(0))
            return;
		if(Highlighter.State == TileHighlightEnum.Path)
		{
			if(IsLastPath(Coordinates))
			{
				RemoveLastPath();
				Highlighter.StopHighlightingAsPath();
			}
		}
		else
		{
			if (AddPath(Coordinates))
				Highlighter.HighlightAsPath();
		}
	}

	void OnMouseExit()
	{
	}

	void OnMouseDown()
    {
        if(IsFirstTurn())
        {
            SetFirstPosition(Coordinates);
            return;
        }

        if (AddPath(Coordinates))
            Highlighter.HighlightAsPath();
    }

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update ()
    {
        //Event OnMouseUp is not triggering in some cases. It need to be in update.
        if (Input.GetMouseButtonUp(0))
            FinishPath();
	}
}
