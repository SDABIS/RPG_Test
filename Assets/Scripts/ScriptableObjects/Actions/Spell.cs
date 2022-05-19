using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Spell", menuName = "Actions/Spell")]
public class Spell : AttackAction
{
    [SerializeField] float manaCost;
    public float ManaCost => manaCost;

    public override void Execute(BattleCharacter actor, BattleCharacter target)
    {
        base.Execute(actor, target);

        actor.ReduceMana(manaCost);
    }
}
