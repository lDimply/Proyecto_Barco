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
            // Direcci�n de rotaci�n basada en el giroscopio
            Vector3 direccion = ObtenerDireccionDesdeGiroscopio();

            if (direccion != Vector3.zero)
            {
                // Calcular la rotaci�n hacia la direcci�n del giroscopio
                Quaternion rot = Quaternion.LookRotation(direccion, Vector3.up);
                boteRb.MoveRotation(Quaternion.Slerp(transform.rotation, rot, Time.deltaTime * 5f));

                // Acelerar hacia adelante
                boteRb.AddForce(transform.forward * velocidad, ForceMode.Acceleration);
            }
        }
    }

    Vector3 ObtenerDireccionDesdeGiroscopio()
    {
        // Obtener el �ngulo de rotaci�n del giroscopio
        Quaternion att = Input.gyro.attitude;

        // Ajustar el quaternion para el modo retrato (en vertical)
        Quaternion correccion = new Quaternion(att.x, att.y, -att.z, -att.w); // Corrige la orientaci�n para el dispositivo en vertical

        // Convertir a grados eulerianos
        Vector3 euler = correccion.eulerAngles;

        // Usamos solo el valor del �ngulo Y (yaw) para determinar la direcci�n en XZ
        float yaw = euler.y;
        float rad = yaw * Mathf.Deg2Rad;

        // Direcci�n calculada
        Vector3 direccion = new Vector3(Mathf.Sin(rad), 0, Mathf.Cos(rad));
        return direccion.normalized;
    }

    // M�todos de aceleraci�n
    public void IniciarAceleracion() => estaAcelerando = true;
    public void DetenerAceleracion() => estaAcelerando = false;
}