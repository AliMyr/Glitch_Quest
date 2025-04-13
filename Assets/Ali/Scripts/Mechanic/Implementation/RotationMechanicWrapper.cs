using UnityEngine;

public class RotationMechanicWrapper : IMechanic, IUpdatableMechanic
{
    private Character character;
    private CameraService cameraService;
    private float mouseSensitivity = 2.0f;
    private bool isActive = false;

    public void Initialize(Character character)
    {
        this.character = character;
        cameraService = CameraService.Instance;

        if (LevelManager.Instance != null && LevelManager.Instance.CurrentLevel >= 2)
        {
            isActive = true;
            if (character?.RotationComponent != null)
                character.RotationComponent.IsRotationEnabled = true;
        }
    }

    public void Enable()
    {
        if (character?.RotationComponent != null)
            character.RotationComponent.IsRotationEnabled = true;
        isActive = true;
    }

    public void Disable()
    {
        if (character?.RotationComponent != null)
            character.RotationComponent.IsRotationEnabled = false;
        isActive = false;
    }

    public void Update()
    {
        if (!isActive)
            return;

        if (LevelManager.Instance != null && LevelManager.Instance.CurrentLevel < 2)
            return;

        if (cameraService == null || character == null || !character.RotationComponent.IsRotationEnabled)
            return;

        var inputService = GameManager.Instance.InputService;
        if (inputService == null)
            return;

        if (!Application.isMobilePlatform)
        {
            Vector2 delta = inputService.RotationDelta;
            if (delta.sqrMagnitude < 0.0001f)
                return;

            Vector2 scaledDelta = delta * mouseSensitivity;
            cameraService.AddRotationDelta(scaledDelta);
        }
    }
}
