using System.Collections;
using System.Collections.Generic;
using MissionSystem.JobSystem;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private JobSystem _jobSystem;

    void Awake()
    {
        _jobSystem = FindObjectOfType<JobSystem>();
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
