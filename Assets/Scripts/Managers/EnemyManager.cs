using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    [SerializeField] private RoomSO testRoom;
    [SerializeField] private float quadrantWidth;
    [SerializeField] private float quadrantHeight;
    [SerializeField] private Transform enemyTemplate;

    public void Start()
    {
        if (testRoom != null)
        {
            for (int i = 0; i < testRoom.numberOfEnemies; i++)
            {
                SpawnEnemy(testRoom.enemies[0].enemy);
            }
        }
    }

    public void SpawnEnemy(EnemySO enemy)
    {
        float posX = Random.Range(-quadrantWidth, quadrantWidth);
        float posY = Random.Range(-quadrantHeight, quadrantHeight);
        Transform enemyTransform = Instantiate(enemyTemplate, transform).GetComponent<Transform>();
        enemyTransform.GetComponent<EnemyController>().Initialize(enemy, new Vector2(posX, posY));
        enemyTransform.gameObject.SetActive(true);
    }
}
