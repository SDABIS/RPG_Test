using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Character Database", menuName = "Game/Character Database")]
public class CharacterDatabase : ScriptableObject
{
    [SerializeField] List<CharacterInfo> characterInfos;
    public List<CharacterInfo> CharacterInfos => characterInfos;
}
