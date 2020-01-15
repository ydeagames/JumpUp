using System;
using UnityEngine;
using Object = UnityEngine.Object;

namespace UnityEditor.Tilemaps
{
    /// <summary>
    /// This Brush instances and places a randomly selected Prefabs onto the targeted location and parents the instanced object to the paint target. Use this as an example to quickly place an assorted type of GameObjects onto structured locations.
    /// </summary>
    [CreateAssetMenu(fileName = "Custom Prefab brush", menuName = "Brushes/Custom Prefab brush")]
    [CustomGridBrush(false, true, false, "Custom Prefab Brush")]
    public class CustomPrefabBrush : GridBrush
    {
        /// <summary>
        /// The selection of Prefabs to paint from
        /// </summary>
        public GameObject[] m_Prefabs;
        /// <summary>
        /// Anchor Point of the Instantiated Prefab in the cell when painting
        /// </summary>
        public Vector3 m_Anchor = new Vector3(0.5f, 0.5f, 0.5f);

        public int m_Index;

        private GameObject prev_brushTarget;
        private Vector3Int prev_position = Vector3Int.one * Int32.MaxValue;

        /// <summary>
        /// Paints Prefabs into a given position within the selected layers.
        /// The CustomPrefabBrush overrides this to provide Prefab painting functionality.
        /// </summary>
        /// <param name="gridLayout">Grid used for layout.</param>
        /// <param name="brushTarget">Target of the paint operation. By default the currently selected GameObject.</param>
        /// <param name="position">The coordinates of the cell to paint data to.</param>
        public override void Paint(GridLayout grid, GameObject brushTarget, Vector3Int position)
        {
            if (position == prev_position)
            {
                return;
            }
            prev_position = position;
            if (brushTarget)
            {
                prev_brushTarget = brushTarget;
            }
            brushTarget = prev_brushTarget;

            // Do not allow editing palettes
            if (brushTarget.layer == 31)
                return;

            int index = Mathf.Clamp(m_Index, 0, m_Prefabs.Length - 1);
            GameObject prefab = m_Prefabs[index];
            GameObject instance = (GameObject)PrefabUtility.InstantiatePrefab(prefab);
            if (instance != null)
            {
                Erase(grid, brushTarget, position);

                Undo.MoveGameObjectToScene(instance, brushTarget.scene, "Paint Prefabs");
                Undo.RegisterCreatedObjectUndo((Object)instance, "Paint Prefabs");
                instance.transform.SetParent(brushTarget.transform);
                instance.transform.position = grid.LocalToWorld(grid.CellToLocalInterpolated(position + m_Anchor));
            }
        }

        /// <summary>
        /// Erases Prefabs in a given position within the selected layers.
        /// The CustomPrefabBrush overrides this to provide Prefab erasing functionality.
        /// </summary>
        /// <param name="gridLayout">Grid used for layout.</param>
        /// <param name="brushTarget">Target of the erase operation. By default the currently selected GameObject.</param>
        /// <param name="position">The coordinates of the cell to erase data from.</param>
        public override void Erase(GridLayout grid, GameObject brushTarget, Vector3Int position)
        {
            if (brushTarget)
            {
                prev_brushTarget = brushTarget;
            }
            brushTarget = prev_brushTarget;
            // Do not allow editing palettes
            if (brushTarget.layer == 31)
                return;

            Transform erased = GetObjectInCell(grid, brushTarget.transform, position);
            if (erased != null)
                Undo.DestroyObjectImmediate(erased.gameObject);
        }

        private static Transform GetObjectInCell(GridLayout grid, Transform parent, Vector3Int position)
        {
            int childCount = parent.childCount;
            for (int i = 0; i < childCount; i++)
            {
                Transform child = parent.GetChild(i);
                if (position == grid.WorldToCell(child.position))
                    return child;
            }
            return null;
        }

        private static float GetPerlinValue(Vector3Int position, float scale, float offset)
        {
            return Mathf.PerlinNoise((position.x + offset) * scale, (position.y + offset) * scale);
        }
    }

    /// <summary>
    /// The Brush Editor for a Prefab Brush.
    /// </summary>
    [CustomEditor(typeof(CustomPrefabBrush))]
    public class CustomPrefabBrushEditor : GridBrushEditor
    {
        private CustomPrefabBrush CustomPrefabBrush { get { return target as CustomPrefabBrush; } }

