using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObjects/Fragment Display")]
public class FragmentDisplaySO : ScriptableObject
{
    [Tooltip("Combines with combat effect name.")]
    public string fragmentModularName;
    [Tooltip("Description on hover of what happens during attack.")]
    [TextArea(3,10)]
    public string description;
}
