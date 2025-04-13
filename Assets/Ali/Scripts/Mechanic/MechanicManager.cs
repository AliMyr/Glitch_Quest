using System.Collections.Generic;
using UnityEngine;

public class MechanicManager
{
    private readonly Dictionary<int, List<IMechanic>> levelMechanics = new();
    private readonly List<IMechanic> activeMechanics = new();
    private Character character;
    private int highestActivatedLevel = 0;

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

        ClearMechanics();

        for (int i = 1; i <= level; i++)
        {
            if (levelMechanics.TryGetValue(i, out var mechanics))
            {
                foreach (var mechanic in mechanics)
                {
                    if (mechanic == null) continue;

                    mechanic.Initialize(character);
                    mechanic.Enable();
                    activeMechanics.Add(mechanic);
                    Debug.Log($"Activated mechanic: {mechanic.GetType().Name} for level {i}");
                }
            }
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
            {
                mechanic.Disable();
            }
        }
        activeMechanics.Clear();
        highestActivatedLevel = 0;
        Debug.Log("MechanicManager: All mechanics cleared");
    }
}