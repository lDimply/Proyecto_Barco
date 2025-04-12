using UnityEngine;

public class BoteControllerOpcion3 : MonoBehaviour
{
    private Rigidbody boteRb;
    public float velocidad = 5f;
    public float velocidadGiro = 5f;
    public bool estaAcelerando = false;

    // Rango máximo de inclinación lateral para controlar el giro
    public float maxAnguloDispositivo = 45f;
    public float maxAnguloBote = 45f;

    private void Awake()
    {
        boteRb = GetComponent<Rigidbody>();
        Input.gyro.enabled = true;
    }

    void Update()
    {
        // Obtener inclinación lateral
        float anguloZ = ObtenerAnguloDesdeGiroscopio();

        if (!float.IsNaN(anguloZ))
        {
            // Invertimos el ángulo para que se sienta natural
            anguloZ = -anguloZ;

            // Normalizamos el ángulo de -maxAnguloDispositivo a +maxAnguloDispositivo
            anguloZ = Mathf.Clamp(anguloZ, -maxAnguloDispositivo, maxAnguloDispositivo);

            // Convertimos ese ángulo al rango de rotación del bote
            float anguloBote = (anguloZ / maxAnguloDispositivo) * maxAnguloBote;

            // Aplicamos la rotación suavizada
            Quaternion rotacionObjetivo = Quaternion.Euler(0, anguloBote, 0);
            boteRb.MoveRotation(Quaternion.Slerp(transform.rotation, rotacionObjetivo, Time.deltaTime * velocidadGiro));
        }

        // Acelera si está presionado
        if (estaAcelerando)
        {
            boteRb.AddForce(transform.forward * velocidad, ForceMode.Acceleration);
        }
    }

    float ObtenerAnguloDesdeGiroscopio()
    {
        Quaternion att = Input.gyro.attitude;
        Quaternion correccion = new Quaternion(att.x, att.y, -att.z, -att.w);
        Vector3 euler = correccion.eulerAngles;

        // Tomamos el ángulo Z (inclinación lateral)
        float roll = euler.z;

        // Pasamos de 0-360 a -180 a 180 para facilitar los cálculos
        if (roll > 180) roll -= 360;

        return roll;
    }

    public void IniciarAceleracion() => estaAcelerando = true;
    public void DetenerAceleracion() => estaAcelerando = false;
}