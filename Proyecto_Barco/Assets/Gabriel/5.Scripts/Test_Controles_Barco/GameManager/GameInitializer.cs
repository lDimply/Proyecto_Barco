using UnityEngine;

public class GameInitializer : MonoBehaviour
{
    [Header("Prefab del GameManager")]
    public GameObject gameManagerPrefab;

    void Awake()
    {
        if (GameManager.Instance == null)
        {
            Instantiate(gameManagerPrefab);
        }
    }
}
