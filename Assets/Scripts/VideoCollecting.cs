using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class VideoCollecting : MonoBehaviour, IEventable
{
    private float videoCount = 0;
    private TMP_Text _tmp;

    public delegate void AllVideo();
    public static event AllVideo? OnAllVideo;

    private void Start()
    {
        _tmp = GetComponent<TMP_Text>();
    }

    private void AddVideo(Collectables video)
    {
        videoCount++;
        _tmp.text = $"Видео собрано: {videoCount}/5";
        if (videoCount == 5)
            OnAllVideo.Invoke();
    }

    public void OnEnable()
    {
        PlayerMovement.Geting += AddVideo;
    }

    public void OnDisable()
    {
        PlayerMovement.Geting -= AddVideo;
    }

}
