using UnityEngine;

public interface IInventoryComponent : ICharacterComponent, IUpdatableMechanic
{
    bool HasItem { get; }
    Item CurrentItem { get; }
}
