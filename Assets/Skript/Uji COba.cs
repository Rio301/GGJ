using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;


public class UjiCOba : MonoBehaviour
{
    [SerializeField] SpriteRenderer sp,sp1,sp2,sp3;
    [SerializeField] Sprite[] daftarbutton;
    [SerializeField] Sprite sp14;
    
    // Start is called before the first frame update
    void Start()
    {
        int i = Random.Range(0, daftarbutton.Length);
        Debug.Log(i);
    }

    // Update is called once per frame
    void Update()
    {
        
        InputButton();
    }
    void InputButton()
    {
        if (Input.GetKeyDown(KeyCode.W))
        {
            sp.color = Color.red;
        }
        else if (Input.GetKeyUp(KeyCode.W))
        {
            sp.color= Color.white;
        }
        if (Input.GetKeyDown(KeyCode.E))
        {
            sp1.color = Color.red;
        }
        else if (Input.GetKeyUp(KeyCode.E))
        {
            sp1.color = Color.white;
        }
        if (Input.GetKeyDown(KeyCode.R))
        {
            sp2.color = Color.red;
        }
        else if (Input.GetKeyUp(KeyCode.R))
        {
            sp2.color = Color.white;
        }
        if (Input.GetKeyDown(KeyCode.T))
        {
            sp3.color = Color.red;
        }
        else if (Input.GetKeyUp(KeyCode.T))
        {
            sp3.color = Color.white;
        }

    }
}
