using UnityEngine;
using Unity.Netcode;
using System;

public class PlayerNetworked : NetworkBehaviour
{
    public NetworkVariable<Vector3> Position = new NetworkVariable<Vector3>();

    public override void OnNetworkSpawn()
    {
        if (IsOwner)
        {
            Move();
        }
    }

    public void Move()
    {
        if (NetworkManager.Singleton.IsServer)
        {
            Vector3 tempPos = transform.position - Vector3.up * 20;
            transform.position = tempPos;
            Position.Value = tempPos;
        }
        else
        {
            SubmitPositionRequestServerRpc();
        }
    }

    [ServerRpc]
    void SubmitPositionRequestServerRpc(ServerRpcParams rpcParams = default)
    {
        Position.Value = Position.Value + Vector3.up * 20;

    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            SubmitPositionRequestServerRpc();
        }
        transform.position = Position.Value;
    }

}
