using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSound : MonoBehaviour
{
    private Player player;
    private float footStepTimer;
    private float footStepTimerMax = 0.1F;

    private void Awake()
    {
        player = GetComponent<Player>();
    }

    private void Update()
    {
        footStepTimer -= Time.deltaTime;

        if (footStepTimer <= 0)
        {
            footStepTimer = footStepTimerMax;
            
            if (player.IsWalking())
            {
                Vector3 footPosition = player.transform.position - new Vector3(0, -1, 0);
                SoundManager.Instance.PlayFootstepsSound(footPosition);
            }
        }
    }
}
