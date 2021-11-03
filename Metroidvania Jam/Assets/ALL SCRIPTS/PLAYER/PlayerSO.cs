using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;


[CreateAssetMenu(menuName = "ScriptableObjects/PlayerSO")]
public class PlayerSO: ScriptableObject
{
    public event UnityAction onAttackSide;
    public event UnityAction onAttackUp;
    public event UnityAction onAttackDown;

    public void onSideAttack()
    {
        if (onAttackSide != null)
        {
            onAttackSide();
        }
    }
    public void onUpAttack()
    {
        if (onAttackUp != null)
        {
            onAttackUp();
        }
    }
    public void onDownAttack()
    {
        if (onAttackDown != null)
        {
            onAttackDown();
        }
    }
}
