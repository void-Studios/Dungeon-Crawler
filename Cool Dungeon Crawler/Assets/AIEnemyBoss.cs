using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIEnemyBoss : MonoBehaviour
{
    [SerializeField] private Transform Player;
    [SerializeField] private PolygonCollider2D Collider2d;
    [SerializeField] private Rigidbody2D Rigid2d;
    [SerializeField] private float moveSpeed = 1f;
    private scriptApostle scriptApostle;

    [SerializeField] private int HPMax;
    private int HPCurrent;

    private Vector2 moveDirection;
    private Vector3 direction;

    private void Awake()
    {
        scriptApostle = FindObjectOfType<scriptApostle>().transform.gameObject.GetComponent<scriptApostle>();

        HPCurrent = HPMax;
        Player = FindObjectOfType<StatsHandler>().transform;
        Collider2d = GetComponent<PolygonCollider2D>();
        Rigid2d = GetComponent<Rigidbody2D>();

    }

    private void FixedUpdate()
    {
        direction = Player.position - this.transform.position;
        moveDirection = new Vector2(direction.x, direction.y).normalized;
        Rigid2d.MovePosition(Rigid2d.position + moveDirection * moveSpeed * Time.fixedDeltaTime);
    }

    public void ReceiveDamage(int value)
    {

        if (HPCurrent > 0)
        {
            HPCurrent-=value;
        }
        else if (HPCurrent<=0)
        {
            Destroy(gameObject);
            Debug.Log("Boss Has died");
            scriptApostle.UpdateEnemyDeath();

        }

    }

}