using UnityEngine;
#if ENABLE_INPUT_SYSTEM
using UnityEngine.InputSystem;
#endif

namespace StarterAssets
{
    public class StarterAssetsInputs : MonoBehaviour
    {
        [Header("Character Input Values")]
        public Vector2 move;
        public Vector2 look;
        public bool jump;
        public bool sprint;
        public bool shoot;
        public bool dash;
        public bool zoom;
        public bool switchWeapon1;
        public bool switchWeapon2;
        public bool switchWeapon3;
        public bool switchWeapon4;

        [Header("Movement Settings")]
        public bool analogMovement;

        [Header("Mouse Cursor Settings")]
        public bool cursorLocked = true;
        public bool cursorInputForLook = true;

#if ENABLE_INPUT_SYSTEM
        public void OnMove(InputValue value)
        {
            MoveInput(value.Get<Vector2>());
        }

        public void OnLook(InputValue value)
        {
            if (cursorInputForLook)
            {
                LookInput(value.Get<Vector2>());
            }
        }

        public void OnJump(InputValue value)
        {
            JumpInput(value.isPressed);
        }

        public void OnSprint(InputValue value)
        {
            SprintInput(value.isPressed);
        }

        public void OnShoot(InputValue value)
        {
            ShootInput(value.isPressed);
        }
        public void OnZoom(InputValue value)
        {
            ZoomInput(value.isPressed);
        }
        public void OnSwitchWeapon1(InputValue value)
        {
            SwitchWeapon1Input(value.isPressed);
        }
        public void OnSwitchWeapon2(InputValue value)
        {
            SwitchWeapon2Input(value.isPressed);
        }
        public void OnSwitchWeapon3(InputValue value)
        {
            SwitchWeapon3Input(value.isPressed);
        }
        public void OnSwitchWeapon4(InputValue value)
        {
            SwitchWeapon4Input(value.isPressed);
        }


#endif


        public void MoveInput(Vector2 newMoveDirection)
        {
            move = newMoveDirection;
        }

        public void LookInput(Vector2 newLookDirection)
        {
            look = newLookDirection;
        }

        public void JumpInput(bool newJumpState)
        {
            jump = newJumpState;
        }
        public void SwitchWeapon1Input(bool newWeaponState)
        {
            switchWeapon1 = newWeaponState;
        }
        public void SwitchWeapon2Input(bool newWeaponState)
        {
            switchWeapon2 = newWeaponState;
        }
        public void SwitchWeapon3Input(bool newWeaponState)
        {
            switchWeapon3 = newWeaponState;
        }
        public void SwitchWeapon4Input(bool newWeaponState)
        {
            switchWeapon4 = newWeaponState;
        }

        public void SprintInput(bool newSprintState)
        {
            sprint = newSprintState;
        }

        public void ShootInput(bool newShootState)
        {
            shoot = newShootState;
        }
        public void ZoomInput(bool newZoomState)
        {
            zoom = newZoomState;
        }
        public void DashInput(bool newDashState)
        {
            dash = newDashState;
        }

        private void OnApplicationFocus(bool hasFocus)
        {
            SetCursorState(cursorLocked);
        }

        private void SetCursorState(bool newState)
        {
            Cursor.lockState = newState ? CursorLockMode.Locked : CursorLockMode.None;
        }
    }

}