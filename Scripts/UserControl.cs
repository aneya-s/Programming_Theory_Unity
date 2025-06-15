using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// This script handle all the control code, so detecting when the users click on a unit or building and selecting those
/// If a unit is selected it will give the order to go to the clicked point or building when right clicking.
/// </summary>
public class UserControl : MonoBehaviour
{
    public Camera GameCamera;
    public float PanSpeed = 10.0f;
    public GameObject Marker;

    private Vehicle m_Selected = null;

    private void Start()
    {
        Marker.SetActive(false);
    }

    public void HandleSelection()
    {
        var ray = GameCamera.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit))
        {
            //the collider could be children of the unit, so we make sure to check in the parent
            var vehicle = hit.collider.GetComponentInParent<Vehicle>();
            m_Selected = vehicle;
       
        }
    }

    public void HandleAction()
    {
        var ray = GameCamera.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit))
        {
            
            var stand = hit.collider.GetComponentInParent<Stand>();

            if (stand != null)
            {
                m_Selected.GoTo(stand);
            }
            else
            {
               
            }
            m_Selected.GoTo(hit.point);
        }
    }

    private void Update()
    {
        Vector2 move = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
        GameCamera.transform.position = GameCamera.transform.position + new Vector3(move.y, 0, -move.x) * PanSpeed * Time.deltaTime;
        //left click
        if (Input.GetMouseButtonDown(0))
        {
            HandleSelection();
        }
        else if (m_Selected != null && Input.GetMouseButtonDown(1))
        {//right click give order to the unit
            HandleAction();
        }

        MarkerHandling();
    }

    // Handle displaying the marker above the unit that is currently selected (or hiding it if no unit is selected)
    void MarkerHandling()
    {
        if (m_Selected == null && Marker.activeInHierarchy)
        {
            Marker.SetActive(false);
            Marker.transform.SetParent(null);
        }
        else if (m_Selected != null && Marker.transform.parent != m_Selected.transform)
        {
            Marker.SetActive(true);
            Marker.transform.SetParent(m_Selected.transform, false);
            Marker.transform.localPosition = Vector3.zero;
        }
    }
}
