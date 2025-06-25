using UnityEngine;

public class FoodCreate : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        Debug.Log(Vector3.Distance(transform.position, other.transform.position));
        if (other.gameObject.name == "Hero" && Vector3.Distance(transform.position, other.transform.position) < 1f)
        {
            Debug.Log("1");
            other.transform.Find("Food").gameObject.SetActive(true);
        }
    }
}