        private SerializedProperty m_Prefabs;
        private SerializedProperty m_Anchor;
        private SerializedObject m_SerializedObject;

        Vector2 prefabScroll;

        protected override void OnEnable()
        {
            base.OnEnable();
            m_SerializedObject = new SerializedObject(target);
            m_Prefabs = m_SerializedObject.FindProperty("m_Prefabs");
            m_Anchor = m_SerializedObject.FindProperty("m_Anchor");
        }

        /// <summary>
        /// Callback for painting the inspector GUI for the CustomPrefabBrush in the Tile Palette.
        /// The CustomPrefabBrush Editor overrides this to have a custom inspector for this Brush.
        /// </summary>
        public override void OnPaintInspectorGUI()
        {
            m_SerializedObject.UpdateIfRequiredOrScript();
            {
                // CustomPrefabBrush.m_Index = EditorGUILayout.IntSlider("Number", CustomPrefabBrush.m_Index, 0,
                //     CustomPrefabBrush.m_Prefabs.Length - 1);

                var buttonHeight = EditorGUIUtility.singleLineHeight * 2f;
                var heightStyle = GUILayout.Height(buttonHeight);

                var lastRect = GUILayoutUtility.GetLastRect();
                var scrollMouse = Event.current.mousePosition;
                scrollMouse.x -= lastRect.xMin - prefabScroll.x;
                scrollMouse.y -= lastRect.yMax - prefabScroll.y;

                prefabScroll = EditorGUILayout.BeginScrollView(prefabScroll);

                if (CustomPrefabBrush.m_Prefabs != null)
                {
                    for (int indexPrefab = 0; indexPrefab < CustomPrefabBrush.m_Prefabs.Length; indexPrefab++)
                    {
                        if (CustomPrefabBrush.m_Prefabs[indexPrefab] == null)
                            continue;

                        var rect = EditorGUILayout.GetControlRect(heightStyle);

                        var bgRect = rect;
                        bgRect.x -= 1f;
                        bgRect.y -= 1f;
                        bgRect.width += 2f;
                        bgRect.height += 2f;
                        if (indexPrefab == CustomPrefabBrush.m_Index)
                        {
                            EditorGUI.DrawRect(bgRect, new Color32(0x42, 0x80, 0xe4, 0xff));
                        }
                        {
                            EditorGUIUtility.AddCursorRect(bgRect, MouseCursor.Link);

                            if (bgRect.Contains(scrollMouse))
                            {
                                EditorGUI.DrawRect(bgRect, new Color32(0x42, 0x80, 0xe4, 0x40));
                                if (Event.current.type == EventType.MouseDown)
                                {
                                    CustomPrefabBrush.m_Index = indexPrefab;
                                    SceneView.RepaintAll();
                                }
                            }
                        }

                        var iconRect = new Rect(rect.x, rect.y, rect.height, rect.height);

                        var icon = AssetPreview.GetAssetPreview(CustomPrefabBrush.m_Prefabs[indexPrefab]);
                        if (icon != null)
                            GUI.DrawTexture(iconRect, icon, ScaleMode.ScaleToFit, true, 1f, Color.white, Vector4.zero, Vector4.one * 4f);
                        else
                            EditorGUI.DrawRect(iconRect, EditorStyles.label.normal.textColor * 0.25f);

                        var labelRect = rect;
                        labelRect.x += iconRect.width + 4f;
                        labelRect.width -= iconRect.width + 4f;
                        labelRect.height = EditorGUIUtility.singleLineHeight;
                        labelRect.y += (buttonHeight - labelRect.height) * 0.5f;
                        var labelStyle = indexPrefab == CustomPrefabBrush.m_Index ? EditorStyles.whiteBoldLabel : EditorStyles.label;
                        GUI.Label(labelRect, CustomPrefabBrush.m_Prefabs[indexPrefab].name, labelStyle);
                    }
                }

                EditorGUILayout.EndScrollView();

                if (AssetPreview.IsLoadingAssetPreviews())
                    Repaint();
            }


            EditorGUILayout.PropertyField(m_Prefabs, true);
            EditorGUILayout.PropertyField(m_Anchor);
            m_SerializedObject.ApplyModifiedPropertiesWithoutUndo();
        }
    }
}