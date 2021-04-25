using System.Collections;
using System.Collections.Generic;
using System.Linq;
using CoreSystems;
using UnityEngine;

public class AngryBoss : Singleton<AngryBoss>
{
    private PASystem _paSystem;

    [Range(0f, 1f)] 
    public float ResponseProbabilityFail = 0.25f;

    [Range(0f, 1f)]
    public float ResponseProbabilityShelfFull = 0.75f;
    
    [Range(0f, 1f)]
    public float ResponseProbabilityWrongItem = 0.75f;

    public List<string> JobFailDialog = new List<string>{
        "Those shelves aren't going to stack themselves you know!!",
        "You better not be daydreaming out there?!"
    };

    public List<string> WrongShelfDialog = new List<string>{
        "That doesn't go there!!",
        "Can't you see the signs?!"
    };

    public List<string> WrongColourDialog = new List<string>{
        "**Sigh** How many times have a I told you?! You need to match the colours!!"
    };

    public List<string> ShelfFullDialog = new List<string>{
        "We've already got plenty of those!!"
    };

    void Awake()
    {
        _paSystem = FindObjectOfType<PASystem>();
    }

    public void OnJobSuccess()
    {
    }

    public void OnJobFail()
    {
        if (!_paSystem.InUse && Random.Range(0f, 1f) < ResponseProbabilityFail)
        {
            RespondToPlayer(JobFailDialog);
        }
    }

    public void OnShelfFull()
    {
        if (!_paSystem.InUse && Random.Range(0f, 1f) < ResponseProbabilityShelfFull)
        {
            RespondToPlayer(ShelfFullDialog);
        }
    }

    // Parameter 'categoryCorrect': Shows if the player got the correct category of item, but the wrong colour. Can be used to provide hints.
    public void OnWrongShelf(bool categoryCorrect)
    {
        if (!_paSystem.InUse && Random.Range(0f, 1f) < ResponseProbabilityWrongItem)
        {
            if (categoryCorrect)
            {
                RespondToPlayer(WrongShelfDialog.Union(WrongColourDialog).ToList());
            }
            else
            {
                RespondToPlayer(WrongShelfDialog);
            }
        }
    }

    private void RespondToPlayer(IList<string> dialogOptions)
    {
        var dialog = PickRandomDialog(dialogOptions);
        _paSystem.MakeAnnouncement(dialog);
    }

    private string PickRandomDialog(IList<string> options)
    {
        int index = Random.Range(0, options.Count());

        return options[index];
    }
}
