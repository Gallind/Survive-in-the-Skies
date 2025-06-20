using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.AI;

public class Unit : MonoBehaviour
{
    private float unitHP;
    public float unitMaxHP;
    public HealthTracker healthTracker;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        if (!gameObject.CompareTag("Enemy"))
        {
            UnitSelectionManager.Instance.allUnitsList.Add(gameObject);
        }
        unitHP = unitMaxHP;
        UpdateHP_UI();
    }

    private void OnDestroy()
    {
        UnitSelectionManager.Instance.allUnitsList.Remove(gameObject);
    }
    public void TakeDamage(int damage)
    {
        unitHP -= damage;
        UpdateHP_UI();
    }
    private void UpdateHP_UI()
    {
        healthTracker.UpdateSliderValue(unitHP, unitMaxHP);
        //if the unit is dead
        if (unitHP <= 0)
        {
            Destroy(gameObject);
        }
    }
}
