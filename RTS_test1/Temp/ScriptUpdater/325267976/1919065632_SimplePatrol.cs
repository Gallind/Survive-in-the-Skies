using UnityEngine;

public class SimplePatrol : MonoBehaviour
{
    public float speed = 5.0f; // Adjust the speed of movement

    private bool movingForward = true;
    private float timer = 0.0f;
    public float switchDirectionTime = 5.0f; // Time to switch direction
    Vector3 direction1, direction2;
    bool firstRound = true;

    void Start()
    {
        direction1 = transform.position;
    }

    void Update()
    {
        timer += Time.deltaTime;

        if (timer >= switchDirectionTime)
        {
            if (firstRound)
            {
                firstRound = false;
                direction2 = transform.position;
            }
            movingForward = !movingForward;
            timer = 0.0f;
        }
        if (firstRound)
        {
            if (movingForward)
            {
                transform.Translate(Vector3.forward * speed * Time.deltaTime);
                //transform.LookAt(Vector3.forward * speed * Time.deltaTime);
            }
            else
            {
                transform.Translate(Vector3.back * speed * Time.deltaTime);
                //transform.LookAt(Vector3.back * speed * Time.deltaTime);
            }
        }
        else
        {
            if (movingForward)
            {
                transform.GetComponent<UnityEngine.AI.NavMeshAgent>().SetDestination(direction2);
                //transform.LookAt(Vector3.forward * speed * Time.deltaTime);
            }
            else
            {
                transform.GetComponent<UnityEngine.AI.NavMeshAgent>().SetDestination(direction1);
                //transform.LookAt(Vector3.back * speed * Time.deltaTime);
            }
        }
    }
}