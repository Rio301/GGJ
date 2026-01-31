using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class Hammer : MonoBehaviour
{
    public float rotationAngle = -45f;
    public float animationDuration = 0.5f;
    public Image hammerImage;

    private RectTransform rectTransform;
    private Canvas canvas;
    private Camera uiCamera;
    private bool isAnimating;
    private Quaternion originalRotation;

    void Start()
    {
        Cursor.visible = false;

        rectTransform = GetComponent<RectTransform>();
        canvas = GetComponentInParent<Canvas>();
        uiCamera = canvas.worldCamera;

        originalRotation = rectTransform.rotation;

        // Safety
        if (hammerImage != null)
            hammerImage.raycastTarget = false;
    }

    void Update()
    {
        FollowMouseWorldSpace();

        if (Input.GetMouseButtonDown(0) && !isAnimating)
        {
            StartCoroutine(HammerSwing());
        }
    }

    void FollowMouseWorldSpace()
    {
        Vector2 localPoint;

        RectTransformUtility.ScreenPointToLocalPointInRectangle(
            canvas.transform as RectTransform,
            Input.mousePosition,
            uiCamera,
            out localPoint
        );

        rectTransform.localPosition = localPoint;
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
            rectTransform.rotation = Quaternion.Slerp(originalRotation, targetRotation, t);
            yield return null;
        }

        t = 0f;

        // Return
        while (t < 1f)
        {
            t += Time.deltaTime / halfDuration;
            rectTransform.rotation = Quaternion.Slerp(targetRotation, originalRotation, t);
            yield return null;
        }

        isAnimating = false;
    }

    void OnDestroy()
    {
        Cursor.visible = true;
    }
}
