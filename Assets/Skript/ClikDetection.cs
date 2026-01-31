using UnityEngine;

public class ClickDetection : MonoBehaviour
{
    public Vector2 offset;          // offset in WORLD units
    public Transform target;

    SoundManejer soundManejer;

    void Start()
    {
        soundManejer = GameObject
            .FindGameObjectWithTag("audio")
            .GetComponent<SoundManejer>();
    }

    void Update()
    {
        Vector3 mouseWorldPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mouseWorldPos.z = 0f;

        Vector2 rayOrigin = (Vector2)mouseWorldPos + offset;

        // Optional: visualize
        target.position = rayOrigin;

        if (Input.GetMouseButtonDown(0))
        {
            RaycastHit2D hit = Physics2D.Raycast(rayOrigin, Vector2.zero);

            if (hit.collider == null) return;

            Transform clickObject = hit.collider.transform;

            if (clickObject.CompareTag("enemy"))
            {
                soundManejer.sfxSource.PlayOneShot(soundManejer.hit);
                clickObject.GetComponent<AnimatorControler>()?.Dead(true);
            }
            else if (clickObject.CompareTag("board"))
            {
                soundManejer.sfxSource.PlayOneShot(soundManejer.miss);
            }
        }
    }
}
