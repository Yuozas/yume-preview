using UnityEngine;

#if UNITY_EDITOR
public static class OnDrawGizmosUtility
{
    public static void Draw(Vector3 center, float distance, Vector2 direction)
    {
        var halfDistance = distance * 0.5f;

        var left = Vector3.right * -direction.y + Vector3.up * direction.x;
        var right = -left;

        var start = center + (left * halfDistance);
        var end = center + (right * halfDistance);

        Gizmos.DrawLine(start, end);

        const int count = 6;
        const float length = 0.5f;

        var spacing = distance / (count - 1);

        for (int i = 0; i < count; i++)
        {
            Gizmos.DrawRay(start, direction.normalized * length);
            start += right * spacing;
        }
    }
}
#endif