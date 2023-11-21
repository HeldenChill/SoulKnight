using Game;
using Project;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Utilities;

public class ChasingEnemy : EnemyBase, IPoolUnit
{
    [SerializeField]
    MovingAgent movingAgent;
    protected override void Awake()
    {
        base.Awake();
        movingAgent.Speed = speed;
        
    }
    private void ChasingPlayer(object position)
    {
        if(position == null) return;
        Vector3 pos = (Vector3)position;
        movingAgent.SetDestination(pos);
        lookAtModule.LookAt(pos);
    }
    public void OnInit()
    {
        Dispatcher.Inst.RegisterListenerEvent(EVENT_ID.PLAYER_GRID_POS_UPDATE, ChasingPlayer);
        ChasingPlayer(Dispatcher.Inst.GetLastParamEvent(EVENT_ID.PLAYER_GRID_POS_UPDATE));
    }

    public void OnDespawn()
    {
        Dispatcher.Inst.UnregisterListenerEvent(EVENT_ID.PLAYER_GRID_POS_UPDATE, ChasingPlayer);
    }

    protected override void Die()
    {
        LevelManager.Inst.PushEnemy(this);
    }
}