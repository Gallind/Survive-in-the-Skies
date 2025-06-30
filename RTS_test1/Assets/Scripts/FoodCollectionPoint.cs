using System.Threading;
using UnityEngine;


public class FoodCollectionPoint : MonoBehaviour
{
    public FoodQuotaTimer timer;
    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Hero"))
        {
            float dist = Vector3.Distance(transform.position, other.transform.position);
            if (dist < 1.6f && other.transform.Find("Food").gameObject.activeSelf)
            {
                // disable the food indicator on the hero
                other.transform.Find("Food").gameObject.SetActive(false);
                timer.AddFood(25);
            }
        }
    }
}
