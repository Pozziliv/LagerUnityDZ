using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndTabs : MonoBehaviour, IEventable
{
    [SerializeField] private GameObject _winTab, _loseTab;

    private void OpenWinTab()
    {
        _winTab.SetActive(true);
    }

    private void OpenLoseTab()
    {
        _loseTab.SetActive(true);
    }

    public void OnEnable()
    {
        VideoCollecting.OnAllVideo += OpenWinTab;
        PlayerMovement.Caught += OpenLoseTab;
    }

    public void OnDisable()
    {
        VideoCollecting.OnAllVideo -= OpenWinTab;
        PlayerMovement.Caught -= OpenLoseTab;
    }
}
