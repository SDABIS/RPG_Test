using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleEnemy : BattleCharacter
{
    public override void ExecuteTurn()
    {
        ExecuteAction(Actions[0], BattleManager.Instance.Players[Random.Range(0, BattleManager.Instance.Players.Count)]);
    }
}
