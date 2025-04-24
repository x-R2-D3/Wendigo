using UnityEngine;

public class NPCMove : MonoBehaviour
{
    public float speed = 2.0f; // Speed of the NPC
    public Vector3 pointA;
    public Vector3 pointB;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        float pingPongValue = Mathf.PingPong(Time.time * speed, 1f);
        transform.position = Vector3.Lerp(pointA, pointB, pingPongValue);
    }
}
