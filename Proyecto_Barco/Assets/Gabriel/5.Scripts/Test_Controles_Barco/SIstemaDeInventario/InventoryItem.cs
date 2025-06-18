[System.Serializable]
public class InventoryItem
{
    public ItemData data;
    public int quantity;

    public InventoryItem(ItemData data)
    {
        this.data = data;
        this.quantity = 1;
    }
}
