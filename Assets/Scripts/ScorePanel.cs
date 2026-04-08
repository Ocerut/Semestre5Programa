using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Fusion;

public class ScorePanel : MonoBehaviour
{
    public Text screenTxt;

    void Update()
    {
        ScoreUpdate();
    }

    void ScoreUpdate()
    {
        string placar = "=== PLACAR ===\n";
        NetworkObject[] todosObjetos = FindObjectsOfType<NetworkObject>();
        int numeroPlayer = 1;

        foreach (NetworkObject networkObj in todosObjetos)
        {
            MovementController player = networkObj.GetComponent<MovementController>();

            if (player != null)
            {
                string marcador = networkObj.HasInputAuthority ? " (VOCÊ)" : "";
                placar += $"Player {numeroPlayer}{marcador}: {player.Score} pontos\n";
                numeroPlayer++;
            }
        }

        screenTxt.text = placar;
    }
}
