// En un script llamado ImpulseUI.cs
using UnityEngine;
using UnityEngine.UI;

public class ImpulseUI : MonoBehaviour
{
    public Slider chargeSlider;
    public BoatController boat;

    void Update()
    {
        if (boat == null || chargeSlider == null) return;

        if (boat.isActiveAndEnabled)
        {
            chargeSlider.gameObject.SetActive(true);
            chargeSlider.value = Mathf.Clamp01(boat.GetChargePercent());
        }
        else
        {
            chargeSlider.gameObject.SetActive(false);
        }
    }
}
