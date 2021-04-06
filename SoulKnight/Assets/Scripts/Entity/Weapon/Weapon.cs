using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Weapon : MonoBehaviour
{
    // Start is called before the first frame update
    public abstract void attack(Vector2 target);
    public abstract void mechanism(Vector2 target);
}
