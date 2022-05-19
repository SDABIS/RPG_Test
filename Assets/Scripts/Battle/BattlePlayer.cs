using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattlePlayer : BattleCharacter
{
    
    protected BattleState currentState;
    public BattleAction SelectedAction { get => currentState.SelectedAction; set { currentState.SelectedAction = value; }}

    public override void ExecuteTurn()
    {
        currentState = new ChooseActionState(gameObject, this, null, null);
    }

    private void Update() {
        if(currentState != null) currentState = currentState.Process();
        
    }

}
