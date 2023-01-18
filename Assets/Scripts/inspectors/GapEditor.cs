using Assets.Scripts.platforms;
using Assets.Scripts.utils;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(Gap))]
[CanEditMultipleObjects]
public class MyPlayerEditor : Editor
{
    private SerializedProperty distance;

    private void OnEnable()
    {
        distance = serializedObject.FindProperty("distance");
        distance = serializedObject.FindProperty("left");
        distance = serializedObject.FindProperty("right");
    }

    public override void OnInspectorGUI()
    {
        _ = DrawDefaultInspector();

        serializedObject.Update();

        if (GUILayout.Button("Set Distance"))
        {
            UpdatePlatformsPosition();
        }

        _ = serializedObject.ApplyModifiedProperties();
    }

    private void UpdatePlatformsPosition()
    {
        Gap gap = target as Gap;

        EdgeCollider2D colliderLeft = gap.left;
        Vector3 leftPos = gap.left.transform.position;
        EdgeCollider2D colliderRight = gap.right;
        Vector3 rightPos = gap.right.transform.position;

        Vector3 rightestPointFromLeftRelative = Geometry.GetExtremePoint(colliderLeft.points, Geometry.Position.right);
        Vector3 rightest = leftPos + rightestPointFromLeftRelative;
        Vector3 leftestPointFromRightRelative = Geometry.GetExtremePoint(colliderRight.points, Geometry.Position.left);
        Vector3 leftest = rightPos + leftestPointFromRightRelative;

        Vector2 currentDistanceBetweenExtremes = leftest - rightest;

        colliderRight.transform.position = new(rightPos.x - currentDistanceBetweenExtremes.x + gap.distanceX, rightPos.y - currentDistanceBetweenExtremes.y + gap.distanceY, rightPos.z);
    }
}