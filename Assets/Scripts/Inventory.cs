using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//A.I.
public class Inventory : MonoBehaviour
{
    public int itemInUse { get; private set; } = 0;  //0 = None | 1 = Phone | 2 = Firework | 3 = Lighter 

    public GameObject[] inventory;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        Use();
    }

    void Use()
    {
        if (Input.GetKeyDown("0") || Input.GetKeyDown("1") && itemInUse == 1
            || Input.GetKeyDown("2") && itemInUse == 2
            || Input.GetKeyDown("3") && itemInUse == 3)
        {
            itemInUse = 0;
            foreach (var item in inventory)
                item.SetActive(false);
        }
        else if (Input.GetKeyDown("1"))
        {
            itemInUse = 1;
            foreach (var item in inventory)
                item.SetActive(false);
            inventory[0].SetActive(true);
        }
        else if (Input.GetKeyDown("2"))
        {
            itemInUse = 2;
            foreach (var item in inventory)
                item.SetActive(false);
            inventory[1].SetActive(true);
        }
        else if (Input.GetKeyDown("3"))
        {
            itemInUse = 3;
            foreach (var item in inventory)
                item.SetActive(false);
            inventory[2].SetActive(true);
        }
    }


}
