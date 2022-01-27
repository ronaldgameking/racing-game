using System;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;
using Unity.Collections;
using UnityUtils;
using System.Runtime.InteropServices;
using TMPro;

public class MessageProtocol : NetworkBehaviour
{
    public TMP_InputField MessageInput;
    public TextMeshProUGUI MessageDisplay;
    public NetworkList<char> MessageOut = new NetworkList<char>();
    public NetworkList<char> MessageIn = new NetworkList<char>();

    public MessageProtocol()
    {
        Logger.Level = Logger.DebugLevel.Verbose;
        Logger.LogVerbose("Constructor msg protocol called");
        if (!Application.isPlaying)
            Debug.LogError("CONSTRUCTOR WAS CALLED IN AN INVALID CONTEXT YOU CANNOT FIX THIS AS IT'S A UNITY ISSUE");
    }

    [ClientRpc]
    void ReceiveMessageClientRpc(ServerRpcParams rpcParams = default)
    {
        System.Text.StringBuilder sb = new System.Text.StringBuilder();
        for (int i = 0; i < MessageOut.Count; i++)
        {
            sb.Append(MessageOut[i]);
        }
        MessageDisplay.text = sb.ToString();
    }

    [ServerRpc]
    void SendMessageServerRpc(ServerRpcParams rpcParams = default)
    {
        for (int i = 0; i < MessageOut.Count; i++)
        {
            Logger.Log("Index " + MessageOut[i]);
        }
        ReceiveMessageClientRpc();
    }

    public override void OnNetworkSpawn()
    {
        MessageDisplay = FindObjectOfType<TextMeshProUGUI>();
        char[] chars = MessageDisplay.text.ToCharArray();
        for (int i = 0; i < MessageDisplay.text.Length; i++)
        {
            MessageOut.Add(chars[i]);
        }
        SendMessageServerRpc();
    }

    public void OnMessageChange()
    {
        if (MessageInput == null)
        {
            Logger.LogVerbose("Message input not assigned");
            return;
        }
        if (MessageInput.text == null)
        {
            Logger.LogVerbose("Input text null");
            return;
        }
        if (MessageIn == null)
        {
            Logger.LogWarning("NATIVE array was disposed, forcefully recreating...");
            MessageIn = new NetworkList<char>();
        }
        char[] chars = MessageInput.text.ToCharArray();
        if (chars == null)
        {
            Logger.LogWarning("INPUT EMPTY?");
            return;
        }
        for (int i = 0; i < MessageInput.text.Length; i++)
        {
            Logger.LogVerbose(MessageInput.text);
            MessageOut.Add(chars[i]);
        }
        SendMessageServerRpc();
    }
}
