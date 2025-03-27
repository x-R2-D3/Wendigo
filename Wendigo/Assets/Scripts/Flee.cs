using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flee : MonoBehaviour
{
    public Transform target;
    public float fleeSpeed = 5f;
    public float fleeRange = 10f;
    private bool hasWon = false;

    void Start()
    {
        GameObject playerObject = GameObject.FindWithTag("Player");
        if (playerObject != null)
        {
            target = playerObject.transform;
        }
        else
        {
            Debug.LogError("Player object not found! Make sure the player has the 'Player' tag.");
        }
    }

    void Update()
    {
        if (target == null || hasWon) return;

        Vector3 direction = transform.position - target.position;
        float distance = direction.sqrMagnitude;

        if (distance < fleeRange)
        {
            transform.Translate(direction.normalized * fleeSpeed * Time.deltaTime, Space.World);
            transform.forward = direction.normalized;
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Goal") && !hasWon)
        {
            hasWon = true;
            Debug.Log(gameObject.name + " reached the goal!");
            target = null; // Stops movement

            // Notify the GameManager
            //SheepManager.Instance.sheepInGoal();
        }
    }
}
