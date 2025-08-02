using System;
using UnityEngine;

public class Enemy : MonoBehaviour
{

    [SerializeField] protected string enemyName;
    [SerializeField] protected float moveSpeed;

    private void Update()
    {
        // MoveAround();
        if (Input.GetKeyDown(KeyCode.F))
            Attack();
    }

    protected virtual void Attack()
    {
        Debug.Log(enemyName + "attacks!");
    }

    private void MoveAround()
    {
        Debug.Log(enemyName + " moves at speed " +  moveSpeed);
    }
    public void TakeDamage()
    {

        Debug.Log("take damage");
    }

    public string GetEnemyName()
    {
        return enemyName;
    }


}