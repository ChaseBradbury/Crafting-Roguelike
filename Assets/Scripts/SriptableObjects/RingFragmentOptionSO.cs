using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObjects/FragmentOptions/Ring Fragment Options")]
public class RingFragmentOptionSO : ScriptableObject
{
    public float targetMinSize = 0.5f;
    public float targetMaxSize = 3.0f;
    public float targetGrowthRate = 0.01f;
    public float targetGrowthAcceleration = 1.1f;
}
