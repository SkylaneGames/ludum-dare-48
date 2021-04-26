using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CharacterMotor))]
public class AIController : MonoBehaviour
{
    public float minTimeBetweenInput = 0.5f;
    public float maxTimeBetweenInput = 3f;

    private float timeBeforeInput = 0f;

    private CharacterMotor _motor;

    private Collider2D _collider;

    void Awake()
    {
        _motor = GetComponent<CharacterMotor>();
        _collider = GetComponent<Collider2D>();
    }

    // Start is called before the first frame update
    void Start()
    {
        WalkInDoor();
    }

    private void WalkInDoor()
    {
        _motor.UpdatedMovement(Vector2.up);
        _collider.enabled = false;
        timeBeforeInput = 2f;

        StartCoroutine(EnableCollider());
    }

    private IEnumerator EnableCollider()
    {
        yield return new WaitForSeconds(2f);
        _collider.enabled = true;
    }

    // Update is called once per frame
    void Update()
    {
        timeBeforeInput -= Time.deltaTime;

        if (timeBeforeInput <= 0)
        {
            PerformInput();
            timeBeforeInput = Random.Range(minTimeBetweenInput, maxTimeBetweenInput);
        }

    }

    private void PerformInput()
    {
        _motor.UpdatedMovement(new Vector2(Random.Range(-1f, 1f), Random.Range(-1f, 1f)));
    }
}
