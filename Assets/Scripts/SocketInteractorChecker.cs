using System.Collections.Generic;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine;
using System.Collections;

public class SocketInteractorChecker : MonoBehaviour
{
    public List<XRSocketInteractor> socketInteractors = new List<XRSocketInteractor>();
    private HashSet<GameObject> stuckObjects = new HashSet<GameObject>();

    public string prefabTagToDelete = "DeleteablePrefab"; // Tag of the prefabs to delete
    public GameObject prefabToSpawn; // Prefab to spawn
    public Vector3 spawnPosition; // Position to spawn the new prefab
    public float delay = 3.0f;
    void Start()
    {
        // Subscribe to the events
        foreach (var interactor in socketInteractors)
        {
            interactor.selectEntered.AddListener(OnObjectEnteredSocket);
            interactor.selectExited.AddListener(OnObjectExitedSocket);
        }
    }

    void OnDestroy()
    {
        // Unsubscribe from the events to avoid memory leaks
        foreach (var interactor in socketInteractors)
        {
            interactor.selectEntered.RemoveListener(OnObjectEnteredSocket);
            interactor.selectExited.RemoveListener(OnObjectExitedSocket);
        }
    }

    void OnObjectEnteredSocket(SelectEnterEventArgs args)
    {
        // Check if the interactableObject is the GameObject you want to stick
        var interactableMonoBehaviour = args.interactableObject as MonoBehaviour;
        if (interactableMonoBehaviour != null && interactableMonoBehaviour.CompareTag("StickyObject"))
        {
            stuckObjects.Add(interactableMonoBehaviour.gameObject);
            Debug.Log("Object entered socket. Current stuck objects count: " + stuckObjects.Count);

            // Check if all socket interactors have a stuck object
            if (stuckObjects.Count == socketInteractors.Count)
            {
                Debug.Log("All objects are stuck!");
            }
        }
        if (interactableMonoBehaviour != null && interactableMonoBehaviour.CompareTag("Small"))
        {
            stuckObjects.Add(interactableMonoBehaviour.gameObject);
            Debug.Log("Object entered socket. Current stuck objects count: " + stuckObjects.Count);

            // Check if all socket interactors have a stuck object
            if (stuckObjects.Count == socketInteractors.Count)
            {
                Debug.Log("All objects are stuck!");
            }
        }
        if (interactableMonoBehaviour != null && interactableMonoBehaviour.CompareTag("Frame"))
        {
            stuckObjects.Add(interactableMonoBehaviour.gameObject);
            Debug.Log("Object entered socket. Current stuck objects count: " + stuckObjects.Count);

            // Check if all socket interactors have a stuck object
            if (stuckObjects.Count == socketInteractors.Count)
            {
                Debug.Log("All objects are stuck!");
            }
        }
    }

    void OnObjectExitedSocket(SelectExitEventArgs args)
    {
        // Check if the interactableObject is the GameObject you want to stick
        var interactableMonoBehaviour = args.interactableObject as MonoBehaviour;
        if (interactableMonoBehaviour != null && interactableMonoBehaviour.CompareTag("StickyObject"))
        {
            stuckObjects.Remove(interactableMonoBehaviour.gameObject);
            Debug.Log("Object exited socket. Current stuck objects count: " + stuckObjects.Count);
        }
        if (interactableMonoBehaviour != null && interactableMonoBehaviour.CompareTag("Small"))
        {
            stuckObjects.Remove(interactableMonoBehaviour.gameObject);
            Debug.Log("Object exited socket. Current stuck objects count: " + stuckObjects.Count);
        }
        if (interactableMonoBehaviour != null && interactableMonoBehaviour.CompareTag("Frame"))
        {
            stuckObjects.Remove(interactableMonoBehaviour.gameObject);
            Debug.Log("Object exited socket. Current stuck objects count: " + stuckObjects.Count);
        }
    }

    // Function to check if all objects are stuck, delete specified prefabs, and spawn a new prefab
    public void CheckAndReplaceObjects()
    {
        StartCoroutine(CheckAndReplaceObjectsAfterDelay(delay));
    }

    private IEnumerator CheckAndReplaceObjectsAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay); // Wait for the specified delay

        if (stuckObjects.Count == socketInteractors.Count)
        {
            // Delete all GameObjects with the specified tag
            GameObject[] objectsToDelete = GameObject.FindGameObjectsWithTag(prefabTagToDelete);
            foreach (GameObject obj in objectsToDelete)
            {
                Destroy(obj);
            }

            // Spawn the specified prefab at the specified position
            if (prefabToSpawn != null)
            {
                Instantiate(prefabToSpawn, spawnPosition, Quaternion.identity);
            }

            Debug.Log("Deleted objects and spawned new prefab.");
        }
        else
        {
            Debug.Log("Not all objects are stuck yet.");
        }
    }
}