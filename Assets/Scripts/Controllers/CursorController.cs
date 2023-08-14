using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CursorController : MonoBehaviour
{
    void Awake()
    {
        FollowMouse();
    }

    void Update()
    {
        FollowMouse();
    }

    public void FollowMouse()
    {
        transform.position = Input.mousePosition;
    }
}
