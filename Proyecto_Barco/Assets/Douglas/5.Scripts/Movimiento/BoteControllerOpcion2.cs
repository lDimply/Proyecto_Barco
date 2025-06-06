﻿using UnityEngine;

public class BoteControllerOpcion2 : MonoBehaviour
{
    private Rigidbody boteRb;
    public float fuerzaBase = 10f;
    public float maxSwipeDist = 300f;
    private Vector2 swipeStart;
    private bool isDragging = false;

    public Camera cam;

    public Transform flecha; // referencia a la flecha indicadora

    void Awake()
    {
        boteRb = GetComponent<Rigidbody>();
        if (flecha != null)
            flecha.gameObject.SetActive(false); // desactiva flecha al inicio
    }

    void Update()
    {
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);

            if (touch.phase == TouchPhase.Began)
            {
                swipeStart = touch.position;
                isDragging = true;

                if (flecha != null)
                    flecha.gameObject.SetActive(true);
            }
            else if (touch.phase == TouchPhase.Moved && isDragging)
            {
                Vector2 swipeCurrent = touch.position;
                Vector2 swipeDelta = swipeCurrent - swipeStart;

                if (flecha != null && swipeDelta.magnitude > 10f)
                {
                    Plane plano = new Plane(Vector3.up, boteRb.position);

                    Ray rayStart = cam.ScreenPointToRay(swipeStart);
                    Ray rayCurrent = cam.ScreenPointToRay(swipeCurrent);

                    if (plano.Raycast(rayStart, out float distStart) && plano.Raycast(rayCurrent, out float distCurrent))
                    {
                        Vector3 worldStart = rayStart.GetPoint(distStart);
                        Vector3 worldCurrent = rayCurrent.GetPoint(distCurrent);

                        Vector3 swipeDir = (worldStart - worldCurrent).normalized;

                        if (swipeDir != Vector3.zero)
                        {
                            // Calculamos el ángulo en grados en el plano X-Z (en sentido horario)
                            float angleZ = Mathf.Atan2(swipeDir.x, swipeDir.z) * Mathf.Rad2Deg;
                            flecha.rotation = Quaternion.Euler(0, 0, -angleZ); // gira solo sobre el eje Z
                        }
                    }
                }
            }
            else if (touch.phase == TouchPhase.Ended && isDragging)
            {
                Vector2 swipeEnd = touch.position;
                Vector2 swipeDelta = swipeEnd - swipeStart;

                if (swipeDelta.magnitude > 20f)
                {
                    ApplySwipeForce(swipeStart, swipeEnd);
                }

                isDragging = false;

                if (flecha != null)
                    flecha.gameObject.SetActive(false);
            }
        }
    }

    void ApplySwipeForce(Vector2 startScreen, Vector2 endScreen)
    {
        Plane plano = new Plane(Vector3.up, boteRb.position);

        Ray rayStart = cam.ScreenPointToRay(startScreen);
        Ray rayEnd = cam.ScreenPointToRay(endScreen);

        if (plano.Raycast(rayStart, out float distStart) && plano.Raycast(rayEnd, out float distEnd))
        {
            Vector3 worldStart = rayStart.GetPoint(distStart);
            Vector3 worldEnd = rayEnd.GetPoint(distEnd);

            Vector3 swipeDir = (worldStart - worldEnd).normalized;

            float swipeDistance = Vector3.Distance(worldStart, worldEnd);
            float fuerzaFinal = fuerzaBase * Mathf.Clamp01(swipeDistance / maxSwipeDist);

            if (swipeDir != Vector3.zero)
            {
                Quaternion rot = Quaternion.LookRotation(swipeDir, Vector3.up);
                boteRb.MoveRotation(rot);
            }

            boteRb.AddForce(swipeDir * fuerzaFinal, ForceMode.Impulse);

            Debug.DrawRay(transform.position, swipeDir * 5f, Color.cyan, 2f);
            Debug.Log($"SwipeDir: {swipeDir}, Fuerza: {fuerzaFinal}");
        }
    }
}
