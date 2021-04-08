using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IItem 
{
    void setActiveContact(bool isActive);
    void getItem();
    int getValue();
}
