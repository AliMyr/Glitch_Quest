public interface IInventoryComponent : ICharacterComponent, IUpdatableMechanic
{
    bool HasItem { get; }
    Item CurrentItem { get; }
    void PickupItem();
    void DropItem();
}
