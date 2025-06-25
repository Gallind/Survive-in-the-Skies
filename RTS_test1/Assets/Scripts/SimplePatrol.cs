using UnityEngine;
using UnityEngine.AI;
public class SimplePatrol : MonoBehaviour
{
    public float speed = 5.0f; // Adjust the speed of movement
    private bool movingForward = true;
    private float timer = 0.0f;
    public float switchDirectionTime = 5.0f; // Time to switch direction
    Vector3 direction1, direction2;
    bool firstRound = true;
    bool assignDirection1 = true;

    void Start()
    {
        //direction1 = transform.position;
    }

    void Update()
    {
        if (assignDirection1)
        {
            direction1 = transform.position;
            assignDirection1 = false;
        }
        timer += Time.deltaTime;

        if (timer >= switchDirectionTime && firstRound)
        {
            firstRound = false;
            direction2 = transform.position;
            movingForward = !movingForward;
            timer = 0.0f;
        }
        if (movingForward && transform.position == direction2 || !movingForward && transform.position == direction1)
        {
            movingForward = !movingForward;
        }
        if (firstRound)
        {
            if (movingForward)
            {
                transform.Translate(Vector3.forward * speed * Time.deltaTime);
            }
            else
            {
                transform.Translate(Vector3.back * speed * Time.deltaTime);
            }
        }
        else
        {
            if (movingForward)
            {
                transform.GetComponent<NavMeshAgent>().SetDestination(direction2);
                transform.LookAt(direction2);
            }
            else
            {
                transform.GetComponent<NavMeshAgent>().SetDestination(direction1);
                transform.LookAt(direction1);
            }
        }
    }
}