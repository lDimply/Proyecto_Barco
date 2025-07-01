using UnityEngine;

public class PulsoVisual : MonoBehaviour
{
    [Header("Parámetros del Pulso")]
    public float escalaMin = 0.95f;
    public float escalaMax = 1.05f;
    public float velocidad = 2f;

    private Vector3 escalaOriginal;

    private void Start()
    {
        escalaOriginal = transform.localScale;
    }

    private void Update()
    {
        float t = (Mathf.Sin(Time.time * velocidad) + 1f) / 2f;
        float escala = Mathf.Lerp(escalaMin, escalaMax, t);
        transform.localScale = escalaOriginal * escala;
    }
}


