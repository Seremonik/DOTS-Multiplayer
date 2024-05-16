using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Features.ServerConnection.Scripts;
using Features.ServerConnection.Scripts.Client;
using TMPro;
using Unity.Entities;
using Unity.NetCode;
using Unity.Networking.Transport;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ClientConnectionManager : MonoBehaviour
{
    [SerializeField]
    private TMP_InputField addressField;
    [SerializeField]
    private TMP_InputField portField;
    [SerializeField]
    private TMP_Dropdown connectionModeDropdown;
    [SerializeField]
    private TMP_Dropdown teamDropdown;
    [SerializeField]
    private Button connectButton;

    private ushort Port => ushort.Parse(portField.text);
    private string Address => addressField.text;

    private void OnEnable()
    {
        connectionModeDropdown.onValueChanged.AddListener(OnConnectionModeChanged);
        connectButton.onClick.AddListener(OnButtonClicked);
        OnConnectionModeChanged(connectionModeDropdown.value);
    }

    private void OnDisable()
    {
        connectionModeDropdown.onValueChanged.RemoveAllListeners();
        connectButton.onClick.RemoveAllListeners();
    }

    private void OnButtonClicked()
    {
        DestroySimulationWorld();
        SceneManager.LoadScene(1);

        switch (connectionModeDropdown.value)
        {
            case 0:
                StartServer();
                StartClient();
                break;
            case 1:
                StartServer();
                break;
            case 2:
                StartClient();
                break;
        }
    }

    private void StartClient()
    {
        var clientWorld = ClientServerBootstrap.CreateClientWorld("Client World");
        var connectionEndpoint = NetworkEndpoint.Parse(Address, Port);
        {
            using var networkDriverQuery =
                clientWorld.EntityManager.CreateEntityQuery(ComponentType.ReadWrite<NetworkStreamDriver>());
            networkDriverQuery.GetSingletonRW<NetworkStreamDriver>().ValueRW.Connect(clientWorld.EntityManager, connectionEndpoint);
        }
        World.DefaultGameObjectInjectionWorld = clientWorld;

        var team = teamDropdown.value switch
        {
            0 => TeamType.AutoAssign,
            1 => TeamType.Blue,
            2 => TeamType.Red,
            _ => TeamType.None
        };

        var teamRequestEntity = clientWorld.EntityManager.CreateEntity();
        clientWorld.EntityManager.AddComponentData(teamRequestEntity, new ClientTeamRequest()
        {
            Value = team
        });
    }

    private void StartServer()
    {
        var serverWorld = ClientServerBootstrap.CreateServerWorld("Server World");
        var serverEndpoint = NetworkEndpoint.AnyIpv4.WithPort(Port);
        {
            using var networkDriverQuery =
                serverWorld.EntityManager.CreateEntityQuery(ComponentType.ReadWrite<NetworkStreamDriver>());
            networkDriverQuery.GetSingletonRW<NetworkStreamDriver>().ValueRW.Listen(serverEndpoint);
        }
    }

    private void DestroySimulationWorld()
    {
        foreach (var world in World.All)
        {
            if (world.Flags == WorldFlags.Game)
            {
                world.Dispose();
                break;
            }
        }
    }

    private void OnConnectionModeChanged(int connectionMode)
    {
        string buttonLabel;
        connectButton.enabled = true;

        switch (connectionMode)
        {
            case 0:
                buttonLabel = "Start Host";
                break;
            case 1:
                buttonLabel = "Start Server";
                break;
            case 2:
                buttonLabel = "Start Client";
                break;
            default:
                buttonLabel = "ERROR";
                break;
        }

        connectButton.GetComponentInChildren<TextMeshProUGUI>().text = buttonLabel;
    }
}