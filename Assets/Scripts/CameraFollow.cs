using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    private Player player;
    [SerializeField] private Vector3 offset;
    [SerializeField] private float xMin;
    [SerializeField] private float yMin;
    [SerializeField] private float xMax;
    [SerializeField] private float yMax;

    // Start is called before the first frame update
    void Start()
    {
        player = FindFirstObjectByType<Player>();
    }

    // Update is called once per frame
    void Update()
    {
        if (player.transform.position.x > xMin)
        {
            if (player.transform.position.x < xMax)
            {
                if (player.transform.position.y > yMin)
                {
                    if (player.transform.position.y < yMax)
                    {
                        transform.position = new Vector3(player.transform.position.x, player.transform.position.y, transform.position.z) + offset;
                    }
                    else if (player.transform.position.y >= yMax)
                    {
                        transform.position = new Vector3(player.transform.position.x, yMax, transform.position.z) + offset;
                    }
                }
                else if (player.transform.position.y <= yMin)
                {
                    transform.position = new Vector3(player.transform.position.x, yMin, transform.position.z) + offset;
                }
            }
            else if (player.transform.position.x >= xMax)
            {
                if (player.transform.position.y > yMin)
                {
                    if (player.transform.position.y < yMax)
                    {
                        transform.position = new Vector3(xMax, player.transform.position.y, transform.position.z) + offset;
                    }
                    else if (player.transform.position.y >= yMax)
                    {
                        transform.position = new Vector3(xMax, yMax, transform.position.z) + offset;
                    }
                }
                else if (player.transform.position.y <= yMin)
                {
                    transform.position = new Vector3(xMax, yMin, transform.position.z) + offset;
                }
            }
        }
        else if (player.transform.position.x <= xMin)
        {
            if (player.transform.position.y > yMin)
            {
                if (player.transform.position.y < yMax)
                {
                    transform.position = new Vector3(xMin, player.transform.position.y, transform.position.z) + offset;
                }
                else if (player.transform.position.y >= yMax)
                {
                    transform.position = new Vector3(xMin, yMax, transform.position.z) + offset;
                }
            }
            else if (player.transform.position.y <= yMin)
            {
                transform.position = new Vector3(xMin, yMin, transform.position.z) + offset;
            }
        }

    }
}
