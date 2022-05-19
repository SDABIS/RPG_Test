using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class StatModifier 
{
    public enum ModType {
        Flat,
        PercentAdd,
        PercentMult,
    }

    public float value;
    public ModType type;
    public CharacterStats.StatType statType;
    public object source;
    public readonly int order;

    public StatModifier() {}

    public StatModifier(float value, ModType type, object source, int order) {
        this.value = value;
        this.type = type;
        this.source = source;
        this.order = order;
    }

    public StatModifier(float value, ModType type, object source) : this(value, type, source, (int)type) {}
    public StatModifier(float value, ModType type) : this(value, type, (int)type) {}
}
