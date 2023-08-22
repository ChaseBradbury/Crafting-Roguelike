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

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        pathLerpPosition += PlayerManager.Weapon.BaseFragment.projectileSpeed;
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

    public void Shoot(int ringIndex, Vector2 start, Vector2 target, float radius)
    {
        startPosition = start;
        targetPosition = target;
        targetRadius = radius;
        this.ringIndex = ringIndex;
        enemyDictionary = new Dictionary<string, EnemyController>();
    }

    public void RunBy()
    {
        Collider2D[] enemies = Physics2D.OverlapCircleAll(transform.position, PlayerManager.Weapon.BaseFragment.projectileSize);
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
                AddEffectToEnemy(enemyController, PlayerManager.Weapon.CenterFragment);
            }
        }
        Destroy(gameObject);
    }

    public void AddEffectToEnemy(EnemyController enemy, FragmentSO fragment)
    {
        if (!enemyDictionary.ContainsKey(enemy.Id + fragment.itemCode))
        {
            enemy.AddEffect(fragment.combatEffect);
            enemyDictionary.Add(enemy.Id+ fragment.itemCode, enemy);
        }
    }
}
