using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonManajer : MonoBehaviour
{
    public GameObject panelRole;
    public AudioSource sfx;
    public float timer = .5f;

    // Start is called before the first frame update
    void Start()
    {
        if (panelRole == null)
        {

        }
        if (sfx == null)
        {

        }
        
    }

    // Update is called once per frame
    void Update()
    {
        
        
    }
    public void startTheGame()
    {
        sfx.Play();
        Data.score = 0;
        StartCoroutine(LoadSceneInGame());
        

    }
    public void exitTheGame()
    {
        sfx.Play();
        Debug.Log("Kamu Pergi Tampa Aba Aba");
        StartCoroutine(timeToexitTheGame());
    }
    
    public void Continue()
    {
        panelRole.SetActive(false);
    }
    IEnumerator LoadSceneInGame()
    {
        yield return new WaitForSeconds(timer);
        SceneManager.LoadScene(1);
    }
    IEnumerator timeToexitTheGame()
    {
        yield return new WaitForSeconds(timer);
        
        Application.Quit();
    }


}
