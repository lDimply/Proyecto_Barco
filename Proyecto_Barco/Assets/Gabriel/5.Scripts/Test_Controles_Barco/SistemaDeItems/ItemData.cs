using UnityEngine;

[CreateAssetMenu(fileName = "Nuevo Item", menuName = "Inventario/Item")]
public class ItemData : ScriptableObject
{
    public string itemName;
    public Sprite icon;
    public ItemType itemType;
    public int maxStack = 1;
    public bool ocultarEnInventario = false;

    [Header("Categoría de ítem especial")]
    public ItemCategoria categoriaEspecial = ItemCategoria.Ninguna;
}

public enum ItemType
{
    Comida,
    Objeto,
    Llave,
    Material,
    Moneda,
    Vida

}

public enum ItemCategoria
{
    Ninguna,
    ItemPermanente,
    ItemGastable
}

