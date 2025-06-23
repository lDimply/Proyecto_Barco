using UnityEngine;

public class ImpulseZone : MonoBehaviour
{
    public BoatBasicController basicController;
    public BoatController impulseController;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            basicController.enabled = false;
            impulseController.ActivateImpulseMode();
            impulseController.OnImpulseComplete += ReturnToNormalControl;
        }
    }

    void ReturnToNormalControl()
    {
        basicController.enabled = true;
        impulseController.OnImpulseComplete -= ReturnToNormalControl; // evitar múltiples suscripciones
    }
}
