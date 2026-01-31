using System.Collections;
using UnityEngine;

public class HammerObject : MonoBehaviour
{
    public float rotationAngle = -45f;
    public float animationDuration = 0.5f;
    public float zDepth = 0f; // set this so hammer stays visible

    private Camera mainCam;
    private bool isAnimating;
    private Quaternion originalRotation;

    void Start()
    {
        Cursor.visible = false;
        mainCam = Camera.main;
        originalRotation = transform.rotation;
    }

    void Update()
    {
        FollowMouseWorld();

        if (Input.GetMouseButtonDown(0) && !isAnimating)
        {
            StartCoroutine(HammerSwing());
        }
    }

    void FollowMouseWorld()
    {
        Vector3 mouse = Input.mousePosition;
        mouse.z = Mathf.Abs(mainCam.transform.position.z) + zDepth;

        Vector3 worldPos = mainCam.ScreenToWorldPoint(mouse);
        worldPos.z = zDepth;

        transform.position = worldPos;
    }

    IEnumerator HammerSwing()
    {
        isAnimating = true;

        Quaternion targetRotation =
            originalRotation * Quaternion.Euler(0, 0, rotationAngle);

        float halfDuration = animationDuration / 2f;
        float t = 0f;

        // Swing down
        while (t < 1f)
        {
            t += Time.deltaTime / halfDuration;
            transform.rotation = Quaternion.Slerp(originalRotation, targetRotation, t);
            yield return null;
        }

        t = 0f;

        // Return
        while (t < 1f)
        {
            t += Time.deltaTime / halfDuration;
            transform.rotation = Quaternion.Slerp(targetRotation, originalRotation, t);
            yield return null;
        }

        isAnimating = false;
    }

    void OnDestroy()
    {
        Cursor.visible = true;
    }
}
