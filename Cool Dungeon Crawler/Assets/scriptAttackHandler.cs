using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class scriptAttackHandler : MonoBehaviour
{
    private bool isDirectionSet;
    private Vector2 destination;
    public PolygonCollider2D attackCollider;
    public Rigidbody2D attackRigid2d;
    [SerializeField]private float projectileSpeed;
    public StatsHandler Player;
    private float lifeSpan;
    private bool isDeathRequested;

    public GameObject sprite;

    #region GetSets
    public Vector3 SetDestination(Vector3 destination) {

        this.destination = destination;
        isDirectionSet = true;
        return this.destination;
    }
    public void SetRotation(float rotation)
    {
        this.transform.rotation = Quaternion.Euler(0, 0, rotation);
    }
    public float SetLifeSpan(float lifeSpan)
    {
        this.lifeSpan = lifeSpan;
        return this.lifeSpan;
    }
    
    public Vector3 GetDestination() { return destination; }
    public Quaternion GetRotation() { return this.transform.rotation; }
    public float GetLifeSpan() { return lifeSpan; }
    #endregion


    private void Awake()
    {
        isDeathRequested = false;
        lifeSpan = GodEye.GetWeaponLifetime();
        projectileSpeed = GodEye.GetWeaponSpeed();
        Player = FindObjectOfType<StatsHandler>();
        sprite = this.gameObject.transform.GetChild(0).gameObject;
        attackCollider = this.gameObject.transform.GetChild(0).GetComponent<PolygonCollider2D>();
        attackRigid2d = this.gameObject.transform.GetChild(0).GetComponent<Rigidbody2D>();
        isDirectionSet = false;
    }
    private void Start()
    {
        StartCoroutine(LifespanCounter());
    }
    void FixedUpdate()
    {
        if (isDirectionSet)
        {
            Vector2 tempForward = new Vector2(this.transform.up.x, this.transform.up.y);
            attackRigid2d.MovePosition(attackRigid2d.position + tempForward * projectileSpeed * Time.fixedDeltaTime);
        }
    }

    public bool InvokeDeath()
    {
        if (!isDeathRequested)
        {
            isDeathRequested = true;
            Destroy(gameObject);
        
        }
        return true;
    }








    IEnumerator LifespanCounter()
    {
        yield return new WaitForSeconds(lifeSpan);
        InvokeDeath();
    }

}
