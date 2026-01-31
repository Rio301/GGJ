using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClikDetection : MonoBehaviour
{
    Vector3 mousePosition;
    RaycastHit2D raycastHit2;
    Transform clickObject;

    SoundManejer soundManejer;
    // Start is called before the first frame update
    void Start()
    {
        soundManejer = GameObject.FindGameObjectWithTag("audio").GetComponent<SoundManejer>();
    }

    // Update is called once per frame
    void Update()
    {
        mousePosition = Input.mousePosition;
        Ray mouseRay = Camera.main.ScreenPointToRay(mousePosition);

        if (Input.GetMouseButtonDown(0))
        {
            raycastHit2 = Physics2D.Raycast(mouseRay.origin, mouseRay.direction);
            clickObject = raycastHit2 ? raycastHit2.collider.transform : null;;

            if (clickObject)
            {
                //clickObject.GetComponent<SpriteRenderer>().color = Color.red;
                if (clickObject.CompareTag("enemy"))
                {
                    soundManejer.sfxSource.PlayOneShot(soundManejer.hit);
                     AnimatorControler anim =  clickObject.GetComponent<AnimatorControler>();
                    anim.Dead(true);
                }
                if (clickObject.CompareTag("board"))
                {
                    soundManejer.sfxSource.PlayOneShot(soundManejer.miss);
                }
            }
        }
        if (Input.GetMouseButtonUp(0))
        {
            raycastHit2 = Physics2D.Raycast(mouseRay.origin, mouseRay.direction);
            clickObject = raycastHit2 ? raycastHit2.collider.transform : null;

            if (clickObject)
            {
                //clickObject.GetComponent<SpriteRenderer>().color = Color.white;
            }
        }

        
    }
}
