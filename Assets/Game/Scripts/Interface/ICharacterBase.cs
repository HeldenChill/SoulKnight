using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ICharacterBase
{
    void GetDamage(int damage);
    int GetLayer();
    int GetLayerMask();
    
    void ChangeWeapon(GameObject weapon);
    
}
