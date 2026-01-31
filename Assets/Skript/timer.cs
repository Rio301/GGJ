using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class timer : MonoBehaviour
{
    public Transform rectangle;
    public float combotimeMax = 4f;
    private float combotime;

    // Start is called before the first frame update
    void Start()
    {
        combotime = combotimeMax;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        rectangle.localScale = new Vector3(combotime / combotimeMax * 16.5f, 0.25f, 0.25f);
        if (Data.score > 0) combotime -= Time.fixedDeltaTime;
        if (combotime < 0)
        {
            Data.health--;
            combotime = combotimeMax;
        }
        rectangle.localScale = new Vector3(combotime/combotimeMax*16.5f , 0.25f, 0.25f);
    }

    public void resetTimer()
    {
        combotime = combotimeMax;
    }
}
