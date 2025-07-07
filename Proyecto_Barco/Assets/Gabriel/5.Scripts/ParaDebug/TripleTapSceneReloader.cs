using UnityEngine;
using UnityEngine.SceneManagement;

public class TripleTapSceneReloader : MonoBehaviour
{
    [Header("Configuración del Triple Tap")]
    public float maxIntervalBetweenTaps = 0.5f; // Tiempo máximo entre taps
    private int tapCount = 0;
    private float lastTapTime = 0f;

    void Update()
    {
        if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Ended)
        {
            float currentTime = Time.time;

            if (currentTime - lastTapTime < maxIntervalBetweenTaps)
            {
                tapCount++;
            }
            else
            {
                tapCount = 1; // reinicia si se tarda mucho entre taps
            }

            lastTapTime = currentTime;

            if (tapCount >= 4)
            {
                
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
            }
        }

#if UNITY_EDITOR
        // Para testear en editor con triple clic izquierdo
        if (Input.GetMouseButtonDown(0))
        {
            float currentTime = Time.time;

            if (currentTime - lastTapTime < maxIntervalBetweenTaps)
            {
                tapCount++;
            }
            else
            {
                tapCount = 1;
            }

            lastTapTime = currentTime;

            if (tapCount >= 5)
            {
                
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
            }
        }
#endif
    }
}
