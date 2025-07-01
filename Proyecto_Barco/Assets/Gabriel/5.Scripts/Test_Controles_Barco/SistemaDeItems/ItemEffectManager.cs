using UnityEngine;

public static class ItemEffectManager
{
    public static void AplicarEfecto(ItemData item)
    {
        if (item == null) return;

        if (item.categoriaEspecial == ItemCategoria.ItemPermanente)
        {
            if (item.itemName == "vidasExtraPermanente")
            {
                SaveData.A�adirVidaExtra();
            }

            // Agrega aqu� m�s efectos de �tems permanentes
        }
        else if (item.categoriaEspecial == ItemCategoria.ItemGastable)
        {
            // Aqu� podr�as poner efectos como curaci�n, buffs, etc.
            Debug.Log($"Efecto gastable de {item.itemName} a�n no implementado.");
        }
    }
}
