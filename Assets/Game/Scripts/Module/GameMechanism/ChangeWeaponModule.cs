using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeWeaponModule : MonoBehaviour
{
    public void ChangeWeapon(GameObject weaponOfPlayer){
            Transform thisParent = transform.parent;
            Vector3 thisPosition = transform.position;
            //gameobject = M4A1
            gameObject.transform.parent = weaponOfPlayer.transform.parent;
            gameObject.transform.parent.GetComponent<ICharacterBase>().ChangeWeapon(gameObject);
            gameObject.transform.localPosition = weaponOfPlayer.transform.localPosition;
            gameObject.GetComponent<Weapon>().contactPlayer.Active = false;
            gameObject.GetComponent<Weapon>().SetLayer();
            //weaponOfPlayer = AK47
            weaponOfPlayer.transform.parent = thisParent;
            weaponOfPlayer.transform.position = thisPosition;
            weaponOfPlayer.GetComponent<Weapon>().contactPlayer.Active = true;
    }
}
