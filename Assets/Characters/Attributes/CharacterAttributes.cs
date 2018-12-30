using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterAttributes : MonoBehaviour {

    public int movementSpeed = 20;
	public int damage = 110;
	public int hitPoints = 100;
	public int mobility = 8;
	public int resourceCost = 10;
	public int turnTimer = 0;

	public Action<CharacterAttributes> RemoveFromArray;

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

    }
    public void Hurt(int damage)
    {
        hitPoints -= damage;
        if (hitPoints < 0)
        {
            Destroy(gameObject, 1);
			RemoveFromArray(this);
        }
    }

	public override bool Equals(object other)
	{
		var c = other as CharacterAttributes;
		return c.gameObject == gameObject;
	}
}
