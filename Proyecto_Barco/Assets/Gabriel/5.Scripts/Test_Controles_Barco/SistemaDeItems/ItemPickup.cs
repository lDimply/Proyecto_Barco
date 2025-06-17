using UnityEngine;

public class ItemPickup : MonoBehaviour
{
    public ItemData item;
    public int cantidad = 1;

    private void OnTriggerEnter(Collider other)
    {
        InventorySystem inventario = other.GetComponent<InventorySystem>();
        if (inventario != null && inventario.AñadirItem(item, cantidad))
        {
            Debug.Log($"Recogiste: {item.itemName} x{cantidad}");
            Destroy(gameObject);
        }
    }
}
