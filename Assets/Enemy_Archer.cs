using UnityEngine;

public class Enemy_Archer : Enemy
{
    protected override void Attack()
    {
        Debug.Log(enemyName + " shoots an arrow!!!");
    }
}
