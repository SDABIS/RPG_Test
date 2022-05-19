using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class BattleState
{
    public enum STATE
    {
        CHOOSE, SUBMENU, TARGET, PERFORM
    }

    public enum EVENT
    {
        ENTER, UPDATE, EXIT
    }
    public STATE name;
    protected EVENT stage;
    protected GameObject go;
    protected BattlePlayer player;
    protected Animator anim;
    protected BattleState nextState;

    protected BattleAction _selectedAction;
    public BattleAction SelectedAction { get => _selectedAction; set { _selectedAction = value; } }

    public BattleState(GameObject go, BattlePlayer player, Animator anim, BattleAction selectedAction)
    {
        this.go = go;
        this.anim = anim;
        stage = EVENT.ENTER;
        this.player = player;
        this._selectedAction = selectedAction;
    }

    public virtual void Enter() { stage = EVENT.UPDATE; }
    public virtual void Update() { stage = EVENT.UPDATE; }
    public virtual void Exit() { stage = EVENT.EXIT; }

    public BattleState Process()
    {
        switch (stage)
        {
            case EVENT.ENTER: Enter(); break;
            case EVENT.UPDATE: Update(); break;
            case EVENT.EXIT: Exit(); return nextState;
        }
        return this;
    }

    /*protected bool CanSeePlayer() {
        Vector3 direction = player.position - npc.transform.position;

        float angle = Vector3.Angle(npc.transform.forward, direction);

        return (angle < visAngle) && (direction.magnitude < visDistance);
    }

    protected bool CanAttackPlayer() {
        Vector3 direction = player.position - npc.transform.position;
        return direction.magnitude < shootDistance;
    }*/
}

public class ChooseActionState : BattleState
{
    public ChooseActionState(GameObject go, BattlePlayer player, Animator anim, BattleAction selectedAction)
                : base(go, player, anim, selectedAction)
    {
        name = STATE.CHOOSE;
    }

    public override void Enter()
    {
        base.Enter();
        _selectedAction = null;
        BattleManager.Instance.ShowPlayerActions(player);
    }

    public override void Update()
    {
        base.Update();

        if(_selectedAction != null) {
            if(_selectedAction is SubMenuAction) {
                nextState = new SubMenuState(go, player, anim, _selectedAction);
                stage = EVENT.EXIT;
            }
            else {
                nextState = new TargetState(go, player, anim, _selectedAction);
                stage = EVENT.EXIT;
            }
        }
    }

    public override void Exit()
    {
        base.Exit();
        BattleManager.Instance.HidePlayerActions();
    }
}

public class SubMenuState : BattleState
{
    public SubMenuState(GameObject go, BattlePlayer player, Animator anim, BattleAction selectedAction)
                : base(go, player, anim, selectedAction)
    {
        name = STATE.SUBMENU;
    }

    public override void Enter()
    {
        base.Enter();
        BattleManager.Instance.ShowPlayerSubActions(player, (SubMenuAction)_selectedAction);
        _selectedAction = null;
    }

    public override void Update()
    {
        base.Update();

        if(_selectedAction != null) {
            nextState = new TargetState(go, player, anim, _selectedAction);
            stage = EVENT.EXIT;
        }
    }

    public override void Exit()
    {
        base.Exit();
        BattleManager.Instance.HidePlayerSubActions();
    }
}
public class TargetState : BattleState
{
    private BattleCharacter target;
    public TargetState(GameObject go, BattlePlayer player, Animator anim, BattleAction selectedAction)
                : base(go, player, anim, selectedAction)
    {
        name = STATE.TARGET;
    }

    public override void Enter()
    {
        base.Enter();
        target = BattleManager.Instance.ChangeCurrentTarget(target, 1, ((TargetChooseAction)_selectedAction).targetType);
    }

    public override void Update()
    {
        base.Update();

        int selectChange = 0;
        if(Input.GetKeyDown(KeyCode.UpArrow)) selectChange = -1;
        else if(Input.GetKeyDown(KeyCode.DownArrow)) selectChange = 1;

        if(selectChange != 0) {
            target = BattleManager.Instance.ChangeCurrentTarget(target, selectChange, ((TargetChooseAction)_selectedAction).targetType);
        }

        if(Input.GetKeyDown(KeyCode.Escape)) {
            nextState = new ChooseActionState(go, player, anim, _selectedAction);
            stage = EVENT.EXIT;
        }
        else if(Input.GetKeyDown(KeyCode.Space)) {
            nextState = new PerformState(go, player, anim, _selectedAction, target);
            stage = EVENT.EXIT;
        }

    }

    public override void Exit()
    {
        base.Exit();
        BattleManager.Instance.EndTarget();
    }
}
public class PerformState : BattleState
{
    private BattleCharacter target;
    public PerformState(GameObject go, BattlePlayer player, Animator anim, BattleAction selectedAction, BattleCharacter target)
                : base(go, player, anim, selectedAction)
    {
        name = STATE.PERFORM;
        this.target = target;
    }

    public override void Enter()
    {
        base.Enter();
        player.ExecuteAction(SelectedAction, target);
    }

    public override void Update()
    {
        base.Update();

    }

    public override void Exit()
    {
        base.Exit();
    }
}
