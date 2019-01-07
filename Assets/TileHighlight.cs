using Assets.Enums;
using UnityEngine;

public class TileHighlight : MonoBehaviour
{
	private TileHighlightEnum state;
	public TileHighlightEnum State
	{
		get
		{
			return state;
		}
		set
		{
			state = value;
			UpdateMaterial();
		}
	}

	public Material Highlighted;
	public Material Default;
	public Material Path;
	public Material AttackArea;

	public Material Material
	{
		get
		{
			switch (State)
			{
				case TileHighlightEnum.Default:
					return Default;
				case TileHighlightEnum.Highlighted:
					return Highlighted;
				case TileHighlightEnum.Path:
					return Path;
				case TileHighlightEnum.AttackArea:
					return AttackArea;
			}
			return null;
		}
	}

	private Renderer Renderer
	{
		get
		{
			return GetComponent<Renderer>();
		}
	}

	void OnMouseEnter()
	{
		if(State == TileHighlightEnum.Default)
			State = TileHighlightEnum.Highlighted;
	}

	void OnMouseExit()
	{
		if(State == TileHighlightEnum.Highlighted)
			State = TileHighlightEnum.Default;
	}


	// Use this for initialization
	void Awake()
	{
		State = TileHighlightEnum.Default;
	}

	// Update is called once per frame
	void Update()
	{

	}

	public void HighlightAsPath()
	{
		State = TileHighlightEnum.Path;
	}

	public void StopHighlightingAsPath()
	{
		if(State == TileHighlightEnum.Path)
			State = TileHighlightEnum.Default;
	}
	public void StopAllHighlightsExceptHoverHighlight()
	{
		if(state != TileHighlightEnum.Highlighted)
			State = TileHighlightEnum.Default;
	}

	public void HighlightAsAttackArea()
	{
		State = TileHighlightEnum.AttackArea;
	}
	
	private void UpdateMaterial()
	{
		Renderer.material = Material;
	}
}
