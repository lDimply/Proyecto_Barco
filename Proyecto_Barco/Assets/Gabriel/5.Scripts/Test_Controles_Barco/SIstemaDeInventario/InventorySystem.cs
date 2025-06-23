using System.Collections.Generic;
using UnityEngine;

public class InventorySystem : MonoBehaviour
{
    [Header("Referencia opcional a la UI")]
    public InventoryUI inventoryUI;      // arr�stralo en el Inspector (o d�jalo vac�o)

    [Header("Capacidad")]
    public int capacidad = 20;

    private List<InventoryItem> items = new List<InventoryItem>();

    // Contador para monedas recogidas
    public int totalCoins = 0;

    // ---------- API p�blica ----------
    public IReadOnlyList<InventoryItem> GetItems() => items;

    public void AddItem(ItemData data)
    {
        // Si el objeto est� marcado para ocultarse en el inventario visual
        if (data.ocultarEnInventario)
        {
            // Puedes hacer l�gica especial aqu� si quieres (como sumar monedas)
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
            Debug.Log($"Se apil� {data.itemName}. Total: {existing.quantity}");
        }
        else
        {
            if (items.Count >= capacidad)
            {
                Debug.LogWarning("Inventario lleno");
                return;
            }
            items.Add(new InventoryItem(data));
            Debug.Log($"Se a�adi� nuevo �tem: {data.itemName}");
        }

        // Refrescar UI si est� asignada
        if (inventoryUI != null)
        {
            inventoryUI.UpdateUI(items);
        }
    }

}
