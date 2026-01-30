using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClikDetection : MonoBehaviour
{
    Vector3 mousePosition;
    RaycastHit2D raycastHit2;
    Transform clickObject;
    // Start is called before the first frame update
    void Start()
    {
        
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
                clickObject.GetComponent<SpriteRenderer>().color = Color.red;
            }
        }
        if (Input.GetMouseButtonUp(0))
        {
            raycastHit2 = Physics2D.Raycast(mouseRay.origin, mouseRay.direction);
            clickObject = raycastHit2 ? raycastHit2.collider.transform : null;

            if (clickObject)
            {
                clickObject.GetComponent<SpriteRenderer>().color = Color.white;
            }
        }

        
    }
}
