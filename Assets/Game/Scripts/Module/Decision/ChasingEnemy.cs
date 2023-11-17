using Project;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Utilities;

public class ChasingEnemy : EnemyBase
{
    [SerializeField]
    MovingAgent movingAgent;
    protected override void Awake()
    {
        base.Awake();
        movingAgent.Speed = speed;
        Dispatcher.Inst.RegisterListenerEvent(EVENT_ID.PLAYER_GRID_POS_UPDATE, ChasingPlayer);
    }
    private void ChasingPlayer(object position)
    {
        Vector3 pos = (Vector3)position;
        movingAgent.SetDestination(pos);
        lookAtModule.LookAt(pos);
    }

    protected void OnDestroy()
    {
        Dispatcher.Inst.UnregisterListenerEvent(EVENT_ID.PLAYER_GRID_POS_UPDATE, ChasingPlayer);
    }
}
