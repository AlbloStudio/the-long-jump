using Assets.Scripts.managers;
using UnityEditor;
using UnityEngine;

public class Developer : MonoBehaviour
{
    [MenuItem("Developer / Place player on clicked position")]
    private static void PlacePlayerOnClick()
    {
        SceneView.duringSceneGui += OnSceneGUI;
    }

    [MenuItem("Developer / Put player back")]
    private static void PutPlayerBack()
    {
        Undo.RecordObject(GeneralData.Instance.Player.transform, "Put player back");
        GeneralData.Instance.Player.transform.position = new Vector3(2, 10, 0);
    }

    private static void OnSceneGUI(SceneView sceneView)
    {
        Event e = Event.current;

        if (e.type == EventType.MouseDown)
        {
            Vector3 distanceFromCam = new(Camera.main.transform.position.x, Camera.main.transform.position.y, 0);
            Plane plane = new(Vector3.forward, distanceFromCam);

            Ray ray = HandleUtility.GUIPointToWorldRay(Event.current.mousePosition);
            if (plane.Raycast(ray, out float enter))
            {
                Vector3 p = ray.GetPoint(enter);
                Undo.RecordObject(GeneralData.Instance.Player.transform, "Place player on clicked position");

                GeneralData.Instance.Player.transform.position = new Vector3(p.x, p.y, 0);
                Selection.objects = new GameObject[] { GeneralData.Instance.Player.gameObject };
            }

            SceneView.duringSceneGui -= OnSceneGUI;
        }
    }
}