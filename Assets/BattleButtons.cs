using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleButtons : MonoBehaviour {

    public Combat Combat;

    public void EndTurn()
    {
        Combat.Next();
    }

    public void Attack()
    {
        Combat.Attack();
    }
}
