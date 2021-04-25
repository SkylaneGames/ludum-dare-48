using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class DreamVision : MonoBehaviour
{
    private IEnumerable<Material> _materials;
    public Transform _player;

    [SerializeField]
    [Range(0f, 1f)]
    private float percentage = 0f;
    public float Percentage
    {
        get { return percentage; }
        set
        {
            percentage = Mathf.Clamp01(value);
        }
    }

    void Awake()
    {
        _materials = FindMaterials();
    }

    private IEnumerable<Material> FindMaterials()
    {
        var materials = Resources.FindObjectsOfTypeAll<Material>();

        return materials.Where(p => p.shader.name == "Shader Graphs/Dreamworld");
    }

    // Update is called once per frame
    void Update()
    {
        foreach (var material in _materials)
        {
            material.SetVector("PlayerPos", _player.transform.position);
            material.SetFloat("Percentage", Percentage);
        }

        // Set any NPCs within a radius of the player (based on percentage) to be enemies.
    }
}
