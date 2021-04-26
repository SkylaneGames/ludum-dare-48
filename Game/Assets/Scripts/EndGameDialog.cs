using System.Collections;
using System.Collections.Generic;
using CoreSystems.TransitionSystem;
using UnityEngine;

public class EndGameDialog : MonoBehaviour
{
    public float Delay = 2f;
    public Dialog[] Dialog;
    
    private PASystem _paSystem;
    

    // Start is called before the first frame update
    void Awake()
    {
        _paSystem = FindObjectOfType<PASystem>();
    }

    void Start()
    {
        StartCoroutine(PlayDialog());
    }

    private IEnumerator PlayDialog()
    {
        yield return new WaitForSeconds(Delay);

        foreach (var dialog in Dialog)
        {
            _paSystem.MakeAnnouncement(dialog);
            yield return new WaitForSeconds(dialog.Duration + 0.5f);
        }

        LevelLoader.Instance.LoadLevel((Level.Menu));
    }
}
