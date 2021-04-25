using System.Collections;
using System.Collections.Generic;
using CoreSystems;
using MissionSystem.JobSystem;
using UnityEngine;

public class GameManager : Singleton<GameManager>
{
    private JobSystem _jobSystem;
    private AngryBoss _angryBoss;
    private PASystem _paSystem;

    public Dialog[] IntroSpeech;

    public float introDelay = 2f;

    public bool GameStarted { get; private set; }

    void Awake()
    {
        _jobSystem = FindObjectOfType<JobSystem>();
        _jobSystem.enabled = false;
        _angryBoss = FindObjectOfType<AngryBoss>();
        _paSystem = FindObjectOfType<PASystem>();
    }

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(Introduction());
    }

    private IEnumerator Introduction()
    {
        yield return new WaitForSeconds(introDelay);

        foreach (var dialog in IntroSpeech)
        {
            _paSystem.MakeAnnouncement(dialog);
            yield return new WaitForSeconds(dialog.Duration + 0.5f);
        }

        StartGame();
    }

    private void StartGame()
    {
        _jobSystem.enabled = true;
        GameStarted = true;
    }

    // Update is called once per frame
    void Update()
    {

    }
}
