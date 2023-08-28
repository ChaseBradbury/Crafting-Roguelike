using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    [SerializeField] private RoomSO testRoom;
    [SerializeField] private float quadrantWidth;
    [SerializeField] private float quadrantHeight;
    [SerializeField] private Transform enemyTemplate;
    [SerializeField] private RewardsController rewardsController;
    private int enemiesLeft = 0;

    public void Start()
    {
        int id = 0;
        foreach (EnemyOption enemyOption in PlayerManager.CurrentRoom.enemies)
        {
            for (int i = 0; i < enemyOption.GetNumber(PlayerManager.CurrentRoom); i++)
            {
                SpawnEnemy(enemyOption.enemy, id++);
            }
        }
    }

    public void SpawnEnemy(EnemySO enemy, int index)
    {
        float posX = Random.Range(-quadrantWidth, quadrantWidth);
        float posY = Random.Range(-quadrantHeight, quadrantHeight);
        Transform enemyTransform = Instantiate(enemyTemplate, transform).GetComponent<Transform>();
        enemyTransform.GetComponent<EnemyController>().Initialize(enemy, new Vector2(posX, posY), index, this);
        enemyTransform.gameObject.SetActive(true);
        ++enemiesLeft;
    }

    public void EnemyKilled(int id)
    {
        --enemiesLeft;
        if (enemiesLeft <= 0)
        {
            PlayerManager.levelOver = true;
            rewardsController.OpenRewardsScreen();
        }
    }
}
