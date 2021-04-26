using System.Collections;
using System.Collections.Generic;
using CoreSystems;
using CoreSystems.TransitionSystem;
using MissionSystem.JobSystem;
using UnityEngine;

public class GameManager : Singleton<GameManager>
{
    private JobSystem _jobSystem;
    private AngryBoss _angryBoss;
    private PASystem _paSystem;
    private PlayerController _player;

    public Dialog[] IntroSpeech;

    public float introDelay = 2f;

    public bool GameStarted { get; private set; }
    private bool gameover = false;

    void Awake()
    {
        _jobSystem = FindObjectOfType<JobSystem>();
        _jobSystem.enabled = false;
        _angryBoss = FindObjectOfType<AngryBoss>();
        _paSystem = FindObjectOfType<PASystem>();
        _player = FindObjectOfType<PlayerController>();
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

    // Player has succumbed to the daydream and goes to confront the boss.
    public void TriggerGameOver()
    {
        if (gameover) return;

        gameover = true;
        _player.DisableInput();
        LevelLoader.Instance.LoadLevel(Level.GameOver);
    }

    // Update is called once per frame
    void Update()
    {

    }
}
