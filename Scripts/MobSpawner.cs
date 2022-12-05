using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MobSpawner : MonoBehaviour
{
    [SerializeField] GameObject mobToSpawn;
    [SerializeField] float maxSpawnDelay;
    float spawnDelay;
    private bool canSpawn = true;
    [SerializeField] private float spawnRange;
    [SerializeField] private bool spawnRangeIsNormalized;
    private static GameManager gameManager;
    // Start is called before the first frame update
    public float SpawnDelay
    {
        get { return spawnDelay; }
        set { spawnDelay = value; }
    }
    void Start()
    {
        spawnDelay = maxSpawnDelay;
        gameManager = FindObjectOfType<GameManager>();
    }

    // Update is called once per frame
    void Update()
    {
        StartCoroutine(SpawnDelayCoroutine(spawnDelay));
    }

    private void OnEnable()
    {
        DestroyAllChildren();

        canSpawn = true;
    }

    IEnumerator SpawnDelayCoroutine(float delay)
    {
        if (canSpawn)
        {
            canSpawn = false;
            yield return new WaitForSeconds(delay);
            Vector3 spawnPos = Vector3.zero;
            if (spawnRangeIsNormalized)
            {
                spawnPos = Camera.main.transform.position + spawnRange * new Vector3(Random.Range(-1f, 1f), Random.Range(-1f, 1f), 0f).normalized;
            }
            else
            {
                spawnPos = Camera.main.transform.position + spawnRange * new Vector3(Random.Range(-1f, 1f), Random.Range(-1f, 1f), 0f);
            }
            
            GameObject mob = Instantiate(mobToSpawn, spawnPos, Quaternion.identity, transform);
            if (mob.CompareTag("Enemy"))
            {
                mob.GetComponent<EnemyBehaviour>().HealthNaturalBuff = HPBuffOnSpawn(gameManager.WaveNumber);
            }
            canSpawn = true;
        }
    }

    private void DestroyAllChildren()
    {
        foreach (Transform child in transform)
        {
            Destroy(child.gameObject);
        }
    }

    private void OnDisable()
    {
        StopAllCoroutines();
        canSpawn = false;
        DestroyAllChildren();
        spawnDelay = maxSpawnDelay;
    }

    private float HPBuffOnSpawn(int level)
    {
        return 0.13f * Mathf.Exp(Mathf.Pow(level, 0.8f) / 2) + 0.75f;
    }
}
