using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Enemy Party", menuName = "Game/Enemy Party")]
public class EnemyParty : ScriptableObject
{
    [SerializeField] List<EnemyInfo> enemies;
    public List<EnemyInfo> Enemies => enemies;

    public int CalculateEXPreward() {
        int exp = 0;
        foreach (EnemyInfo enemy in enemies)
        {
            exp += enemy.ExpReward;
        }

        return exp;
    }
}
