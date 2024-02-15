using System.Collections;
using System.Collections.Generic;
using UnityEditor.UIElements;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.XR;

public class GameMenuManager : MonoBehaviour
{
    [SerializeField] Transform head;
    [SerializeField] float spawnDistance = 2f;
    [SerializeField] float funnySpawnDistance = 1f;
    [SerializeField] GameObject menu;
    [SerializeField] GameObject funnyMenu;
    [SerializeField] InputActionProperty rShowButton;
    [SerializeField] InputActionProperty lShowButton;


    void Update()
    {
        if(rShowButton.action.WasPressedThisFrame())
        {
            menu.SetActive(!menu.activeSelf);

            menu.transform.position = head.position + new Vector3(head.forward.x, 0, head.forward.z).normalized * spawnDistance;
        }

        menu.transform.LookAt(new Vector3(head.position.x, head.position.y, head.position.z));
        menu.transform.forward *= -1;

        if (lShowButton.action.WasPressedThisFrame())
        {
            funnyMenu.SetActive(!funnyMenu.activeSelf);

            funnyMenu.transform.position = head.position + new Vector3(head.forward.x, 0, head.forward.z).normalized * funnySpawnDistance;
        }

        funnyMenu.transform.LookAt(new Vector3(head.position.x, head.position.y, head.position.z));
        funnyMenu.transform.forward *= -1;
    }

}
