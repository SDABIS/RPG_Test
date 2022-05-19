using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CreateAssetMenu(fileName = "Zone", menuName = "Game/Zone")]
public class Zone : ScriptableObject
{
    [SerializeField] SceneAsset scene;
    [SerializeField] List<EnemyParty> enemyParties = new List<EnemyParty>();

    public SceneAsset Scene => scene;
    public List<EnemyParty> EnemyParties => enemyParties;

    public EnemyParty GetRandomEnemyParty() {
        //Debug.Log(enemyParties.Count);
        return enemyParties[Random.Range(0, enemyParties.Count)];
    }
}
