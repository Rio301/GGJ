using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class timer : MonoBehaviour
{
    public Transform rectangle;
    public float combotimeMax = 4f;
    public float combotime;
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
        rectangle.localScale = new Vector3(combotime / combotimeMax * 1.8f, rectangle.localScale.y, 1f);
        if (Data.score > 0) combotime -= Time.fixedDeltaTime;
        if (combotime < 0)
        {
            Data.health--;
            soundManejer.sfxSource.PlayOneShot(soundManejer.healtReduce2);
            combotime = combotimeMax;
        }
    }

    public void resetTimer()
    {
        combotime = combotimeMax;
    }

    public void playMiss()
    {
        StartCoroutine(Miss());
    }

    IEnumerator Miss()
    {
        combotime -= combotimeMax/16;
        float t = 0f;
        while (t < 1f)
        {
            t += Time.deltaTime / 1f;
            rectangle.localScale = new Vector3(combotime / combotimeMax * 1.8f, Mathf.Lerp(2f, 1f, t), 1f);
            yield return null;
        }
    }
}
