﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ICharacterBase
{
    void getDamage(int damage);
    int getLayer();
    int getLayerMask();
    
    void changeWeapon(GameObject weapon);
    
}
