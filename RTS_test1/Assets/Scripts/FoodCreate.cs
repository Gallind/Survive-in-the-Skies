using Unity.VisualScripting;
using UnityEngine;

public class FoodCreate : MonoBehaviour
{
    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Hero") && !other.transform.Find("Food").gameObject.activeSelf)
        {
            float dist = Vector3.Distance(transform.position, other.transform.position);
            if (dist < 1.6f)
            {
                other.transform.Find("Food").gameObject.SetActive(true);

                // remove the food crate from the game
                Destroy(gameObject);
            }
        }
    }
}
