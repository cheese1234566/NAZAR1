using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimator : MonoBehaviour
{
    private const string IS_WALKING = "IsWalking";

    [SerializeField] private Player player;

    private Animator animor;

    private void Awake()
    {
        animor = GetComponent<Animator>();
    }

    private void Update()
    {
        animor.SetBool(IS_WALKING, player.IsWalking());
    }
}
