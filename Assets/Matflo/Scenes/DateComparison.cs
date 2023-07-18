using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DateComparison : MonoBehaviour
{
    public GameObject canvas;
    DateTime currentDate;
    DateTime targetDate;
    void Start()
    {
        currentDate = DateTime.Now;
        targetDate = new DateTime(2023, 7, 28);
        dateCompare();
    }


    public void dateCompare()
    {
        int comparisonResult = DateTime.Compare(currentDate, targetDate);
        if (comparisonResult > 0)
        {
            Debug.Log("Current date is after the target date.");
            canvas.SetActive(false);
        }
        else if (comparisonResult < 0)
        {
           // Debug.Log("Current date is before the target date.");
        }
        else
        {
           // Debug.Log("Current date is the same as the target date.");
        }
    }
}
