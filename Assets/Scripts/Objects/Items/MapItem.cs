using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapItem : BaseItem
{
    protected override void AddUpgrade()
    {
        var player = GameObject.FindGameObjectWithTag("Player");
        GameObject playerMap = player.transform.Find("HUD").Find("MapText").gameObject;
        playerMap.SetActive(true);
    }

}
