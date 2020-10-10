using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpwaner : MonoBehaviour
{
    [SerializeField] List<WavaConfig> WaveConfigs;
    [SerializeField] bool looping = false;
    int startingWave = 0;
    // Start is called before the first frame update
    IEnumerator Start()
    {
        do
        {
            yield return StartCoroutine(SpwanAllWaves());
        }
        while (looping);
        
        //var currentWave = WaveConfigs[startingWave];
        //StartCoroutine(SpawnAllEnemiesInWave(currentWave));
    }

    private IEnumerator SpwanAllWaves()
    {
        //Debug.Log("WORKING OR NOt");
        for(int waveIndex = startingWave; waveIndex < WaveConfigs.Count; waveIndex++)
        {
            var currentWave = WaveConfigs[waveIndex];
            yield return StartCoroutine(SpawnAllEnemiesInWave(currentWave));
        }
    }

    private IEnumerator SpawnAllEnemiesInWave(WavaConfig wave)
    {
        for (int i = 0; i < wave.getNumberOfEnemy(); i++)
        {
            var newEnemy = Instantiate(wave.getEnemyPrefab(),
            wave.GetWayPoints()[0].transform.position,
            Quaternion.identity);
            newEnemy.GetComponent<EnemyPath>().setWaveConfig(wave);
            yield return new WaitForSeconds(wave.getTimeBetweenSpawn());
        }
    }
}
