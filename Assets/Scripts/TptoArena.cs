using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TptoArena : MonoBehaviour
{
    public Transform pontoDestino;
    public GameObject player;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject == player)
        {
            Tp();
        }
    }
    private void Tp()
    {
        player.transform.position = pontoDestino.position;
    }
}
