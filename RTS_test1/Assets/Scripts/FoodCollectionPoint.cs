using UnityEngine;


public class FoodCollectionPoint : MonoBehaviour
{
    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Hero"))
        {
            float dist = Vector3.Distance(transform.position, other.transform.position);
            if (dist < 1.6f)
            {
                // disable the food indicator on the hero
                other.transform.Find("Food").gameObject.SetActive(false);
            }
        }
    }
}
