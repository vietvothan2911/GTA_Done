using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MinimapPolice : MonoBehaviour
{
    [SerializeField]
    private GameObject minimapPolice;

    public void OnWanter()
    {
        minimapPolice.SetActive(true);
        minimapPolice.transform.localScale = Vector3.one * 10;
    }

    public void EndWanter()
    {
        minimapPolice.SetActive(false);
        
    }

    public void ONMinimap()
    {
        minimapPolice.transform.localScale = Vector3.one * 30;
    }
}
