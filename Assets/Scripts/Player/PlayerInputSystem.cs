using System.Collections;
using System.Collections.Generic;
using Unity.Entities;
using UnityEngine;
using UnityEngine.InputSystem;

[UpdateInGroup(typeof(InitializationSystemGroup), OrderLast = true)]
public partial class PlayerInputSystem : SystemBase
{
    private PlayerInput inputActions;
    private Entity player;

    protected override void OnCreate()
    {
        RequireForUpdate<PlayerTag>();
        RequireForUpdate<PlayerMoveInput>();
        inputActions = new PlayerInput();
    }

    protected override void OnStartRunning()
    {
        inputActions.Enable();
        inputActions.Gameplay.Shoot.performed += OnShoot;
        player = SystemAPI.GetSingletonEntity<PlayerTag>();
    }

    private void OnShoot(InputAction.CallbackContext context)
    {
        if (!SystemAPI.Exists(player))      
            return;
        
        SystemAPI.SetComponentEnabled<FireProjectileTag>(player, true);
    }

    protected override void OnUpdate()
    {
        Vector2 moveInput = inputActions.Gameplay.Movement.ReadValue<Vector2>();

        SystemAPI.SetSingleton(new PlayerMoveInput { value = moveInput });
    }

    protected override void OnStopRunning()
    {
        inputActions.Disable();
        player = Entity.Null;
    }
}