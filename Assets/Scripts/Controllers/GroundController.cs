using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundController : MonoBehaviour
{
    void Start()
    {
        SpriteRenderer sprite = transform.GetComponent<SpriteRenderer>();
        sprite.color = PlayerManager.CurrentRoom.tileColor;
    }
}
