using System.Collections.Generic;
using UnityEngine;

public class InventorySystem : MonoBehaviour
{
    [Header("Referencia opcional a la UI")]
    public InventoryUI inventoryUI;      // arrástralo en el Inspector (o déjalo vacío)

    [Header("Capacidad")]
    public int capacidad = 20;

    private List<InventoryItem> items = new List<InventoryItem>();

    // Contador para monedas recogidas
    public int totalCoins = 0;

    // ---------- API pública ----------
    public IReadOnlyList<InventoryItem> GetItems() => items;

    public void AddItem(ItemData data)
    {
        // Si el objeto está marcado para ocultarse en el inventario visual
        if (data.ocultarEnInventario)
        {
            // Puedes hacer lógica especial aquí si quieres (como sumar monedas)
            if (data.itemType == ItemType.Moneda)
            {
                totalCoins++;
                Debug.Log($"Moneda recogida. Total monedas: {totalCoins}");
            }
            else
            {
                Debug.Log($"Item '{data.itemName}' recogido pero oculto en el inventario.");
            }

            return; // No se agrega a la lista
        }

        // Si ya existe y se puede apilar
        InventoryItem existing = items.Find(i => i.data == data);
        if (existing != null && existing.quantity < data.maxStack)
        {
            existing.quantity++;
            Debug.Log($"Se apiló {data.itemName}. Total: {existing.quantity}");
        }
        else
        {
            if (items.Count >= capacidad)
            {
                Debug.LogWarning("Inventario lleno");
                return;
            }
            items.Add(new InventoryItem(data));
            Debug.Log($"Se añadió nuevo ítem: {data.itemName}");
        }

        // Refrescar UI si está asignada
        if (inventoryUI != null)
        {
            inventoryUI.UpdateUI(items);
        }
    }

}
