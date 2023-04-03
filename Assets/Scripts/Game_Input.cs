using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

namespace Code_Monkey_Kitchen_Chaos
{
    public class Game_Input : MonoBehaviour
    {
        public event EventHandler OnInteractAction;

    private Player_Input_Actions playerInputActions;
        private void Awake()
        {
            playerInputActions = new Player_Input_Actions();
            playerInputActions.Player.Enable();

            playerInputActions.Player.Interact.performed += Interact_performed;
        }

        private void Interact_performed(UnityEngine.InputSystem.InputAction.CallbackContext obj)
        {
                OnInteractAction?.Invoke(this, EventArgs.Empty);
        }

        public Vector2 GetMovementVectorNormalized()
        {
            Vector2 inputVector = playerInputActions.Player.Move.ReadValue<Vector2>();

            inputVector = inputVector.normalized;
            return inputVector;
        }
    }

}
