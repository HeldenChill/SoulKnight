using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Utilities;

public class PlayerGUIControl : MonoBehaviour
{
    [SerializeField]protected BarControl hpBar;
    [SerializeField]protected BarControl shieldBar;
    [SerializeField]protected BarControl manaBar;
    [SerializeField]
    Text enemyNumber;

    private void Awake()
    {
        Dispatcher.Inst.RegisterListenerEvent(EVENT_ID.ENEMY_COUNT_CHANGE, OnEnemyCountChange);
    }
    public Text moneyText;

    public BarControl HpBar{
        get{return hpBar;}
    }
    public BarControl ShieldBar{
        get{return shieldBar;}
    }
    public BarControl ManaBar{
        get{return manaBar;}
    }

    public void OnEnemyCountChange(object value)
    {
        int count = (int)value;
        enemyNumber.text = $"Enemy:{count}";
    }

    private void OnDestroy()
    {
        Dispatcher.Inst.UnregisterListenerEvent(EVENT_ID.ENEMY_COUNT_CHANGE, OnEnemyCountChange);
    }
}
