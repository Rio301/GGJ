using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;


public class EndGame : MonoBehaviour
{
    public TextMeshProUGUI scoreText;
    public AudioSource sfx;
    public AudioClip win, gameOver;
    // Start is called before the first frame update
    private void Awake()
    {
        if (Data.health <= 0)
        {
            sfx.clip = gameOver;
        }
        else if (Data.score >= 40)
        {
            sfx.clip = win;
        }
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
