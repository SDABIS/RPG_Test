using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public abstract class BattleCharacter : MonoBehaviour
{
    protected CharacterStats stats;
    public CharacterStats Stats {
        get => stats;

        set { stats = value; }
    }

    protected List<BattleAction> _actions = new List<BattleAction>();
    public List<BattleAction> Actions => _actions;
    [SerializeField] TempText tempText;
    public UnityEvent OnTurnEnd; 

    private float speedCounter = 0;
    public float SpeedCounter => speedCounter;

    public abstract void ExecuteTurn();

    public void IncreaseSpeedCounter() {
        speedCounter += stats.Speed;
    }
    public void DecreaseSpeedCounter(float amount) {
        speedCounter -= amount;
    }

    public virtual void AddAction(BattleAction action) {
        _actions.Add(action);
    }

    public void ExecuteAction(BattleAction action, BattleCharacter target) {
        action.Execute(this, target);

        OnTurnEnd.Invoke();
    }

    public void ReceiveDamage(float amount, AttackAction.DamageType damageType) {
        float realDamage = stats.CalculateDamage(amount);
        tempText.Activate(realDamage.ToString());

        if(Stats.CurrentHp <= 0) {
            EventBroker.Instance.CallCharacterDeath(this);
        }
    }

    public void ReduceMana(float amount) {
        Stats.CurrentMana -= amount;
    }

}
