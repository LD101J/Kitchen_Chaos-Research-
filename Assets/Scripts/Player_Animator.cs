using System.Collections;
using System.Collections.Generic;
using UnityEngine;
 namespace Code_Monkey_Kitchen_Chaos
{
    public class Player_Animator : MonoBehaviour
    {
        private const string IS_WALKING = "IsWalking";
        [SerializeField] private Player player;
        private Animator animator;

        private void Awake()
        {
            animator = GetComponent<Animator>();
        }
        private void Update()
        {
            animator.SetBool(IS_WALKING, player.IsWalking());
        }
    }

}
