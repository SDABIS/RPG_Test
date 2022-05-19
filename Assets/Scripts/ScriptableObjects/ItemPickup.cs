using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ItemType { WEAPON, HELMET, CHEST, BOOTS }

[CreateAssetMenu(fileName = "Item", menuName = "Game/Item")]
public class ItemPickup : ScriptableObject
{

    public List<StatModifier> modifiers;
    public ItemType type;
    public Sprite sprite;
}
