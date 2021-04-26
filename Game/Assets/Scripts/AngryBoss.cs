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

    public List<Dialog> JobFailDialog = new List<Dialog>{
        new Dialog{Text="Those shelves aren't going to stack themselves you know!!", Duration = 4},
        new Dialog{Text="You better not be daydreaming out there?!", Duration = 4}
    };

    public List<Dialog> WrongShelfDialog = new List<Dialog>{
        new Dialog{Text="That doesn't go there!!", Duration = 4},
        new Dialog{Text="Can't you see the signs?!", Duration = 4}
    };

    public List<Dialog> WrongColourDialog = new List<Dialog>{
        new Dialog{Text="**Sigh** How many times have a I told you?! You need to match the colours!!", Duration = 4}
    };

    public List<Dialog> ShelfFullDialog = new List<Dialog>{
        new Dialog{Text="We've already got plenty of those!!", Duration = 4}
    };

    void Awake()
    {
        _paSystem = FindObjectOfType<PASystem>();
    }

    public void OnJobSuccess()
    {
        DreamVision.Instance.DecreaseDreamState(0.05f);
    }

    public void OnJobFail()
    {
        DreamVision.Instance.IncreaseDreamState(0.1f);
        if (!_paSystem.InUse && Random.Range(0f, 1f) < ResponseProbabilityFail)
        {
            RespondToPlayer(JobFailDialog);
        }
    }

    public void OnShelfFull()
    {
        DreamVision.Instance.IncreaseDreamState(0.1f);
        if (!_paSystem.InUse && Random.Range(0f, 1f) < ResponseProbabilityShelfFull)
        {
            RespondToPlayer(ShelfFullDialog);
        }
    }

    // Parameter 'categoryCorrect': Shows if the player got the correct category of item, but the wrong colour. Can be used to provide hints.
    public void OnWrongShelf(bool categoryCorrect)
    {
        DreamVision.Instance.IncreaseDreamState(0.1f);
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

    private void RespondToPlayer(IList<Dialog> dialogOptions)
    {
        var dialog = PickRandomDialog(dialogOptions);
        _paSystem.MakeAnnouncement(dialog);
    }

    private Dialog PickRandomDialog(IList<Dialog> options)
    {
        int index = Random.Range(0, options.Count());

        return options[index];
    }
}
