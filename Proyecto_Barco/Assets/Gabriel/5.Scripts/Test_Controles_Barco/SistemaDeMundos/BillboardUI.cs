using UnityEngine;

public class BillboardUI : MonoBehaviour
{
    private Transform objetivo;

    private void Start()
    {
        objetivo = Camera.main.transform;
    }


    private void LateUpdate()
    {
        if (objetivo == null) return;

        Vector3 direccion = transform.position - objetivo.position;
        transform.rotation = Quaternion.LookRotation(direccion);
    }
}
