using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DaysEventsController : MonoBehaviour
{
    [SerializeField] private List<GameObject> events;

    private void Awake()
    {
        for(int i = 0; i < events.Count; i++) 
        {
            events[i].SetActive(false);
        }
    }

    public void ActiveEventDay(int day)
    {
        day -= 1;
        switch (day)
        {
            case 0:
                events[day].SetActive(true);
                break;
            case 1:
                events[day].SetActive(true);
                events[day - 1].SetActive(false);
                break;
            //case 2:
            //    events[day].SetActive(true);
            //    events[day - 1].SetActive(false);
            //    break;
            //case 3:
            //    events[day].SetActive(true);
            //    events[day - 1].SetActive(false);
            //    break;
            //case 4:
            //    events[day].SetActive(true);
            //    events[day - 1].SetActive(false);
            //    break;
        }
    }
}
