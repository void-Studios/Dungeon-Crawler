using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemyRotate : MonoBehaviour
{
    // Start is called before the first frame update
    public Vector3 playerPos;
    public TrailRenderer attackTrail;
    public Transform target;
    public Vector3 object_pos;
    private float angle;
    private GameObject gameHandler;

    // Update is called once per frame

    void Update()
    {
        // playerPos = Input.mousePosition;
        // playerPos.z=-10f;
        gameHandler = GameObject.Find("Game Handler");
        playerPos = gameHandler.GetComponent<buttonHandler>().Player.transform.position;

        
        object_pos = transform.position;
        playerPos.x = playerPos.x - object_pos.x;
        playerPos.y = playerPos.y - object_pos.y;

        playerPos.Normalize();
        angle = Mathf.Atan2(playerPos.y, playerPos.x) * Mathf.Rad2Deg - 90;
        transform.rotation = Quaternion.Euler(0, 0, angle);

        if (Input.GetMouseButtonDown(0))
        {
            Vector3 aim = Input.mousePosition;
            aim.z = 0;
            float angleAim;
            angleAim = Mathf.Atan2(aim.y, aim.x) * Mathf.Rad2Deg;
            StartCoroutine(Shoot());
        }
    }
    public IEnumerator Shoot()
    {
        attackTrail.transform.localPosition = Vector3.zero;
        float delay = 0;

        while (delay < 1)
        {

            attackTrail.gameObject.SetActive(true);
            attackTrail.transform.position += transform.up * .4f;
            yield return new WaitForSeconds(0.02f);
            delay += 0.4f;
        }
        attackTrail.gameObject.SetActive(false);
    }
}
