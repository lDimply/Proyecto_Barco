using UnityEngine;

[RequireComponent(typeof(Collider))]
public class ItemPickup : MonoBehaviour
{
    public ItemData itemData;

    private void OnTriggerEnter(Collider other)
    {
        InventorySystem inv = other.GetComponent<InventorySystem>();
        if (inv != null && itemData != null)
        {
            inv.AddItem(itemData);
            Destroy(gameObject);
        }
    }
}
