using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomerSpawner : MonoBehaviour
{
    [Range(0, 75)]
    public int MaxCustomers = 30;


    [Range(0, 1)]
    public float RateOfSpawn = 0.1f;

    public Transform SpawnPoint;
    public Animator DoorAnimator;

    public GameObject[] CustomerPrefabs;

    private float checkSpawn = 0f;
    private int customers = 0;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Time.time > checkSpawn)
        {
            if (Random.Range(0f, 1f) < RateOfSpawn)
            {
                SpawnCustomer();
            }
            
            checkSpawn = Time.time + 1f;
        }
    }

    private void SpawnCustomer()
    {
        var prefab = PickRandomPrefab();
        var spawnPoint = SpawnPoint.position;
        spawnPoint.x += Random.Range(-0.5f, 0.5f);
        Instantiate(prefab, spawnPoint, Quaternion.identity, transform);
        DoorAnimator?.SetTrigger("Open");
        customers++;
    }

    private GameObject PickRandomPrefab()
    {
        return CustomerPrefabs[Random.Range(0, CustomerPrefabs.Length)];
    }
}
