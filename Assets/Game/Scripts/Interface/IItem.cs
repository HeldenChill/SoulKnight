using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IItem 
{
    void SetActiveContact(bool isActive);
    void GetItem();
    int GetValue();
}
