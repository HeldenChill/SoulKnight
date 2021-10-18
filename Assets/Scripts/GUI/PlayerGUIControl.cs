using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerGUIControl : MonoBehaviour
{
    [SerializeField]protected BarControl hpBar;
    [SerializeField]protected BarControl shieldBar;
    [SerializeField]protected BarControl manaBar;
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
}
