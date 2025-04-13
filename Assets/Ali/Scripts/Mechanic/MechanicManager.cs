using System.Collections.Generic;
using UnityEngine;

public class MechanicManager
{
    private readonly Dictionary<int, List<IMechanic>> levelMechanics = new();
    private readonly List<IMechanic> activeMechanics = new();
    private Character character;

    public void Initialize(Character character)
    {
        this.character = character;
        if (character == null)
        {
            Debug.LogError("Character is null in MechanicManager");
        }
    }

    public void RegisterMechanic(int level, IMechanic mechanic)
    {
        if (mechanic == null)
        {
            Debug.LogError("Mechanic is null in RegisterMechanic");
            return;
        }

        if (!levelMechanics.ContainsKey(level))
            levelMechanics[level] = new List<IMechanic>();

        levelMechanics[level].Add(mechanic);
        Debug.Log($"Mechanic {mechanic.GetType().Name} registered for level {level}");
    }

    public void ActivateMechanicsForLevel(int level)
    {
        if (character == null)
        {
            Debug.LogError("Character is null in ActivateMechanicsForLevel");
            return;
        }

        if (!levelMechanics.TryGetValue(level, out var mechanics))
        {
            Debug.LogWarning($"MechanicManager: No mechanics registered for level {level}");
            return;
        }

        foreach (var mechanic in mechanics)
        {
            if (mechanic == null)
            {
                Debug.LogError("Mechanic is null in ActivateMechanicsForLevel");
                continue;
            }

            if (activeMechanics.Contains(mechanic))
                continue;

            mechanic.Initialize(character);
            mechanic.Enable();
            activeMechanics.Add(mechanic);
        }
    }

    public void UpdateMechanics()
    {
        if (character == null) return;

        foreach (var mechanic in activeMechanics)
        {
            if (mechanic == null) continue;

            if (mechanic is IUpdatableMechanic updatable)
                updatable.Update();
        }
    }

    public void ClearMechanics()
    {
        foreach (var mechanic in activeMechanics)
        {
            if (mechanic != null)
            mechanic.Disable();
        }
        activeMechanics.Clear();
        Debug.Log("MechanicManager: All mechanics cleared");
    }
}