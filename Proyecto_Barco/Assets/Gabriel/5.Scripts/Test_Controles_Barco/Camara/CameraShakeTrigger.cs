using UnityEngine;
using Unity.Cinemachine;

public class CameraShakeTrigger : MonoBehaviour
{

    public static CameraShakeTrigger Instance;

    public CinemachineImpulseSource impulseSource;



    public void GenerarShake()
    {
        if (impulseSource != null)
        {
            impulseSource.GenerateImpulse();
            Debug.Log("Impulso generado.");
        }
    }
}