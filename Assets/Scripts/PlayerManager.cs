using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;
using System.Collections;
using NUnit.Framework;
using System.Collections.Generic;
using Unity.Cinemachine;
using UnityEngine.UI;

public class PlayerManager : MonoBehaviour
{
    public int totalPlayers = 0;
    public List<GameObject> players;

    public UnityEvent<GameObject> OnPlayerJoin;


    public Transform[] spawnPoints;

    public Image[] scorePanels; 

    public static PlayerManager Instance;


    private void Awake()
    {
        Instance = this;
    }

    public void PlayerJoin(PlayerInput input)
    {
        input.gameObject.GetComponent<SplitScreenCamera>().index = totalPlayers;
        totalPlayers++;
        players.Add(input.gameObject);

        OnPlayerJoin.Invoke(input.gameObject);

        input.gameObject.transform.position = spawnPoints[totalPlayers-1].position;

        Color temp = new Color(
            Random.value,
            Random.value,
            Random.value
        );

        input.gameObject.GetComponent<ColourManager>().SetColour(temp);

        scorePanels[totalPlayers-1].color = temp;


        foreach (var player in players)
        {
            SplitScreenCamera tempCamObject = player.GetComponent<SplitScreenCamera>();

            tempCamObject.totalPlayers = totalPlayers;
            tempCamObject.SetupCamera();

            switch (tempCamObject.index)
            { 
                case 0:
                    tempCamObject.GetComponentInChildren<CinemachineBrain>().ChannelMask = OutputChannels.Channel01;
                    tempCamObject.GetComponentInChildren<CinemachineCamera>().OutputChannel = OutputChannels.Channel01;
                    break;
                case 1:
                    tempCamObject.GetComponentInChildren<CinemachineBrain>().ChannelMask = OutputChannels.Channel02;
                    tempCamObject.GetComponentInChildren<CinemachineCamera>().OutputChannel = OutputChannels.Channel02;
                    break;
                case 2:
                    tempCamObject.GetComponentInChildren<CinemachineBrain>().ChannelMask = OutputChannels.Channel03;
                    tempCamObject.GetComponentInChildren<CinemachineCamera>().OutputChannel = OutputChannels.Channel03;
                    break;
                case 4:
                    tempCamObject.GetComponentInChildren<CinemachineBrain>().ChannelMask = OutputChannels.Channel03;
                    tempCamObject.GetComponentInChildren<CinemachineCamera>().OutputChannel = OutputChannels.Channel03;
                    break;
            }
        }
    }
}
