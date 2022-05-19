using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class Inventory 
{
    [SerializeField] List<Item> itemList = new List<Item>();
    [SerializeField] Dictionary<ItemType, List<Item>> indexedItemList = new Dictionary<ItemType, List<Item>>();
    public List<Item> Items => itemList;
    [SerializeField] int maxAmount;

    public Inventory(int maxAmount) {
        this.maxAmount = maxAmount;

        foreach (ItemType i in Enum.GetValues(typeof(ItemType)))
        {
            indexedItemList.Add(i, new List<Item>());
        }
    }

    public bool AddItem(Item item) {
        if(itemList.Count >= maxAmount ) return false;

        itemList.Add(item);
        indexedItemList[item.Type].Add(item);
        //EventBroker.Instance.CallInventoryChange();
        return true;

    }

    public bool RemoveItem(Item item) {
        itemList.Remove(item);
        indexedItemList[item.Type].Remove(item);
        //EventBroker.Instance.CallInventoryChange();
        return true;
    }
}
