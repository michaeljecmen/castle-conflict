using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(BezierPath))]
public class DrawBezier : Editor {
    private void OnSceneViewGUI(SceneView sv) {
        BezierPath be = target as BezierPath;

        be.p0 = Handles.PositionHandle(be.p0, Quaternion.identity);
        be.p3 = Handles.PositionHandle(be.p3, Quaternion.identity);
        be.p1 = Handles.PositionHandle(be.p1, Quaternion.identity);
        be.p2 = Handles.PositionHandle(be.p2, Quaternion.identity);

        Handles.DrawBezier(be.p0, be.p3, be.p1, be.p2, Color.red, null, 2f);
    }

    void OnEnable() {
        SceneView.onSceneGUIDelegate += OnSceneViewGUI;
    }

    void OnDisable() {
        SceneView.onSceneGUIDelegate -= OnSceneViewGUI;
    }
}