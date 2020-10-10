using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPath : MonoBehaviour
{
    WavaConfig WaveConfig;
    List<Transform> Waypoints;
    
    int wayPointIndex = 0;

    // Start is called before the first frame update
    void Start()
    {
        Waypoints = WaveConfig.GetWayPoints();
        transform.position = Waypoints[wayPointIndex].transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        Move();
    }

    public void setWaveConfig(WavaConfig waveConfig)
    {
        this.WaveConfig = waveConfig;
    }

    private void Move()
    {
        if (wayPointIndex <= Waypoints.Count - 1)
        {
            var tatgetPosition = Waypoints[wayPointIndex].transform.position;
            var movementThisFrame = Time.deltaTime * WaveConfig.getMoveSpeed();
            transform.position = Vector2.MoveTowards(transform.position, tatgetPosition, movementThisFrame);
            if (tatgetPosition == transform.position)
            {
                wayPointIndex++;
            }
        }
        else
        {
            Destroy(gameObject);
        }
    }
}
