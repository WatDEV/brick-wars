using Assets.Characters;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterHighlight : MonoBehaviour
{
	private Material regular;
	private Material higlighted;
	private Material selected;
	public Action<CharacterHighlight> DeselectAllOther;
	public Action UpdateAttackArea;
	public CharacterHighlightEnum state;
	public CharacterHighlightEnum State
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

	public Material Material
	{
		get
		{
			switch(state)
			{
				case CharacterHighlightEnum.Default:
					if (regular == null)
						regular = Resources.Load<Material>($"Materials/Team{TeamNumber}Colour");
					return regular;
				case CharacterHighlightEnum.Highlighted:
					if (higlighted == null)
						higlighted = Resources.Load<Material>($"Materials/Team{TeamNumber}Highlighted");

					return higlighted;
				case CharacterHighlightEnum.Selected:
					if (selected == null)
						selected = Resources.Load<Material>($"Materials/Team{TeamNumber}Selected");

					return selected;
			}
			return null;
		}
	}

	public int TeamNumber { get; set; }

	private Renderer rend;

	private Renderer Renderer
	{
		get
		{
			if (rend == null)
				rend = GetComponentInChildren<Renderer>();

			return rend;
		}
	}

	/*void OnMouseEnter()
	{
		if (state != CharacterHighlightEnum.Selected)
			state = CharacterHighlightEnum.Highlighted;
	}

	void OnMouseExit()
	{
		if(state != CharacterHighlightEnum.Selected)
			state = CharacterHighlightEnum.Default;
	}
	private void OnMouseDown()
	{
		if (state != CharacterHighlightEnum.Selected)
		{
			state = CharacterHighlightEnum.Selected;
			DeselectAllOther(this);
			UpdateAttackArea();
		}
		else
		{
			state = CharacterHighlightEnum.Default;
		}
	}*/

	void Update()
	{
		UpdateMaterial();
	}

	void Awake()
	{
		state = CharacterHighlightEnum.Default;
	}

	private void UpdateMaterial()
	{
		Renderer.material = Material;
	}

	public void Select()
	{
		state = CharacterHighlightEnum.Selected;
		DeselectAllOther(this);
		UpdateAttackArea();
	}
	public void Deselect() => state = CharacterHighlightEnum.Default;
}
