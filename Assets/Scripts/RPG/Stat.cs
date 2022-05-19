using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

[System.Serializable]
public class Stat
{
    [SerializeField] float baseValue;

    [SerializeField] List<StatModifier> modifiers = new List<StatModifier>();
    [SerializeField] bool isDirty = true;
    [SerializeField] float finalValue;
    
    public float Value {
        get {
            if(isDirty) {
                finalValue = CalculateValue();
                isDirty = false;
            }

            return finalValue;
        }
    }

    public Stat(float value) {
        baseValue = value;
    }

    public void SetBase(float amount) {
        isDirty = true;
        baseValue = amount;
    }

    private float CalculateValue() {
        float result = baseValue;
        float percentAdditive = 1;

        foreach (StatModifier mod in modifiers)
        {
            if(mod.type == StatModifier.ModType.Flat) {
                result += mod.value;
            }

            else if(mod.type == StatModifier.ModType.PercentAdd) {
                percentAdditive += mod.value;
            }

            else if(mod.type == StatModifier.ModType.PercentMult) {
                result *= 1 + mod.value;
            }
        }
        result *= percentAdditive;
        return result;
    }

    public void AddModifier(StatModifier mod) {
        isDirty = true;
        modifiers.Add(mod);
        modifiers.OrderBy(mod => mod.order).ToList();
    }

    public void RemoveModifier(StatModifier mod) {
        isDirty = true;
        modifiers.Remove(mod);
    }

    public void RemoveAllModifiersFromSource(object source) {
        for (int i = modifiers.Count - 1; i>= 0; i--) {
            if(modifiers[i].source == source) {
                RemoveModifier(modifiers[i]);
            }
        }
    }


}

