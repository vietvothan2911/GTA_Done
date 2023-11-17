using Newtonsoft.Json;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;

public class PlayerMenuSkill : MonoBehaviour
{
    public ShopSkinData skinData;

    [SerializeField]
    private string dataSkinName;

    [SerializeField]
    private List<ShopItem> shopItems;

    [SerializeField]
    private SkinnedMeshRenderer player, gunPlayer;

    public void Awake()
    {
      
        skinData = new ShopSkinData();

        if (PlayerPrefs.HasKey(dataSkinName))
        {
            string data = PlayerPrefs.GetString(dataSkinName);
            skinData = JsonConvert.DeserializeObject<ShopSkinData>(data);
        }
        else
        {
            skinData.MaterialPlayer = 0;

        }


        if (skinData.MaterialPlayer != 0)
        {
            Material[] mats = player.materials;

            mats[0] = shopItems[skinData.MaterialPlayer - 1].skin;
            player.materials = mats;

            Material[] gunmats = gunPlayer.materials;

            gunmats[0] = shopItems[skinData.MaterialPlayer - 1].skinGun;
            gunPlayer.materials = gunmats;
        }
    }

}
