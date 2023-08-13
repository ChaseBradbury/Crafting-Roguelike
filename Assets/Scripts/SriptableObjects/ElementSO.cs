using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObjects/Element")]
public class ElementSO : ScriptableObject
{
    public string elementCode;
    public string elementDisplayName;
    public Sprite icon;
}
