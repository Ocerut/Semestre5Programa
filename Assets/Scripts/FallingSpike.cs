using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Falling : MonoBehaviour
{
    [SerializeField] private float detectRange;
    [SerializeField] private GameObject stalactite;
    [SerializeField] private GameObject stalaPrefab;
    private bool hasSpawned;
    private Vector2 stalactPos;

    // Start is called before the first frame update
    void Start()
    {
        stalactPos = new Vector2(stalactite.transform.position.x, stalactite.transform.position.y);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Destroy(stalactite);
        if (hasSpawned == false)
        {
          Instantiate(stalaPrefab, stalactPos, Quaternion.identity);
          hasSpawned = true;
        }
        
    }
}
