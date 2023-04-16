using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ThurtwingsGames.Fruits
{
    public class FruitsSpawner : MonoBehaviour
    {
        private Collider spawnArea;
        [SerializeField] GameObject[] fruitsPrefabs;
        [SerializeField] GameObject bombPrefab;
        [Range(0,1)]public float bombChance = .05f;
        [SerializeField] internal float minSpawnDelay = .25f;
        [SerializeField] internal float maxSpawnDelay = 1f;
        [SerializeField] internal float minAngle = -15f;
        [SerializeField] internal float maxAngle = 15f;
        [SerializeField] internal float minForce = 18f;
        [SerializeField] internal float maxForce = 22f;
        [SerializeField] internal float maxLifeTime = 5f;


        private void Awake()
        {
            spawnArea = GetComponent<Collider>();

        }

        private void OnEnable()
        {
            StartCoroutine(Spawn());
        }

        private void OnDisable()
        {
            StopAllCoroutines();
        }

        private IEnumerator Spawn()
        {

            yield return new WaitForSeconds(3f);
            while (enabled)
            {
                GameObject prefab = fruitsPrefabs[Random.Range(0, fruitsPrefabs.Length)];

                if (Random.value < bombChance) prefab = bombPrefab;

                Vector3 pos = new Vector3();
                pos.x = Random.Range(spawnArea.bounds.min.x, spawnArea.bounds.max.x);

                pos.y = Random.Range(spawnArea.bounds.min.y, spawnArea.bounds.max.y);

                pos.z = Random.Range(spawnArea.bounds.min.z, spawnArea.bounds.max.z);

                Quaternion rot = Quaternion.Euler(0, 0, Random.Range(minAngle, maxAngle));

                GameObject fruit = Instantiate(prefab, pos, rot);

                Destroy(fruit, maxLifeTime);

                float force = Random.Range(minForce, maxForce);

                fruit.GetComponent<Rigidbody>().AddForce(fruit.transform.up * force, ForceMode.Impulse);

                yield return new WaitForSeconds(Random.Range(minSpawnDelay, maxSpawnDelay));


            }
        }
        
    }
}
