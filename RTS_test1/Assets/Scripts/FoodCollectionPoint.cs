using UnityEngine;

public class FoodCollectionPoint : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        Debug.Log(Vector3.Distance(transform.position, other.transform.position));
        if (other.gameObject.name == "Hero" && Vector3.Distance(transform.position, other.transform.position) < 1f)
        {
            Transform foodTransform = other.transform.Find("Food");

            if (foodTransform != null && foodTransform.gameObject.activeSelf) // checks if the food is active on the hero
            {
                Debug.Log("1");
                foodTransform.gameObject.SetActive(false);
            }
        }
    }
}
