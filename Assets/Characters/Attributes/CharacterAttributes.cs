using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterAttributes : MonoBehaviour {

    public int movementSpeed = 20;
	public int damage = 110;
    public int hitPoints = 100;
    public int maxHitPoints = 100;
    public int mobility = 8;
	public int turnTimer = 0;
    public int mobilityLeft = 8;
    public bool isStunned = false;

    public GameObject HealthBarGO;
    private HealthBarScript healthBar;

	public Action<CharacterAttributes> RemoveFromArray;
    public Action<CharacterAttributes> RemoveFromQueue;

	public int numberOfAttacks = 1;
	public int remainingNumberOfAttacks = 1;

	public bool hasAttacked	{ get { return remainingNumberOfAttacks <= 0; } }

	// Use this for initialization
	void Start () {
        healthBar = HealthBarGO.GetComponent<HealthBarScript>();
        hitPoints = maxHitPoints;
        healthBar.UpdateValue(hitPoints / (float)maxHitPoints);
	}
	
	// Update is called once per frame
	void Update () {

    }
    public void Hurt(int damage)
    {
        hitPoints -= damage;
        healthBar.UpdateValue(hitPoints / (float)maxHitPoints);
        if (hitPoints <= 0)
        {
            Destroy(gameObject, 1);
            RemoveFromArray(this);
            RemoveFromQueue(this);
        }
    }

	public override bool Equals(object other)
	{
		var c = other as CharacterAttributes;
		return c.gameObject == gameObject;
	}

    public void NewTurn()
    {
        mobilityLeft = mobility;
		remainingNumberOfAttacks = numberOfAttacks;
    }
}
