using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

[CreateAssetMenu(fileName = "Magic", menuName = "Actions/Magic Action")]
public class SpellSubMenu : SubMenuAction
{
    public override void Execute(BattleCharacter actor, BattleCharacter target)
    {
        
    }

    public override List<BattleAction> GetSubActions(BattleCharacter character)
    {
        return character.Stats.CharacterInfo.Spells.ToList<BattleAction>();
    }
}
