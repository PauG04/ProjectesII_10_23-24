using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public class SaveComponents : MonoBehaviour
{
    [Header("Bottles")]
    [SerializeField] private List<GameObject> bottleOjects;
    [SerializeField] private List<LiquidManager> liquidsManager;
    [SerializeField] private List<BuyShop> store;

    [Header("Money")]
    [SerializeField] private EconomyManager economyManager;

    [Header("Days")]
    [SerializeField] private DayManager dayManager;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.S))
        {
            Debug.Log("Saved Components");
            SaveAllComponents();
        }
        if (Input.GetKeyDown(KeyCode.L))
        {
            LoadAllComponents();
        }
    }

    public void SaveAllComponents()
    {
        liquidsManager.Clear();
        foreach (GameObject bottle in bottleOjects)
        {
            if (bottle.transform.childCount > 0)
            {
                liquidsManager.Add(bottle.transform.GetChild(0).GetComponentInChildren<LiquidManager>());
            }
            // Has Child
            if (bottle.transform.childCount >= 2)
            {
                PlayerPrefs.SetInt(bottle.name + "_BottleHasChild", 1);
                Debug.Log("Saved " + bottle.name + "_BottleHasChild with value: " + 1);

            }
            // Is Empty
            else if (bottle.transform.childCount <= 0)
            {
                PlayerPrefs.SetInt(bottle.name + "_BottleHasChild", -1);
                Debug.Log("Saved " + bottle.name + "_BottleHasChild with value: " + -1);

            }
            else
            {
                PlayerPrefs.SetInt(bottle.name + "_BottleHasChild", 0);
                Debug.Log("Saved " + bottle.name + "_BottleHasChild with value: " + 0);
            }
        }

        foreach (LiquidManager liquid in liquidsManager) 
        {
            PlayerPrefs.SetInt(liquid.name + "_BottleCurrentLiquid", liquid.GetCurrentLiquid());
            Debug.Log("Saved " + liquid.name + "_BottleCurrentLiquid with value: " + liquid.GetCurrentLiquid());
        }

        PlayerPrefs.SetFloat("Money", economyManager.GetMoney());
        Debug.Log("Saved " + economyManager.GetMoney() + "€ CurrentMoney");

        PlayerPrefs.SetInt("CurrentDay", dayManager.GetCurrentDay());
        Debug.Log("Saved " + PlayerPrefs.GetInt("CurrentDay") + " Day.");

    }

    public void LoadAllComponents()
    {
        dayManager.SetCurrentDay(PlayerPrefs.GetInt("CurrentDay", 1));
        Debug.Log("Loaded " + PlayerPrefs.GetInt("CurrentDay") + " Day.");

        for (int i = 0; i < bottleOjects.Count; i++)
        {
            if (PlayerPrefs.GetInt(bottleOjects[i].name + "_BottleHasChild") > 0)
            {
                Debug.Log("Loaded " + bottleOjects[i].name + "_BottleHasChild with value: " + 1);

                GameObject item = Instantiate(store[i].GetObject()/*, store[i].GetPosition(), Quaternion.identity, bottleOjects[i].transform*/);
                store[i].CreateBackgroundDrink(item);

                // Create Bottle
            }
            else if (PlayerPrefs.GetInt(bottleOjects[i].name + "_BottleHasChild") < 0)
            {
                Debug.Log("Loaded " + bottleOjects[i].name + "_BottleHasChild with value: " + -1);
                for (int j = bottleOjects[i].transform.childCount - 1; j >= 0; j--)
                {
                    Destroy(bottleOjects[j].transform.GetChild(j).gameObject);
                }
                // Delete Bottle
            }
            else
            {
                Debug.Log("Loaded " + bottleOjects[i].name + "_BottleHasChild with default value");
            }
        }
       
        foreach (LiquidManager liquid in liquidsManager)
        {
            liquid.SetCurrentLiquid(PlayerPrefs.GetInt(liquid.name + "_BottleCurrentLiquid", liquid.GetMaxLiquid()));
            Debug.Log("Loaded " + liquid.name + "_BottleCurrentLiquid with liquid: " + PlayerPrefs.GetFloat(liquid.name + "_BottleCurrentLiquid"));
        }

        economyManager.SetMoney(PlayerPrefs.GetFloat("Money", 0));
        Debug.Log("Loaded " + PlayerPrefs.GetFloat("Money", 0) + "€ CurrentMoney");


    }
}
