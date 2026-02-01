using UnityEngine;

public class ClickDetection : MonoBehaviour
{
    public Vector2 offset;          // offset in WORLD units
    //public Transform target;
    public GameObject BonkPrefab;

    SoundManejer soundManejer;
    timer timer;

    void Start()
    {
        soundManejer = GameObject
            .FindGameObjectWithTag("audio")
            .GetComponent<SoundManejer>();
        timer = GameObject.FindGameObjectWithTag("timer").GetComponent<timer>();
    }

    void Update()
    {
        Vector3 mouseWorldPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mouseWorldPos.z = 0f;

        Vector2 rayOrigin = (Vector2)mouseWorldPos + offset;

        // Optional: visualize
        //target.position = rayOrigin;

        if (Input.GetMouseButtonDown(0))
        {
            RaycastHit2D hit = Physics2D.Raycast(rayOrigin, Vector2.zero);

            if (hit.collider == null) return;

            Transform clickObject = hit.collider.transform;

            if (clickObject.CompareTag("enemy"))
            {
                Data.score += 1;
                soundManejer.sfxSource.PlayOneShot(soundManejer.hit);
                clickObject.GetComponent<BoxCollider2D>().enabled = false;
                clickObject.GetComponent<CapsuleCollider2D>().enabled = false;
                clickObject.GetComponent<AnimatorControler>()?.Dead(true);
                timer.resetTimer();
                Vector3 bonkSpawnPoint = new Vector3(rayOrigin.x, rayOrigin.y, mouseWorldPos.z);
                GameObject obj =  Instantiate(BonkPrefab, bonkSpawnPoint, Quaternion.Euler(0f, 0f, Random.Range(-30f, 30f)));
                Destroy(obj, 3f);
            }
            else if (clickObject.CompareTag("hostileEnemy"))
            {
                Data.health -= 1;
                soundManejer.sfxSource.PlayOneShot(soundManejer.healtReduce2);
                clickObject.GetComponent<BoxCollider2D>().enabled = false;
                clickObject.GetComponent<CapsuleCollider2D>().enabled = false;
                clickObject.GetComponent<AnimatorControler>()?.Dead(true);
                timer.resetTimer();
            }
            
        }
    }
}
