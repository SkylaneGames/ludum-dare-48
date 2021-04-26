using System.Collections;
using System.Collections.Generic;
using System.Linq;
using CoreSystems;
using CoreSystems.MusicSystem;
using UnityEngine;

public class DreamVision : Singleton<DreamVision>
{
    private IEnumerable<Material> _materials;
    public Transform _player;

    [SerializeField]
    [Range(0f, 1f)]
    private float percentage = 0f;

    public float InitialDreamDelay = 10f;
    public float DelayAfterSuccess = 2f;

    private float targetPercentage = 0f;
    public float TargetPercentage
    {
        get { return targetPercentage; }
        set
        {
            targetPercentage = Mathf.Clamp01(value);
        }
    }
    
    public float RateOfChangePerSecond = 0.05f;

    void Awake()
    {
        _materials = FindMaterials();
    }

    public void IncreaseDreamState(float amount)
    {
        var delay = targetPercentage == 0f ? InitialDreamDelay : DelayAfterSuccess;
        StartCoroutine(DaydreamAfter(amount, delay));

    }

    public void DecreaseDreamState(float amount)
    {
        // StopCoroutine("DaydreamAfter");
        TargetPercentage -= amount;
    }

    private IEnumerator DaydreamAfter(float increase, float after)
    {
        yield return new WaitForSeconds(after);

        TargetPercentage += increase;
    }

    private IEnumerable<Material> FindMaterials()
    {
        var materials = Resources.FindObjectsOfTypeAll<Material>();

        return materials.Where(p => p.shader.name == "Shader Graphs/Dreamworld");
    }

    // Update is called once per frame
    void Update()
    {
        percentage = Mathf.MoveTowards(percentage, targetPercentage, RateOfChangePerSecond * Time.deltaTime);

        foreach (var material in _materials)
        {
            material.SetVector("PlayerPos", _player.transform.position);
            material.SetFloat("Percentage", percentage);
        }

        if (percentage >= 1f)
        {
            GameManager.Instance.TriggerGameOver();
        }

        var musicVolume = 1f - percentage;
        MusicManager.Instance?.SetVolume(MusicTrackIdentifier.MainTrack, musicVolume);

        // Set any NPCs within a radius of the player (based on percentage) to be enemies.
    }
}
