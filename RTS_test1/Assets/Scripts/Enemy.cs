using UnityEngine;

public class Enemy : MonoBehaviour
{
    public int enemyHP;
    public void ReceiveDamage(int damage)
    {
        enemyHP -= damage;
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
