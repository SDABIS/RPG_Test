using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Battle Action", menuName = "Actions/Attack Action")]
public class AttackAction : TargetChooseAction
{
    public enum DamageType {
        FIRE, ICE, THUNDER, PHYSICAL
    }
    [SerializeField] float damageMultiplier;
    [SerializeField] DamageType damageType;

    public override void Execute(BattleCharacter actor, BattleCharacter target)
    {
        float damage = actor.Stats.Strength * damageMultiplier;

        target.ReceiveDamage(damage, damageType);
    }
}
