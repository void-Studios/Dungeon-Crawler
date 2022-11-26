using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class scriptEnemyHandler : MonoBehaviour
{
    [SerializeField] private float canCheckDelay = 1f;

    public PolygonCollider2D enemyCollider2d;
    public Rigidbody2D enemyRigid2d;
    public Transform playerRoot;

    [SerializeField] private float moveSpeed=1f;
    private Vector2 moveDirection;
    private Vector3 direction;

    private void Awake()
    {
        playerRoot= FindObjectOfType<StatsHandler>().transform;
        enemyCollider2d = GetComponent<PolygonCollider2D>();
        enemyRigid2d = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        direction = (playerRoot.position - transform.position);//.normalized;
        moveDirection = new Vector2(direction.x, direction.y).normalized;
        enemyRigid2d.MovePosition((enemyRigid2d.position + moveDirection * moveSpeed * Time.fixedDeltaTime));
    }


    IEnumerator CheckPlayerDelay()
    {
        yield return new WaitForSeconds(canCheckDelay);
        StartCoroutine(CheckPlayerDelay());
    }
}
