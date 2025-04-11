using UnityEngine;

public class BoteControllerOpcion3 : MonoBehaviour
{
    private Rigidbody boteRb;
    public float velocidad = 5f;
    public bool estaAcelerando = false;

    private void Awake()
    {
        boteRb = GetComponent<Rigidbody>();
        Input.gyro.enabled = true;
    }

    void Update()
    {
        if (estaAcelerando)
        {
            // Dirección de rotación basada en el giroscopio
            Vector3 direccion = ObtenerDireccionDesdeGiroscopio();

            if (direccion != Vector3.zero)
            {
                // Calcular la rotación hacia la dirección del giroscopio
                Quaternion rot = Quaternion.LookRotation(direccion, Vector3.up);
                boteRb.MoveRotation(Quaternion.Slerp(transform.rotation, rot, Time.deltaTime * 5f));

                // Acelerar hacia adelante
                boteRb.AddForce(transform.forward * velocidad, ForceMode.Acceleration);
            }
        }
    }

    Vector3 ObtenerDireccionDesdeGiroscopio()
    {
        // Obtener el ángulo de rotación del giroscopio
        Quaternion att = Input.gyro.attitude;

        // Ajustar el quaternion para el modo retrato (en vertical)
        Quaternion correccion = new Quaternion(att.x, att.y, -att.z, -att.w); // Corrige la orientación para el dispositivo en vertical

        // Convertir a grados eulerianos
        Vector3 euler = correccion.eulerAngles;

        // Usamos solo el valor del ángulo Y (yaw) para determinar la dirección en XZ
        float yaw = euler.y;
        float rad = yaw * Mathf.Deg2Rad;

        // Dirección calculada
        Vector3 direccion = new Vector3(Mathf.Sin(rad), 0, Mathf.Cos(rad));
        return direccion.normalized;
    }

    // Métodos de aceleración
    public void IniciarAceleracion() => estaAcelerando = true;
    public void DetenerAceleracion() => estaAcelerando = false;
}