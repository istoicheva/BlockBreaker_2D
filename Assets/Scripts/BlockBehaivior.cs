using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockBehaivior : MonoBehaviour
{

    [SerializeField] AudioClip destroyAudio;
    [SerializeField] int blockPointsDestoryed = 12;
    [SerializeField] Sprite[] damageSprites;
    [SerializeField] int maxHits = 3;

    Level level;

    [SerializeField] int hitTimes;
    
    private void Start()
    {
        CountBreakableBlocks();
    }

    private void CountBreakableBlocks()
    {
        level = FindObjectOfType<Level>();
        if (tag == "Breakable")
        {
            level.CountBlock();           // Counts breakable blocks for this level
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (tag == "Breakable")
        {
            HandleHit();
        }
    }

    private void HandleHit()
    {
        hitTimes++;
        if (hitTimes >= maxHits)
        {
            DestoryBlock();  
        }
        else
        {
            ShowNextHitSprite();
        }
    }

    private void ShowNextHitSprite()
    {
        int spriteIndex = hitTimes - 1;
        if (damageSprites[spriteIndex] != null)
        {
            GetComponent<SpriteRenderer>().sprite = damageSprites[spriteIndex];
        }
        else
        {
            Debug.LogError("Block sprite is missing from array!");
        }
    }

    private void DestoryBlock()
    {
        FindObjectOfType<GameSession>().AddToScore(blockPointsDestoryed);
        
        AudioSource.PlayClipAtPoint(destroyAudio, Camera.main.transform.position);      // Play sound on the Main Carama position
        level.BlockIsDestoryed();               // Remove breakable block from level

        Destroy(gameObject);                                                            // Destory block
    }

    public int GetTimesHit()
    {
        return hitTimes;
    }

    public int GetMaxHits()
    {
        return maxHits;
    }
}
