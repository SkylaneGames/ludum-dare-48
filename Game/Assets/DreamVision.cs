using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DreamVision : MonoBehaviour
{
    private Material _material;
    public Transform _player;

    [Range(0f, 1f)]
    public float Percentage = 0f;

    void Awake()
    {
        _material = GetComponent<SpriteRenderer>().material;
    }

    // Update is called once per frame
    void Update()
    {
        _material.SetVector("PlayerPos", _player.transform.position);
        _material.SetFloat("Percentage", Percentage);
    }
}
