using System.Collections.Generic;
using UnityEngine;

namespace VeggieJump.Platforms
{
    /// <summary>
    /// Procedurally spawns platforms above the player as they climb, and
    /// recycles (destroys) platforms that have scrolled far below the camera.
    /// Attach this to an empty "PlatformSpawner" GameObject in the scene.
    /// </summary>
    public class PlatformSpawner : MonoBehaviour
    {
        [Header("References")]
        [SerializeField] private GameObject normalPlatformPrefab;
        [SerializeField] private GameObject movingPlatformPrefab;
        [SerializeField] private Transform player;
        [SerializeField] private Camera mainCamera;

        [Header("Generation Settings")]
        [SerializeField] private float minGapY = 1.2f;
        [SerializeField] private float maxGapY = 2.2f;
        [SerializeField] private float spawnWidthX = 2.6f;
        [SerializeField] private float movingPlatformChance = 0.2f;
        [SerializeField] private float despawnBelowDistance = 12f;

        private readonly List<GameObject> activePlatforms = new List<GameObject>();
        private float highestSpawnY;

        private void Start()
        {
            // Seed a few platforms above the starting position immediately
            highestSpawnY = player.position.y - 1f;
            for (int i = 0; i < 10; i++)
            {
                SpawnNextPlatform();
            }
        }

        private void Update()
        {
            float cameraTopY = mainCamera.transform.position.y + GetCameraHalfHeight();

            // Keep spawning while the highest platform is below the camera's view
            while (highestSpawnY < cameraTopY + 2f)
            {
                SpawnNextPlatform();
            }

            DespawnOldPlatforms();
        }

        private void SpawnNextPlatform()
        {
            highestSpawnY += Random.Range(minGapY, maxGapY);
            float x = Random.Range(-spawnWidthX, spawnWidthX);

            bool isMoving = Random.value < movingPlatformChance && movingPlatformPrefab != null;
            GameObject prefab = isMoving ? movingPlatformPrefab : normalPlatformPrefab;

            GameObject platform = Instantiate(prefab, new Vector3(x, highestSpawnY, 0f), Quaternion.identity, transform);
            activePlatforms.Add(platform);
        }

        private void DespawnOldPlatforms()
        {
            float cameraBottomY = mainCamera.transform.position.y - GetCameraHalfHeight() - despawnBelowDistance;

            for (int i = activePlatforms.Count - 1; i >= 0; i--)
            {
                if (activePlatforms[i] == null)
                {
                    activePlatforms.RemoveAt(i);
                    continue;
                }

                if (activePlatforms[i].transform.position.y < cameraBottomY)
                {
                    Destroy(activePlatforms[i]);
                    activePlatforms.RemoveAt(i);
                }
            }
        }

        private float GetCameraHalfHeight()
        {
            return mainCamera.orthographic ? mainCamera.orthographicSize : 5f;
        }
    }
}
