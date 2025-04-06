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
    }

    public void PickupItem()
    {
        if (currentItem != null)
        {
            Debug.Log("Inventory is full.");
            return;
        }
        Collider[] colliders = Physics.OverlapSphere(character.CharacterTransform.position, pickupRadius);
        foreach (var collider in colliders)
        {
            if (collider.CompareTag("Pickup"))
            {
                var pickup = collider.GetComponent<PickupItem>();
                if (pickup != null)
                {
                    currentItem = new Item(pickup.itemName);
                    Debug.Log("Picked up item: " + currentItem.Name);
                    Object.Destroy(collider.gameObject);
                    return;
                }
            }
        }
        Debug.Log("No pickup item found nearby.");
    }

    public void DropItem()
    {
        if (currentItem == null)
        {
            Debug.Log("No item to drop.");
            return;
        }
        if (allowedPrefabs.TryGetValue(currentItem.Name, out var prefab))
        {
            Vector3 dropPosition = character.CharacterTransform.position +
                                   character.CharacterTransform.forward +
                                   new Vector3(0, 0.5f, 0);
            Object.Instantiate(prefab, dropPosition, Quaternion.identity);
            Debug.Log("Dropped item: " + currentItem.Name);
            currentItem = null;
        }
        else
        {
            Debug.Log("No allowed prefab for item: " + currentItem.Name);
        }
    }

    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
            PickupItem();
        if (Input.GetKeyDown(KeyCode.Q))
            DropItem();
    }
}
