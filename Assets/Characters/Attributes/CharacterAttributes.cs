using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterAttributes : MonoBehaviour {

    private int movementSpeed = 20;
    private int damage = 110;
    private int hitPoints = 100;
    private int mobility = 8;
    private int speed = 20;
    private int resourceCost = 10;

    [SerializeField]
    public virtual int ResourceCost
    {
        get { return resourceCost; }
        set { resourceCost = value; }
    }

    [SerializeField]
    public virtual int Speed
    {
        get { return speed; }
        set { speed = value; }
    }

    [SerializeField]
    public virtual int Mobility
    {
        get { return mobility; }
        set { mobility = value; }
    }

    [SerializeField]
    public virtual int HitPoints
    {
        get { return hitPoints; }
        set { hitPoints = value; }
    }

    [SerializeField]
    public virtual int Damage
    {
        get { return damage; }
        set { damage = value; }
    }

    [SerializeField]
    public virtual int MovementSpeed
    {
        get { return movementSpeed; }
        set { movementSpeed = value; }
    }


    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

    }
    public void Hurt(int damage)
    {
        HitPoints -= damage;
        if (HitPoints < 0)
        {
            Destroy(gameObject, 1);
        }
    }
}
