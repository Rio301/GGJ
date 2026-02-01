using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonManajer : MonoBehaviour
{
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void startTheGame(string inGameScene)
    {
        SceneManager.LoadScene(inGameScene);
        Data.score = 0;

    }
    public void exitTheGame()
    {
        Application.Quit();
        Debug.Log("Kamu Pergi Tampa Aba Aba");
    }
    public void Continue()
    {

    }
    
}
