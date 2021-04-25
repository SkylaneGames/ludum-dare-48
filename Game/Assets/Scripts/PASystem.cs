using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PASystem : MonoBehaviour
{
    public bool InUse { get; private set; }
    public GameObject PAGameObject;
    public TMP_Text Text;

    public float Duration = 4f;

    void Start()
    {
        PAGameObject.SetActive(false);
    }

    public bool MakeAnnouncement(Dialog dialog)
    {
        if (InUse)
        {
            return false;
        }

        Text.text = dialog.Text;

        StartCoroutine(DisplayAnnouncementFor(dialog.Duration));

        return true;
    }

    public IEnumerator DisplayAnnouncementFor(float seconds)
    {
        InUse = true;
        PAGameObject.SetActive(true);
        yield return new WaitForSeconds(seconds);
        PAGameObject.SetActive(false);
        InUse = false;
    }
}
