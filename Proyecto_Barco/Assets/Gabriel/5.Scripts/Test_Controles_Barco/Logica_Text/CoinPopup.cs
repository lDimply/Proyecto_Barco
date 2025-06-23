using UnityEngine;
using TMPro;

public class CoinPopup : MonoBehaviour
{
    public TMP_Text text;
    public float duration = 1.5f;
    public Vector3 floatOffset = new Vector3(0, 0.5f, 0);
    public float floatSpeed = 1f;

    private Transform targetToFollow;

    public void SetAmount(int amount)
    {
        if (text != null)
            text.text = $"+{amount}";
    }

    public void SetTarget(Transform target)
    {
        targetToFollow = target;
    }

    void Update()
    {
        if (targetToFollow != null)
        {
            transform.position = targetToFollow.position + floatOffset;
            transform.LookAt(Camera.main.transform);
            transform.Rotate(0, 180, 0);
        }

        Destroy(gameObject, duration);
    }
}
