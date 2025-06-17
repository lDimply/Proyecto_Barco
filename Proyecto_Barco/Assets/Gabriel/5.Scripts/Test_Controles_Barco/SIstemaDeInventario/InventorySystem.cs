using System.Collections.Generic;
using UnityEngine;

public class InventorySystem : MonoBehaviour
{
    public int capacidad = 20;
    public List<InventorySlot> slots = new List<InventorySlot>();

    void Start()
    {
        for (int i = 0; i < capacidad; i++)
            slots.Add(new InventorySlot());
    }

    public bool AñadirItem(ItemData nuevoItem, int cantidad = 1)
    {
        // 1) intentar apilar
        foreach (var slot in slots)
        {
            if (slot.item == nuevoItem && slot.cantidad < nuevoItem.maxStack)
            {
                slot.cantidad += cantidad;
                return true;
            }
        }
        // 2) buscar hueco vacío
        foreach (var slot in slots)
        {
            if (slot.EstaVacio)
            {
                slot.item = nuevoItem;
                slot.cantidad = cantidad;
                return true;
            }
        }
        Debug.Log("Inventario lleno");
        return false;
    }
}
