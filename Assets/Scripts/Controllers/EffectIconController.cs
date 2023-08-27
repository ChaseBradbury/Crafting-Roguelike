using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EffectIconController : MonoBehaviour
{
    private SpriteRenderer sprite;
    private bool decays;
    private float timeAlive;

    public bool Decays { get => decays; set => decays = value; }

    public void SetEffect(CombatEffectSO effect, bool decays)
    {
        sprite = transform.GetComponent<SpriteRenderer>();
        sprite.sprite = effect.sprite;
        this.decays = decays;
    }

    public void SetPosition(float x)
    {
        transform.localPosition = new Vector3(x, 0, 0);
    }

    public bool Decay(float time)
    {
        if (decays && timeAlive++ > time)
        {
            return true;
        }
        return false;
    }
}
