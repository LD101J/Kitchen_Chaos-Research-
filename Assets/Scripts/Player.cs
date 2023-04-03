using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System; 

namespace Code_Monkey_Kitchen_Chaos
{
    public class Player : MonoBehaviour
    {
        #region Variables
        [SerializeField] private float moveSpeed;
        private bool isWalking;
        private Vector3 lastInterectDirection;
        [SerializeField] private Game_Input gameInput;
        [SerializeField] private LayerMask counterLayer;
        private Clear_Counter selectedCounter;
        public event EventHandler OnSelectedCounterChanged;
        #endregion
        public class OnSelectedCounterChangedEventArgs : EventArgs
        {

        }
        private void Start()
        {
            gameInput.OnInteractAction += GameInput_OnInteractAction;
        }

        private void GameInput_OnInteractAction(object sender, System.EventArgs e)
        {
            if(selectedCounter != null)
            {
                selectedCounter.Interact();
            }
        }

        private void Update()
        {
            HandleMovement();
            HandleInteractions();
        }
        public bool IsWalking()
        {
            return isWalking;
        }
        private void HandleMovement()
        {
            Vector2 inputVector = gameInput.GetMovementVectorNormalized();
            float rotateSpeed = 10f;
            Vector3 moveDirection = new Vector3(inputVector.x, 0f, inputVector.y);

            float moveDistance = moveSpeed * Time.deltaTime;
            float playerRadius = .7f;
            float playerHeight = 2f;
            bool canMove = !Physics.CapsuleCast(transform.position, transform.position + Vector3.up * playerHeight, playerRadius, moveDirection, moveDistance);
            if (!canMove)
            {
                Vector3 moveDirectionX = new Vector3(moveDirection.x, 0, 0).normalized;
                canMove = !Physics.CapsuleCast(transform.position, transform.position + Vector3.up * playerHeight, playerRadius, moveDirectionX, moveDistance);

                if (canMove)
                {
                    moveDirection = moveDirectionX;
                }
                else
                {
                    Vector3 moveDirectionZ = new Vector3(0, 0, moveDirection.z).normalized;
                    canMove = !Physics.CapsuleCast(transform.position, transform.position + Vector3.up * playerHeight, playerRadius, moveDirectionZ, moveDistance);

                    if (canMove)
                    {
                        moveDirection = moveDirectionZ;
                    }
                }
            }

            if (canMove)
            {
                transform.position += moveDirection * moveDistance;
            }
            isWalking = moveDirection != Vector3.zero;

            transform.forward = Vector3.Slerp(transform.forward, moveDirection, Time.deltaTime * rotateSpeed);
        }
        private void HandleInteractions()
        {

        }
    }

}
