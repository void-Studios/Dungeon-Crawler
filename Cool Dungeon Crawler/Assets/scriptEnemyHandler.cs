using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class scriptEnemyHandler : MonoBehaviour
{
    private bool canCheckForPlayer;
    [SerializeField] private float canCheckDelay = 1f;


    public PolygonCollider2D enemyCollider2d;
    public Rigidbody2D enemyRigid2d;
    public GameObject playerObj;
    public Transform playerRoot;


    [SerializeField] private float moveSpeed=1f;
    private Vector2 moveDirection;
    private Vector3 direction;

    private float rotationRadian;
    private float rotationAngle;
    

    // Start is called before the first frame update
    private void Awake()
    {
        canCheckForPlayer = true;

        playerObj = GameObject.FindGameObjectWithTag("Player");
        playerRoot= FindObjectOfType<StatsHandler>().transform;
       //playerRoot = playerObj.transform.GetChild(1);

        enemyCollider2d = GetComponent<PolygonCollider2D>();
        enemyRigid2d = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        /*
        Vector2 p2 = new Vector2(playerObj.transform.position.x, playerObj.transform.position.y);
        Vector2 p1 = new Vector2(this.transform.position.x, this.transform.position.y);
        float vertical = p2.x - p1.x;
        float horizontal = p2.y - p1.y;
         */      
        //direction = (playerObj.transform.position - this.transform.position);//.normalized;
        
        //moveDirection = new Vector2(horizontal,vertical).normalized;

        rotationRadian = Mathf.Atan2(direction.y, direction.x);
        rotationAngle = rotationRadian * Mathf.Rad2Deg + 90;

        //this.transform.rotation = Quaternion.Euler(0, 0, rotationAngle);
        

        //transform.RotateAround(playerObj.transform.position, Vector3.forward, rotationAngle);

    }
    private void FixedUpdate()
    {
        direction = (playerRoot.position - transform.position);//.normalized;
        moveDirection = new Vector2(direction.x, direction.y).normalized;
        //Vector2 tempForward = new Vector2(this.transform.up.x, this.transform.up.y);
        //Vector2 velocity = direction * moveSpeed * Time.fixedDeltaTime;

        //enemyRigid2d.velocity = velocity;
        
        enemyRigid2d.MovePosition((enemyRigid2d.position + moveDirection * moveSpeed * Time.fixedDeltaTime));
    }

    public void Death()
    {
        Destroy(gameObject);
    }


    IEnumerator CheckPlayerDelay()
    {
        /*
        //float vertical = playerObj.transform.position.y - transform.position.y;
        //float horizontal = playerObj.transform.position.x - transform.position.x;




        direction = (this.transform.position - playerObj.transform.position);//.normalized;

        rotationRadian = Mathf.Atan2(direction.y, direction.x);
        rotationAngle = rotationRadian * Mathf.Rad2Deg + 90;
        
        moveDirection = new Vector2(direction.x,direction.y);
        */

        yield return new WaitForSeconds(canCheckDelay);
        StartCoroutine(CheckPlayerDelay());
    }
}
