using UnityEngine;
using UnityEngine.UI;

public class DirectInventoryControl : MonoBehaviour
{
    [SerializeField] private Button useButton;
    [SerializeField] private Button throwButton;

    void Start()
    {
        if (useButton != null)
        {
            useButton.onClick.AddListener(OnUseButtonClicked);
        }

        if (throwButton != null)
        {
            throwButton.onClick.AddListener(OnThrowButtonClicked);
        }

        // ��������� ��������� �������� ����� 1 ������� ����� �������
        Invoke("CheckAllowedItems", 1f);
    }

    void OnUseButtonClicked()
    {
        Debug.Log("DirectInventoryControl: Use button clicked");

        if (GameManager.Instance != null && GameManager.Instance.Player != null)
        {
            Character player = GameManager.Instance.Player;

            if (player.InventoryComponent != null)
            {
                // ���������� ��������� � ������� ��������� �������
                player.InventoryComponent.ActivateInventory();
                player.InventoryComponent.PickupItem();
            }
            else
            {
                Debug.LogError("DirectInventoryControl: Player InventoryComponent is null");
            }
        }
        else
        {
            Debug.LogError("DirectInventoryControl: GameManager.Instance or Player is null");
        }
    }

    void OnThrowButtonClicked()
    {
        Debug.Log("DirectInventoryControl: Throw button clicked");

        if (GameManager.Instance != null && GameManager.Instance.Player != null)
        {
            Character player = GameManager.Instance.Player;

            if (player.InventoryComponent != null)
            {
                // ���������, ���� �� ������� ����� �������������
                bool hasItem = player.InventoryComponent.HasItem;
                Debug.Log($"DirectInventoryControl: Player has item: {hasItem}");

                // ���������� ��������� � ������� ��������� �������
                player.InventoryComponent.ActivateInventory();

                // �������� DropItem � ��������� ��������� ��� �������
                Debug.Log("DirectInventoryControl: Attempting to drop item");
                player.InventoryComponent.DropItem();

                // ���������, ��������� �� ������ HasItem ����� ������������
                bool hasItemAfterDrop = player.InventoryComponent.HasItem;
                Debug.Log($"DirectInventoryControl: Player has item after drop: {hasItemAfterDrop}");

                // ���� ������� �� ��� �� ��������, �������� ��������� allowedItems
                if (hasItem && hasItemAfterDrop)
                {
                    Debug.LogWarning("DirectInventoryControl: Item was not dropped, possible issue with prefabs");
                }
            }
        }
    }

    // �������� ���� ����� � DirectInventoryControl
    public void CheckAllowedItems()
    {
        Debug.Log("=== Checking Allowed Items ===");

        if (GameManager.Instance == null || GameManager.Instance.Player == null)
        {
            Debug.LogError("GameManager.Instance or Player is null");
            return;
        }

        Character player = GameManager.Instance.Player;

        if (player.InventoryComponent == null)
        {
            Debug.LogError("Player.InventoryComponent is null");
            return;
        }

        // ��� ����� ������ � ���������� ���� allowedItems � InventoryComponent
        // ��������� ��� ���������, ���������� ���������
        System.Reflection.FieldInfo allowedItemsField = typeof(InventoryComponent).GetField("allowedItems",
            System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance);

        if (allowedItemsField != null)
        {
            var allowedItems = allowedItemsField.GetValue(player.InventoryComponent) as AllowedItemPrefab[];

            if (allowedItems != null && allowedItems.Length > 0)
            {
                Debug.Log($"Found {allowedItems.Length} allowed items:");

                foreach (var item in allowedItems)
                {
                    if (item != null)
                    {
                        Debug.Log($"Item name: {item.itemName}, Prefab: {(item.prefab != null ? item.prefab.name : "NULL")}");
                    }
                    else
                    {
                        Debug.Log("Item is null");
                    }
                }
            }
            else
            {
                Debug.LogError("No allowed items configured!");
            }
        }
        else
        {
            Debug.LogError("Could not access allowedItems field");
        }

        // ����� �������� ������� �������
        if (player.InventoryComponent.HasItem && player.InventoryComponent.CurrentItem != null)
        {
            Debug.Log($"Current item: {player.InventoryComponent.CurrentItem.Name}");
        }
        else
        {
            Debug.Log("No current item");
        }
    }
}