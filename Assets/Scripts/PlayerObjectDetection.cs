using UnityEngine;
using Unity.Collections;
using Unity.Jobs;
using Unity.Burst;

[BurstCompile]
public struct RaycastJob : IJobParallelFor
{
    [ReadOnly]
    public Vector3 playerPosition;
    [ReadOnly]
    public GameObject[] objectsToDetect;
    [ReadOnly]
    public LayerMask detectionLayer;
    public NativeArray<RaycastHit> raycastHits;

    public void Execute(int index)
    {
        Vector3 origin = playerPosition;
        Vector3 direction = objectsToDetect[index].transform.position - playerPosition;
        RaycastHit hit;
        if (Physics.Raycast(origin, direction, out hit, direction.magnitude, detectionLayer))
        {
            raycastHits[index] = hit;
        }
    }
}

public class PlayerObjectDetection : MonoBehaviour
{
    public GameObject[] objectsToDetect;
    public LayerMask detectionLayer;

    private NativeArray<RaycastHit> raycastHits;
    private JobHandle raycastJobHandle;

    private void OnEnable()
    {
        raycastHits = new NativeArray<RaycastHit>(objectsToDetect.Length, Allocator.Persistent);
    }

    private void OnDisable()
    {
        raycastHits.Dispose();
    }

    void Update()
    {
        RaycastJob raycastJob = new RaycastJob
        {
            playerPosition = transform.position,
            objectsToDetect = objectsToDetect,
            detectionLayer = detectionLayer,
            raycastHits = raycastHits
        };

        raycastJobHandle = raycastJob.Schedule(objectsToDetect.Length, 64);
    }

    void LateUpdate()
    {
        raycastJobHandle.Complete();

        for (int i = 0; i < raycastHits.Length; i++)
        {
            RaycastHit hit = raycastHits[i];
            if (hit.collider != null)
            {
                Debug.Log("Object detected: " + hit.collider.gameObject.name);
                // Do something with the detected object
            }
        }
    }
}
