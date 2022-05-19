using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using System;

[System.Serializable]
public class CharacterStats 
{
    public enum StatType { HP, MANA, STRENGTH, DEFENSE, SPEED }

    [System.Serializable]
    public class StatList {
        public Stat maxHp; 
        public Stat maxMana; 
        public Stat strength; 
        public Stat defense; 
        public Stat speed; 

    }
    [SerializeField] CharacterInfo _characterInfo;
    public CharacterInfo CharacterInfo => _characterInfo;
    
    [SerializeField] StatList statList;
    [SerializeField] float _currentHp;
    [SerializeField] float _currentMana;

    public UnityEvent OnHpManaChange = new UnityEvent();

    #region Accessors

    public float MaxHP {
        get { return statList.maxHp.Value; } private set { statList.maxHp.SetBase(value); }
    }
    public float MaxMana {
        get { return statList.maxMana.Value; } private set { statList.maxMana.SetBase(value); }
    }
    public float Strength {
        get { return statList.strength.Value; } private set { statList.strength.SetBase(value); }
    }
    public float Defense {
        get { return statList.defense.Value; } private set { statList.defense.SetBase(value); }
    }
    public float Speed {
        get { return statList.speed.Value; } private set { statList.speed.SetBase(value); }
    }

    public float CurrentHp {
        get => _currentHp; set { _currentHp = value; OnHpManaChange.Invoke(); }
    }

    public float CurrentMana {
        get => _currentMana; set { _currentMana = value; OnHpManaChange.Invoke(); }
    }
    

    #endregion

    [SerializeField] int _level = 1;
    [SerializeField] int _expToNextLevel;
    [SerializeField] int _exp = 0;
    [SerializeField] bool _hasLeveledUp = false;

    #region Level

    public int Exp {
        get {
            return _exp;
        }

        set {
            _exp = value;
            
            if(_exp >= _expToNextLevel) {
                _exp -= _expToNextLevel;
                LevelUp();
            }
            else _hasLeveledUp = false;
        }
    }

    private void LevelUp() {
        _level++;
        _hasLeveledUp = true;
        _expToNextLevel = _characterInfo.GetExpForNextLevel(_level);

        MaxHP = _characterInfo.GetHpForLevel(_level);
        MaxMana = _characterInfo.GetManaForLevel(_level);
        Strength = _characterInfo.GetStrengthForLevel(_level);
        Defense = _characterInfo.GetDefenseForLevel(_level);
        Speed = _characterInfo.GetSpeedForLevel(_level);

        //EventBroker.Instance.CallStatChange();
    }


    public bool HasLeveledUp => _hasLeveledUp;

    public int Level => _level;
    #endregion

    private Dictionary<ItemType, Item> currentEquip = new Dictionary<ItemType, Item>();
    public Dictionary<ItemType, Item> CurrentEquip => currentEquip;

    public CharacterStats(CharacterInfo initialStats) {
        this._characterInfo = initialStats;

        _level = _characterInfo.initialLevel;
    
        _expToNextLevel = _characterInfo.GetExpForNextLevel(_level);

        statList = new StatList();
        statList.maxHp = new Stat(_characterInfo.GetHpForLevel(_level));
        statList.maxMana = new Stat(_characterInfo.GetManaForLevel(_level));
        statList.strength = new Stat(_characterInfo.GetStrengthForLevel(_level));
        statList.defense = new Stat(_characterInfo.GetDefenseForLevel(_level));
        statList.speed = new Stat(_characterInfo.GetSpeedForLevel(_level));

        CurrentHp = MaxHP;
        CurrentMana = MaxMana;

        foreach (ItemType i in Enum.GetValues(typeof(ItemType)))
        {
            currentEquip.Add(i, null);
        }
    }

    public float CalculateDamage(float damage) {
        float realDamage = Mathf.Clamp(damage - Defense, 0, Mathf.Infinity);
        CurrentHp -= realDamage;
        return realDamage;
    }

    public void EquipItem(Item item) {
        ItemPickup itemDef = item.ItemDefinition;

        Item current = currentEquip[itemDef.type];

        if(current != null) UnequipItem(current);

        foreach (StatModifier mod in itemDef.modifiers)
        {
            mod.source = item;

            switch(mod.statType) {
                case StatType.HP:
                    statList.maxHp.AddModifier(mod);

                break;
                case StatType.MANA:
                    statList.maxMana.AddModifier(mod);

                break;
                case StatType.STRENGTH: 
                    statList.strength.AddModifier(mod);
                break;

                case StatType.DEFENSE:
                    statList.defense.AddModifier(mod);

                break;
                case StatType.SPEED:
                    statList.speed.AddModifier(mod);

                break;
                default: break;
            }
        }

        currentEquip[itemDef.type] = item;

        //EventBroker.Instance.CallEquipChange();
    }

    public void UnequipItem(Item item) {
        currentEquip[item.ItemDefinition.type] = null;
        
        statList.maxHp.RemoveAllModifiersFromSource(item);
        statList.maxMana.RemoveAllModifiersFromSource(item);
        statList.strength.RemoveAllModifiersFromSource(item);
        statList.defense.RemoveAllModifiersFromSource(item);
        statList.speed.RemoveAllModifiersFromSource(item);
    }
}
