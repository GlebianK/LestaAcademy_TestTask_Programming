using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloseButton : MonoBehaviour
{
    //[SerializeField] private GameObject parentPanel;

    public void OnClickClose(GameObject parentPanel)
    {
        parentPanel.SetActive(false);
    }
}