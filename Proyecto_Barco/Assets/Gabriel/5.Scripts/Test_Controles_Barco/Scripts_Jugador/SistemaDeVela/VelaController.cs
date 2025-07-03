using UnityEngine;

public class VelaController : MonoBehaviour
{
    [Header("Estado de la vela")]
    public bool velaIzada = false;

    [Header("Visual de la vela")]
    public GameObject velaVisual; // Se puede cambiar en el futuro

    [Header("Feedback")]
    public AudioSource audioSource;
    public AudioClip sonidoIzar;
    public AudioClip sonidoArriar;
    public Transform feedbackEscalaTarget;
    public float escalaFeedback = 1.1f;
    public float duracionFeedback = 0.2f;

    void Start()
    {
        if (velaVisual != null)
            velaVisual.SetActive(velaIzada);
    }

    public void IzarVela()
    {
        velaIzada = true;

        if (velaVisual != null)
            velaVisual.SetActive(true);

        ReproducirFeedback(sonidoIzar);
       
    }

    public void ArriarVela()
    {
        velaIzada = false;

        if (velaVisual != null)
            velaVisual.SetActive(false);

        ReproducirFeedback(sonidoArriar);
       
    }

    private void ReproducirFeedback(AudioClip clip)
    {
        if (audioSource != null && clip != null)
            audioSource.PlayOneShot(clip);

        if (feedbackEscalaTarget != null)
            StopAllCoroutines(); // evitar solapamiento
        StartCoroutine(EscalaFeedbackCoroutine());
    }

    private System.Collections.IEnumerator EscalaFeedbackCoroutine()
    {
        Vector3 escalaOriginal = feedbackEscalaTarget.localScale;
        Vector3 escalaAumentada = escalaOriginal * escalaFeedback;

        float t = 0f;
        while (t < duracionFeedback)
        {
            feedbackEscalaTarget.localScale = Vector3.Lerp(escalaOriginal, escalaAumentada, t / duracionFeedback);
            t += Time.deltaTime;
            yield return null;
        }

        t = 0f;
        while (t < duracionFeedback)
        {
            feedbackEscalaTarget.localScale = Vector3.Lerp(escalaAumentada, escalaOriginal, t / duracionFeedback);
            t += Time.deltaTime;
            yield return null;
        }

        feedbackEscalaTarget.localScale = escalaOriginal;
    }

    public bool EstaIzada()
    {
        return velaIzada;
    }
}
