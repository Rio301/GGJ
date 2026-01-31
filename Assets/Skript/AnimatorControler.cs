using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimatorControler : MonoBehaviour
{
    Animator anim;
    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        anim.SetTrigger("Spawn");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    
}
