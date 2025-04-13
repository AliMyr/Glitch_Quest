using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class InventoryComponent : IInventoryComponent
{
    private Character character;
    private Item currentItem = null;
    [SerializeField] private AllowedItemPrefab[] allowedItems;
    private Dictionary<string, GameObject> allowedPrefabs;
    private float pickupRadius = 2f;
    private bool isActive;

    public bool HasItem => currentItem != null;
    public Item CurrentItem => currentItem;

    public void Initialize(Character character)
    {
        this.character = character;
        allowedPrefabs = new Dictionary<string, GameObject>();
        foreach (var item in allowedItems)
        {
            if (item != null && !allowedPrefabs.ContainsKey(item.itemName))
                allowedPrefabs.Add(item.itemName, item.prefab);
        }
        isActive = false;
    }

    public void ActivateInventory() => isActive = true;
    public void DeactivateInventory() => isActive = false;

    public void PickupItem()
    {
        if (!isActive)
        {
            return;
        }
        if (currentItem != null)
        {
            return;
        }
        Collider[] colliders = Physics.OverlapSphere(character.CharacterTransform.position, pickupRadius);
        foreach (var collider in colliders)
        {
            if (collider.CompareTag("Pickup"))
            {
                var pickup = collider.GetComponent<PItem>();
                if (pickup != null)
                {
                    currentItem = new Item(pickup.itemName);
                    Object.Destroy(collider.gameObject);
                    return;
                }
            }
        }
    }

    public void DropItem()
    {
        if (!isActive)
        {
            return;
        }
        if (currentItem == null)
        {
            return;
        }
        if (allowedPrefabs.TryGetValue(currentItem.Name, out var prefab))
        {
            Vector3 dropPosition = character.CharacterTransform.position +
                                   character.CharacterTransform.forward +
                                   new Vector3(0, 0.5f, 0);
            Object.Instantiate(prefab, dropPosition, Quaternion.identity);
            currentItem = null;
        }
    }
}
