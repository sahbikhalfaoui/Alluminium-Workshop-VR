using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnerScript : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject cubePrefab;

    public void Spawn()
    {
        StartCoroutine(SpawnWithDelay(3.0f)); // Start the coroutine with a 3-second delay
    }

    private IEnumerator SpawnWithDelay(float delay)
    {
        yield return new WaitForSeconds(delay); // Wait for the specified delay
        Instantiate(cubePrefab, transform.position, Quaternion.identity); // Spawn the prefab
    }
}
