using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryHandler : MonoBehaviour {
    private List<InventoryItem> auxInventory = new List<InventoryItem>();


    private class InventoryItem
    {
        public GameObject item;
        public int amount;

        public InventoryItem(GameObject newItem, int count) {
            item = newItem;
            amount = count;
        }
    }

    public void AddItemToInventory(GameObject item, int amount) {
        int itemIndex = FindItem(item);
        if (itemIndex == -1) {
            auxInventory.Add(new InventoryItem(item, amount));
        } else {
            auxInventory[itemIndex].amount += amount;
        }
        
    }

    public void RemoveItemToInventory(GameObject item, int amount) {
        int itemIndex = FindItem(item);
        if (itemIndex != -1) {
            auxInventory[itemIndex].amount -= amount;

            if(auxInventory[itemIndex].amount < 1) {
                auxInventory.RemoveAt(itemIndex);
            }
        } 

    }


    private int FindItem(GameObject item) {
        int i = 0;
        int posItem = -1;
        foreach (InventoryItem inventoryItem in auxInventory) {
            if (inventoryItem.item.name == item.name) {
                posItem = i;
            }
            i++;
        }
        return posItem;
    }

    public string GetListItems() {
        string itemsToText = "";
        foreach (InventoryItem inventoryItem in auxInventory) {
            itemsToText += $"{inventoryItem.item.name}: {inventoryItem.amount}\n"; 
        }
        return itemsToText;
    }
}
