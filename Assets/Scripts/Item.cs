using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Fusion;

public class Item : NetworkBehaviour
{
    [Networked] public NetworkBool IsCollected { get; set; }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player" && other.GetComponent<NetworkObject>().HasInputAuthority)
        {
            if (IsCollected) return;
            PlayerRef localPlayer = other.GetComponent<NetworkObject>().InputAuthority;
            RPC_Owner(localPlayer);
        }
    }
    [Rpc(RpcSources.All, RpcTargets.StateAuthority)]
    public void RPC_Owner(PlayerRef collector)
    {
        if (IsCollected) return;
        IsCollected = true;
        NetworkObject playerObj = Runner.GetPlayerObject(collector);
        if (playerObj != null)
        {
            MovementController player = playerObj.GetComponent<MovementController>();
            if (player != null)
            {
                player.RPC_AddScore(1);
            }
        }
        Runner.Despawn(Object);
    }


}
