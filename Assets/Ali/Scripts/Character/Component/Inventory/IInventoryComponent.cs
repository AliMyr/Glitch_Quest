public interface IInventoryComponent : ICharacterComponent
{
    bool HasItem { get; }
    Item CurrentItem { get; }
    void PickupItem();
    void DropItem();
    void ActivateInventory();
    void DeactivateInventory();
}
