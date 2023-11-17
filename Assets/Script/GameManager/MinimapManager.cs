using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MinimapManager : MonoBehaviour
{
    [SerializeField]
    private RawImage bgMinimap;

    [SerializeField]
    private RawImage minimapUi;

    [SerializeField]
    private bl_MiniMap bl_MiniMap;

    //[SerializeField]
    //private GameObject gameUi, pauseUi;

    [SerializeField]
    private Camera cameraMinimap;

    [SerializeField]
    private GameObject player, minimapPoint;
    public float size;

    [SerializeField]
    private GameObject minimapRig;

    public void Start()
    {
        OnMinimapUi();
    }

    public void OnBgMiniMap()
    {
        //gameUi.SetActive(false);
        //pauseUi.SetActive(true);
        bl_MiniMap.DefaultHeight = 1200;
        cameraMinimap.orthographicSize = 1200;
        bl_MiniMap.Target = minimapPoint.transform;

        minimapRig.transform.rotation = Quaternion.Euler(90, 0, 0);
        //bl_MiniMap.miniMapCamera.transform.position = minimapPoint.transform.position - Vector3.up * 200;

        //Time.timeScale = 0;
    }

    public void OnMinimapUi()
    {
        //gameUi.SetActive(true);
        //pauseUi.SetActive(false);
        bl_MiniMap.DefaultHeight = size;
        cameraMinimap.orthographicSize = size;
        //Time.timeScale = 1;
        bl_MiniMap.Target = player.transform;
        //bl_MiniMap.miniMapCamera.transform.position = player.transform.position + Vector3.up * 360;
    }


}
