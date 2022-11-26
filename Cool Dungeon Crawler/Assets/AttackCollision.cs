using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackCollision : MonoBehaviour
{
    public StatsHandler Player;

    private void Awake()
    {
        Player = FindObjectOfType<StatsHandler>();

    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("boss"))
        {
            Debug.Log(Player.GetCurrentAttack());
            gameObject.GetComponentInParent<scriptAttackHandler>().InvokeDeath();

            collision.gameObject.GetComponent<AIEnemyBoss>().ReceiveDamage(Player.GetCurrentAttack());
        }
    }
}
