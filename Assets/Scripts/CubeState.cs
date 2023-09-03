using System.Collections.Generic;
using UnityEngine;

public static class CubeState
{
    public static readonly Dictionary<CubeRotationState, Quaternion> Rotation = new Dictionary<CubeRotationState, Quaternion>
    {
        { CubeRotationState.Down,  Quaternion.Euler(-135, 0, 0) },
        { CubeRotationState.Front,  Quaternion.Euler(-45, 0, 0) },
        { CubeRotationState.Up,  Quaternion.Euler(45, 0, 0) },
        { CubeRotationState.Back,  Quaternion.Euler(135, 0, 0) },
    };

    public static CubeRotationState GetNextRotationState(CubeRotationState state)
    {
        return (CubeRotationState)(((int)state + 1) % 4);
    }

    public static CubeRotationState GetPreviousRotationState(CubeRotationState state)
    {
        return (CubeRotationState)(((int)state + 3) % 4);
    }

    public static CubeRotationState GenerateRandomRotationState()
    {
        int[] states = { 0, 2, 3 };
        return (CubeRotationState)states[Random.Range(0, 3)];
    }
}