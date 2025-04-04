using UnityEngine;

public class InventoryMechanic : IMechanic, IUpdatableMechanic
{
    private Character character;
    private Item currentItem;
    private GameObject itemPrefab;
    private float pickupRadius = 2f;

    public InventoryMechanic(GameObject itemPrefab)
    {
        this.itemPrefab = itemPrefab;
    }

    public void Initialize(Character character) => this.character = character;

    public void Enable() { }

    public void Disable() { }

    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            if (currentItem == null)
                PickupNearbyItem();
            else
                Debug.Log("Inventory full");
        }

        if (Input.GetKeyDown(KeyCode.U))
        {
            if (currentItem != null)
            {
                Item duplicate = new Item(currentItem.Name);
                Debug.Log("Item duplicated: " + duplicate.Name);
            }
        }

        if (Input.GetKeyDown(KeyCode.Q))
        {
            if (currentItem != null)
                DropItem();
        }
    }

    private void PickupNearbyItem()
    {
        Collider[] colliders = Physics.OverlapSphere(character.CharacterTransform.position, pickupRadius);
        foreach (var collider in colliders)
        {
            if (collider.CompareTag("Pickup"))
            {
                PickupItem pickup = collider.GetComponent<PickupItem>();
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

    private void DropItem()
    {
        if (itemPrefab != null)
        {
            Vector3 dropPosition = character.CharacterTransform.position + character.CharacterTransform.forward;
            Object.Instantiate(itemPrefab, dropPosition, Quaternion.identity);
        }
        Debug.Log("Item dropped: " + currentItem.Name);
        currentItem = null;
    }
}
