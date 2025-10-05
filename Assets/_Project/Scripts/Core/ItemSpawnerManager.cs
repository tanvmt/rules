using UnityEngine;
using System.Collections.Generic;
using System.Linq;

namespace NenNhangSinhMenh.Core
{
    public class ItemSpawnerManager : MonoBehaviour
    {
        public static ItemSpawnerManager Instance { get; private set; }

        [Header("Spawn Locations")]
        [Tooltip("The specific location where the first items will spawn (usually on the Altar).")]
        [SerializeField] private Transform initialSpawnPoint;

        [Tooltip("List of all possible random locations for subsequent items.")]
        [SerializeField] private List<Transform> randomSpawnPoints;

        [Header("Item Lists")]
        [Tooltip("Items that spawn together at the initialSpawnPoint at the very beginning.")]
        [SerializeField] private List<GameObject> initialItemPrefabs;

        [Tooltip("Incense prefabs to be spawned sequentially after the first one is used.")]
        [SerializeField] private List<GameObject> sequentialIncensePrefabs;

        private List<Transform> _availableRandomSpawnPoints;
        private int _currentIncenseIndex = 0;

        private void Awake()
        {
            if (Instance != null && Instance != this)
            {
                Destroy(gameObject);
            }
            else
            {
                Instance = this;
            }

            _availableRandomSpawnPoints = new List<Transform>(randomSpawnPoints);
            
            SpawnInitialItems();
        }

        private void SpawnInitialItems()
        {
            if (initialSpawnPoint == null)
            {
                Debug.LogError("Initial Spawn Point is not set in the ItemSpawnerManager!");
                return;
            }

            for (int i = 0; i < initialItemPrefabs.Count; i++)
            {
                GameObject itemPrefab = initialItemPrefabs[i];
                Vector3 spawnPosition = initialSpawnPoint.position + Vector3.right * (i * 0.2f); 
                
                Instantiate(itemPrefab, spawnPosition, initialSpawnPoint.rotation);
                Debug.Log($"Spawned initial item {itemPrefab.name} at the altar.");
            }
        }

        public void SpawnNextIncense()
        {
            if (_currentIncenseIndex >= sequentialIncensePrefabs.Count)
            {
                Debug.Log("All incense sticks have been spawned.");
                return;
            }
            
            if (_availableRandomSpawnPoints.Count == 0)
            {
                Debug.LogError("No available random spawn points left!");
                return;
            }

            int randomIndex = Random.Range(0, _availableRandomSpawnPoints.Count);
            Transform spawnPoint = _availableRandomSpawnPoints[randomIndex];
            _availableRandomSpawnPoints.RemoveAt(randomIndex);

            GameObject incenseToSpawn = sequentialIncensePrefabs[_currentIncenseIndex];
            
            Instantiate(incenseToSpawn, spawnPoint.position, spawnPoint.rotation);
            Debug.Log($"Spawned incense #{_currentIncenseIndex + 2} ({incenseToSpawn.name}) at {spawnPoint.name}");

            _currentIncenseIndex++;
        }
    }
}