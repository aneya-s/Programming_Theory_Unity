using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine.SceneManagement;
using Unity.VisualScripting;

public class GameManager : MonoBehaviour
{
    public BusStand[] busStands;
    public TaxiStand[] taxiStands;
    public Taxi[] taxis;
    public Bus[] busses;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
       busStands = FindObjectsByType<BusStand>(FindObjectsSortMode.None);
       taxiStands = FindObjectsByType<TaxiStand>(FindObjectsSortMode.None);
       taxis = FindObjectsByType<Taxi>(FindObjectsSortMode.None);
       busses = FindObjectsByType<Bus>(FindObjectsSortMode.None);
    }

    // Update is called once per frame
    void Update()
    {
        foreach (Taxi taxi in taxis)
        {
            if (!taxi.busy)
            {
                taxi.GoTo(taxiStands[Random.Range(0, taxiStands.Length)]);
            }
        }

        foreach (Bus bus in busses)
        {
            if (!bus.busy)
            {
                bus.GoTo(busStands[Random.Range(0, busStands.Length)]);
            }
        }
    }
    
}
