using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class Item : MonoBehaviour
{
    private ItemPickup itemDefinition;

    public ItemPickup ItemDefinition {
        get => itemDefinition;

        set {
            itemDefinition = value;
        }
    }

    public ItemType Type => itemDefinition.type;

}
