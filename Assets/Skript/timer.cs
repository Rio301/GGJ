using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class timer : MonoBehaviour
{
    public Transform rectangle;
    public float combotimeMax = 4f;
    private float combotime;
    SoundManejer soundManejer;
    // Start is called before the first frame update
    void Start()
    {
        soundManejer = GameObject.FindGameObjectWithTag("audio").GetComponent<SoundManejer>();
        combotime = combotimeMax;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        rectangle.localScale = new Vector3(combotime / combotimeMax * 1.8f, 1f, 1f);
        if (Data.score > 0) combotime -= Time.fixedDeltaTime;
        if (combotime < 0)
        {
            Data.health--;
            soundManejer.sfxSource.PlayOneShot(soundManejer.healtReduce2);
            combotime = combotimeMax;
        }
        rectangle.localScale = new Vector3(combotime/combotimeMax*1.8f , 1f, 1f);
    }

    public void resetTimer()
    {
        combotime = combotimeMax;
    }
}
