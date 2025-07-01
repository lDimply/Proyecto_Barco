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
                SaveData.AñadirVidaExtra();
            }

            // Agrega aquí más efectos de ítems permanentes
        }
        else if (item.categoriaEspecial == ItemCategoria.ItemGastable)
        {
            // Aquí podrías poner efectos como curación, buffs, etc.
            Debug.Log($"Efecto gastable de {item.itemName} aún no implementado.");
        }
    }
}
