using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class TargetChooseAction : BattleAction
{
    public enum TargetType {
        ENEMY, PLAYER, ALL
    }

    public TargetType targetType;
}
