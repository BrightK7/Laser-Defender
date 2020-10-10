using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(menuName = "Enemy Wave Config")]
public class WavaConfig : ScriptableObject
{
    [SerializeField] GameObject enemyPrefab;
    [SerializeField] GameObject pathPrefab;
    [SerializeField] float timeBetweenSpawn = 0.5f;
    [SerializeField] float spawnRandomFactor = 0.3f;
    [SerializeField] int numberOfEnemy = 5;
    [SerializeField] float moveSpeed = 2f;

    public GameObject getEnemyPrefab() { return enemyPrefab; }
    public List<Transform> GetWayPoints()
    {
        List<Transform> wayPoints = new List<Transform>();
        foreach(Transform  child in pathPrefab.transform)
        {
            wayPoints.Add(child);
        }
        return wayPoints;
    }
    public float getTimeBetweenSpawn() { return timeBetweenSpawn;}
    public float getNumberOfEnemy() { return numberOfEnemy; }
    public float getMoveSpeed() { return moveSpeed; }

}
