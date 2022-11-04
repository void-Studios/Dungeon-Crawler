using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class scriptRotateSelf : MonoBehaviour
{
    private float degreeMax = 360;
    private float degreeCurrent;
    public int rotationSpeed;


    // Start is called before the first frame update
    void Start()
    {
        degreeCurrent = 0;
//        TimerOneSecond();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        degreeCurrent += (Time.fixedDeltaTime * rotationSpeed / Mathf.PI);

        if (degreeCurrent - degreeMax > 0)
        {
            degreeCurrent -= degreeMax;
        }

        transform.rotation = Quaternion.Euler(0, 0, degreeCurrent);
    }
/*
    private IEnumerator TimerOneSecond()
    {
        

        yield return new WaitForSeconds(1);
        TimerOneSecond();
    }
*/
}
