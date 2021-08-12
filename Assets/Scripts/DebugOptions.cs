using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DebugOptions : MonoBehaviour {

    public List<GameObject> items;
    public TextMeshProUGUI inventorytext;

    InventoryHandler inventoryHandler;

    // Start is called before the first frame update
    void Start() {
        inventoryHandler = GameObject.Find("GameManager").GetComponent<InventoryHandler>();
    }

    public void AddOneItem(int index) {
        inventoryHandler.AddItemToInventory(items[index], 1);
        UpdateInventory();
    }

    public void RemoveOneItem(int index) {
        inventoryHandler.RemoveItemToInventory(items[index], 1);
        UpdateInventory();
    }

    public void UpdateInventory() {
        inventorytext.text = $"Inventory:\n{inventoryHandler.GetListItems()}";
    }
}
