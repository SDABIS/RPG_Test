using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class SubMenuAction : BattleAction
{
    public abstract List<BattleAction> GetSubActions(BattleCharacter character);
}
