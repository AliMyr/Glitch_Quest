using UnityEngine;

public class InventoryMechanic : MonoBehaviour, IMechanic, IUpdatableMechanic
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
            PickupNearbyItem();
        if (Input.GetKeyDown(KeyCode.U) && currentItem != null)
            Debug.Log("Item duplicated: " + new Item(currentItem.Name).Name);
        if (Input.GetKeyDown(KeyCode.Q))
            DropItem();
    }

    private void PickupNearbyItem()
    {
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
                    Destroy(collider.gameObject);
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
            Vector3 dropPosition = character.CharacterTransform.position +
                                   character.CharacterTransform.forward +
                                   new Vector3(0, 0.5f, 0);
            Instantiate(itemPrefab, dropPosition, Quaternion.identity);
        }
        Debug.Log("Item dropped: " + currentItem?.Name);
        currentItem = null;
    }

}
