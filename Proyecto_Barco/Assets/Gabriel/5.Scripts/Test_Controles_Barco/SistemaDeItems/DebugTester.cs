using UnityEngine;

public class DebugTester : MonoBehaviour
{
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            SaveData.ResetearVidasExtra();
            PlayerPrefs.Save();
            Debug.Log("🔁 Vidas extra permanentes reiniciadas (para testeo)");
        }
    }
}
