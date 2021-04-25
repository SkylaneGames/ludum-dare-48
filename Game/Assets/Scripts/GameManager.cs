using System.Collections;
using System.Collections.Generic;
using CoreSystems;
using MissionSystem.JobSystem;
using UnityEngine;

public class GameManager : Singleton<GameManager>
{
    private JobSystem _jobSystem;
    private AngryBoss _angryBoss;

    void Awake()
    {
        _jobSystem = FindObjectOfType<JobSystem>();
        _angryBoss = FindObjectOfType<AngryBoss>();
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
