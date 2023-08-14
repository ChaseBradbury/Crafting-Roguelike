using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObjects/Recipe")]
public class RecipeSO : ScriptableObject
{
    [Header("Input")]
    public ItemSO northSlot;
    public ItemSO eastSlot;
    public ItemSO southSlot;
    public ItemSO westSlot;
    [Tooltip("When checked, the player must place in the correct slots.")]
    public bool positionMatters;
    [Header("Output")]
    public ItemSO outputItem;
}
