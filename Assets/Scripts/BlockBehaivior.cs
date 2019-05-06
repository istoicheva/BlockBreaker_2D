using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockBehaivior : MonoBehaviour
{

    [SerializeField] AudioClip destroyAudio;
    [SerializeField] int blockPointsDestoryed = 12;

    Level level;
    
    private void Start()
    {
        level = FindObjectOfType<Level>();
        level.CountBreakableBlocks();           // Counts breakable blocks for this level
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        DestoryBlock();
    }

    private void DestoryBlock()
    {
        FindObjectOfType<GameSession>().AddToScore(blockPointsDestoryed);
        
        AudioSource.PlayClipAtPoint(destroyAudio, Camera.main.transform.position);      // Play sound on the Main Carama position
        level.BlockIsDestoryed();               // Remove breakable block from level

        Destroy(gameObject);                                                            // Destory block
    }
}
