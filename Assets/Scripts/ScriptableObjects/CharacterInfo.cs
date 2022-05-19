using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Character Info", menuName = "Game/Character Info")]
public class CharacterInfo : ScriptableObject
{
    [System.Serializable]
    public class CharLevelUps
    {
        public int expToNext;
        public int maxHealth;
        public int maxMana;
        public float strength;
        public float defense;
        public float speed;
    }
    //[SerializeField] int maxLevel = 100;

    [SerializeField] List<CharLevelUps> levelUps;
    [SerializeField] BattleCharacter _battleCharacter;
    public BattleCharacter BattleCharacter => _battleCharacter;
    
    [SerializeField] List<BattleAction> _actions;
    public List<BattleAction> Actions => _actions;

    [SerializeField] List<Spell> _spells;
    public List<Spell> Spells => _spells;

    public int initialLevel;

    public int GetExpForNextLevel(int level) => levelUps[level - 1].maxHealth;
    public int GetHpForLevel(int level) => levelUps[level - 1].maxHealth;
    public int GetManaForLevel(int level) => levelUps[level - 1].maxMana;
    public float GetStrengthForLevel(int level) => levelUps[level - 1].strength;
    public float GetDefenseForLevel(int level) => levelUps[level - 1].defense;
    public float GetSpeedForLevel(int level) => levelUps[level - 1].speed;
}
