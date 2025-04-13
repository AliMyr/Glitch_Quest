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

        // Заполняем словарь префабов
        if (allowedItems != null && allowedItems.Length > 0)
        {
            foreach (var item in allowedItems)
            {
                if (item != null && !string.IsNullOrEmpty(item.itemName) &&
                    item.prefab != null && !allowedPrefabs.ContainsKey(item.itemName))
                {
                    allowedPrefabs.Add(item.itemName, item.prefab);
                    Debug.Log($"InventoryComponent: Registered prefab for {item.itemName}");
                }
            }
        }
        else
        {
            Debug.LogWarning("InventoryComponent: No allowed items configured!");
        }

        // Автоматически активируем инвентарь при инициализации
        isActive = true;
        Debug.Log("InventoryComponent: Initialized and activated");
    }

    public void ActivateInventory()
    {
        isActive = true;
        Debug.Log("InventoryComponent: Activated");
    }

    public void DeactivateInventory()
    {
        isActive = false;
        Debug.Log("InventoryComponent: Deactivated");
    }

    public void PickupItem()
    {
        Debug.Log("InventoryComponent: PickupItem called, isActive=" + isActive);

        if (!isActive)
        {
            Debug.LogWarning("InventoryComponent: Cannot pickup, inventory not active! Activating now...");
            ActivateInventory();
        }

        if (currentItem != null)
        {
            Debug.Log("InventoryComponent: Already have an item: " + currentItem.Name);
            return;
        }

        Collider[] colliders = Physics.OverlapSphere(character.CharacterTransform.position, pickupRadius);
        Debug.Log($"InventoryComponent: Found {colliders.Length} colliders in radius {pickupRadius}");

        foreach (var collider in colliders)
        {
            if (collider.CompareTag("Pickup"))
            {
                Debug.Log($"InventoryComponent: Found pickup: {collider.name}");
                var pickup = collider.GetComponent<PItem>();
                if (pickup != null)
                {
                    currentItem = new Item(pickup.itemName);
                    Debug.Log($"InventoryComponent: Picked up item {currentItem.Name}");
                    Object.Destroy(collider.gameObject);
                    return;
                }
                else
                {
                    Debug.LogWarning($"InventoryComponent: Object has Pickup tag but no PItem component: {collider.name}");

                    // Автоматически добавляем компонент PItem
                    pickup = collider.gameObject.AddComponent<PItem>();
                    pickup.itemName = collider.name;
                    Debug.Log($"InventoryComponent: Added PItem component to {collider.name}");

                    currentItem = new Item(pickup.itemName);
                    Debug.Log($"InventoryComponent: Picked up item {currentItem.Name} after adding PItem");
                    Object.Destroy(collider.gameObject);
                    return;
                }
            }
        }

        Debug.Log("InventoryComponent: No pickup found in range");
    }

    public void DropItem()
    {
        Debug.Log("InventoryComponent: DropItem called, isActive=" + isActive);

        if (!isActive)
        {
            Debug.LogWarning("InventoryComponent: Cannot drop, inventory not active! Activating now...");
            ActivateInventory();
        }

        if (currentItem == null)
        {
            Debug.Log("InventoryComponent: No item to drop");
            return;
        }

        Debug.Log($"InventoryComponent: Attempting to drop item: {currentItem.Name}");

        // Вывод информации о всех разрешенных префабах для отладки
        Debug.Log($"InventoryComponent: Checking allowed prefabs ({allowedPrefabs.Count} registered):");
        foreach (var kvp in allowedPrefabs)
        {
            Debug.Log($"  - Item: {kvp.Key}, Prefab: {(kvp.Value != null ? kvp.Value.name : "NULL")}");
        }

        // Пытаемся найти префаб для текущего предмета
        if (allowedPrefabs.TryGetValue(currentItem.Name, out var prefab))
        {
            Vector3 dropPosition = character.CharacterTransform.position +
                                  character.CharacterTransform.forward +
                                  new Vector3(0, 0.5f, 0);

            Debug.Log($"InventoryComponent: Creating prefab for {currentItem.Name} at {dropPosition}");
            GameObject obj = Object.Instantiate(prefab, dropPosition, Quaternion.identity);

            // Убедимся, что у выброшенного предмета есть компонент PItem и тег Pickup
            if (!obj.CompareTag("Pickup"))
            {
                obj.tag = "Pickup";
                Debug.Log("InventoryComponent: Added Pickup tag to dropped object");
            }

            PItem pItem = obj.GetComponent<PItem>();
            if (pItem == null)
            {
                pItem = obj.AddComponent<PItem>();
                Debug.Log("InventoryComponent: Added missing PItem component to dropped object");
            }

            pItem.itemName = currentItem.Name;

            // Очищаем текущий предмет
            Item droppedItem = currentItem;
            currentItem = null;
            Debug.Log($"InventoryComponent: Successfully dropped item {droppedItem.Name}");
        }
        else
        {
            Debug.LogError($"InventoryComponent: No prefab found for item {currentItem.Name}! Creating default cube...");

            // Создаем простой куб, если префаб не найден
            Vector3 dropPosition = character.CharacterTransform.position +
                                  character.CharacterTransform.forward +
                                  new Vector3(0, 0.5f, 0);

            GameObject defaultCube = GameObject.CreatePrimitive(PrimitiveType.Cube);
            defaultCube.transform.position = dropPosition;
            defaultCube.tag = "Pickup";

            PItem pItem = defaultCube.AddComponent<PItem>();
            pItem.itemName = currentItem.Name;

            Debug.Log($"InventoryComponent: Created default cube for {currentItem.Name}");

            // В любом случае очищаем текущий предмет
            Item droppedItem = currentItem;
            currentItem = null;
            Debug.Log($"InventoryComponent: Dropped item {droppedItem.Name} as default cube");
        }

        // Финальная проверка, что предмет действительно сброшен
        if (currentItem != null)
        {
            Debug.LogError("InventoryComponent: Failed to reset currentItem! Forcing reset...");
            currentItem = null;
        }
    }
}