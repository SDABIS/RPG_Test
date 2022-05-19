using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class EventBroker : Singleton<EventBroker> {

    public UnityEvent OnBattleStart;
    public UnityEvent OnBattleEnd;
    public UnityEvent<BattleCharacter> OnCharacterDeath;

    public void CallBattleStart() {
        OnBattleStart.Invoke();
    }

    public void CallBattleEnd() {
        OnBattleEnd.Invoke();
    }

    public void CallCharacterDeath(BattleCharacter character) {
        OnCharacterDeath.Invoke(character);
    }
}