[System.Serializable]
public class InventorySlot
{
    public ItemData item;
    public int cantidad;

    public bool EstaVacio => item == null || cantidad <= 0;

    public void LimpiarSlot()
    {
        item = null;
        cantidad = 0;
    }
}
