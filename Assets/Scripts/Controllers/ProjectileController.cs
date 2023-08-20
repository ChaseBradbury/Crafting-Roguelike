using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class ProjectileController : MonoBehaviour
{
    private Vector2 startPosition;
    private Vector2 targetPosition;
    private float targetRadius;
    private WeaponMode weapon;
    private float pathLerpPosition = 0;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        pathLerpPosition += weapon.BaseFragment.projectileSpeed;
        if (pathLerpPosition >= 1)
        {
            Impact();
        }
        else
        {
            transform.position = Vector2.Lerp(startPosition, targetPosition, pathLerpPosition);
            RunBy();
        }
    }

    public void Shoot(WeaponMode weaponMode, Vector2 start, Vector2 target, float radius)
    {
        startPosition = start;
        targetPosition = target;
        targetRadius = radius;
        weapon = weaponMode;
    }

    public void RunBy()
    {
        Collider2D[] enemies = Physics2D.OverlapCircleAll(transform.position, weapon.BaseFragment.projectileSize);
        foreach (Collider2D enemy in enemies)
        {
            enemy.transform.Find("Sprite").GetComponent<SpriteRenderer>().color = new Color(0, 1, 0);
        }
    }

    public void Impact()
    {
        Collider2D[] enemies = Physics2D.OverlapCircleAll(targetPosition, targetRadius);
        foreach (Collider2D enemy in enemies)
        {
            enemy.transform.Find("Sprite").GetComponent<SpriteRenderer>().color = new Color(1, 0, 0);
        }
        Destroy(this);
    }
}
