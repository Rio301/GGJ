using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;


public class EndGame : MonoBehaviour
{
    public TextMeshProUGUI scoreText;
    public float volume;
    public AudioSource sfx;
    public AudioClip win, gameOver;

    public GameObject gameOverTitle, winTitle;
    // Start is called before the first frame update
    Data data;
    void Start()
    {

        if (Data.health <= 0)
        {
            gameOverTitle.SetActive(true);
            winTitle.SetActive(false);
            sfx.clip = gameOver;
            sfx.Play();
        }
        else if (Data.score >= 15)
        {
            winTitle.SetActive(true);
            gameOverTitle.SetActive(false);
            sfx.clip = win;
            sfx.Play();
            sfx.volume = 5;
        }
    }

}
