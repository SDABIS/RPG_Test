using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(fileName = "Battle Action", menuName = "Actions/Battle Action")]
public abstract class BattleAction : ScriptableObject
{

    [SerializeField] string _actionName;
    public string ActionName => _actionName;

    public abstract void Execute(BattleCharacter actor, BattleCharacter target);

}
