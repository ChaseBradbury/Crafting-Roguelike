using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CraftingSetupDevelop : MonoBehaviour
{
    [SerializeField] public LoadManager loadmanager;

    public void Start()
    {
        loadmanager.LoadNew();
    }
}
