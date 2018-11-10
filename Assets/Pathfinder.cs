using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pathfinder : MonoBehaviour
{
	public Vector2Int Coordinates { get; set; }
	public Func<Vector2Int, bool> AddPath;
	public Action FinishPath;
    private TileHighlight Highlighter { get { return GetComponent<TileHighlight>(); } }

	void OnMouseEnter()
	{
        if (!Input.GetMouseButton(0))
            return;
        if (AddPath(Coordinates))
            Highlighter.HighlightAsPath();
	}

	void OnMouseExit()
	{
	}

	void OnMouseDown()
    {
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
