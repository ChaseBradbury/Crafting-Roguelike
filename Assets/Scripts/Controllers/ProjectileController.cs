using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class ProjectileController : MonoBehaviour
{
    private Vector2 startPosition;
    private Vector2 targetPosition;
    private float targetRadius;
    private int ringIndex;
    private float pathLerpPosition = 0;
    private Dictionary<string, EnemyController> enemyDictionary;
    private float attackStrength;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (!PlayerManager.IsPaused())
        {
            pathLerpPosition += PlayerManager.Weapon.BaseFragment.fragmentOptions.projectileSpeed;
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
    }

    public void Shoot(int ringIndex, Vector2 start, Vector2 target, float radius, float attackStrength)
    {
        startPosition = start;
        targetPosition = target;
        targetRadius = radius;
        this.ringIndex = ringIndex;
        enemyDictionary = new Dictionary<string, EnemyController>();
        this.attackStrength = attackStrength;
        AudioManager.PlayAttack(PlayerManager.Weapon.BaseFragment.sound, attackStrength);
        transform.Find("Icon").GetComponent<SpriteRenderer>().sprite = PlayerManager.Weapon.BaseFragment.imbuement.icon;
    }

    public void RunBy()
    {
        Collider2D[] enemies = Physics2D.OverlapCircleAll(transform.position, targetRadius);
        foreach (Collider2D enemy in enemies)
        {
            EnemyController enemyController = enemy.GetComponent<EnemyController>();
            if (enemyController != null)
            {
                AddEffectToEnemy(enemyController, PlayerManager.Weapon.BaseFragment);
            }
        }
    }

    public void Impact()
    {
        Collider2D[] enemies = Physics2D.OverlapCircleAll(targetPosition, targetRadius);
        foreach (Collider2D enemy in enemies)
        {
            EnemyController enemyController = enemy.GetComponent<EnemyController>();
            if (enemyController != null)
            {
                AddEffectToEnemy(enemyController, PlayerManager.Weapon.RingFragments[ringIndex]);
            }
        }
        AudioManager.PlayAttack(PlayerManager.Weapon.RingFragments[ringIndex].sound, attackStrength);
        Destroy(gameObject);
    }

    public void AddEffectToEnemy(EnemyController enemy, FragmentSO fragment)
    {
        if (!enemyDictionary.ContainsKey(enemy.Id + fragment.itemCode))
        {
            enemy.AddEffect(fragment.imbuement.combatEffect, attackStrength, targetPosition);
            enemyDictionary.Add(enemy.Id + fragment.itemCode, enemy);
        }
    }
}
