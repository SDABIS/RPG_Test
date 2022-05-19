using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Enemy Info", menuName = "Game/Enemy Info")]
public class EnemyInfo : CharacterInfo
{
    [SerializeField] int moneyReward;    
    public int MoneyReward => moneyReward;

    [SerializeField] int expReward;    
    public int ExpReward => expReward;

    [SerializeField] List<ItemPickup> itemReward;
    public List<ItemPickup> ItemReward => itemReward;
}
