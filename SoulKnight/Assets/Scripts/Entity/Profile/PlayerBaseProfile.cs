using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBaseProfile
{
    // Start is called before the first frame update
    [SerializeField]protected int speed = 5;
    protected int hp = 5;
    protected int shield = 5;
    protected int mana = 100;
    public int maxHp;
    public int maxShield;
    public int maxMana;

    public int Hp{
        get{return hp;}
        set{
            if(value <= 0){
                hp = 0;
            }
            else if(value <= maxHp){
                hp = value;
            }
            else{
                hp = maxHp;
            }
        }
    }

    public int Shield{
        get{return shield;}
        set{
            if(value < 0){
                Hp += value;
                shield = 0;
            }
            else if(value <= maxShield){
                shield = value;
            }
            else{
                shield = maxShield;
            }
        }
    }

    public int Mana{
        get{return mana;}
        set{
            if(value < 0){
                mana = 0;
            }
            else if(value <= maxMana){
                mana = value;
            }
            else{
                mana = maxMana;
            }
        }
    }

    public int Speed{
        get{return speed;}
        set{
            speed = value;
        }
    }

    public string getInfo(){
        string res = "";
        res += maxHp.ToString() + " " + maxMana.ToString() + " " + maxShield.ToString() 
            + " " + hp.ToString() + " " + mana.ToString() + " " + shield.ToString();

        return res;
    }

    public void setMaxAll(){
        Hp = maxHp;
        Mana = maxMana;
        Shield = maxShield;
    }
}
