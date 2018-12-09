using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace UnityEditor
{
    [CustomGridBrush(true, false, false, "Box Brush")]
    public class BoxBrush : GridBrush
    {
        public Vector3Int boxpos = Vector3Int.zero;
        public bool isActive = false;
        public int width = 10;
        public int height = 10;
        public override void Paint(GridLayout grid, GameObject brushTarget, Vector3Int position)
        {
            if (isActive)
            {

                for (int i = boxpos.x; i < boxpos.x + width; i++)
                {
                    for (int j = boxpos.y; j < boxpos.y + height; j++)
                    {
                        Vector3Int pos = new Vector3Int(i, j, position.z);
                        base.Paint(grid, brushTarget, pos);

                    }

                }
                isActive = false;
            }
            else
            {
                boxpos = position;
                isActive = true;

            }
        }

        [MenuItem("Assets/Create/Box Brush")]
        public static void CreateBrush()
        {
            string path = EditorUtility.SaveFilePanelInProject("Save Box Brush", "New Box Brush", "Asset", "Save Box Brush", "Assets");
            if (path == "")
                return;
            AssetDatabase.CreateAsset(ScriptableObject.CreateInstance<BoxBrush>(), path);
        }


    }
}
