using LevelEditor.Settings;
using UnityEditor;

namespace RimeEditor.Editor
{
    /// <inheritdoc />
    [CustomEditor(typeof(GlobalSetting))]
    public class GlobalSettingEditor : UnityEditor.Editor
    {
        private bool[] _folds = new bool[5];

        /// <inheritdoc />
        public override void OnInspectorGUI()
        {
            // Update the serializedProperty - always do this in the beginning of OnInspectorGUI.
            serializedObject.Update();
            OnGUI(serializedObject, ref _folds);
            serializedObject.ApplyModifiedProperties();
        }

        /// <summary>
        ///     Custom drawing methods
        /// </summary>
        /// <param name="serialized">Serialized objects</param>
        /// <param name="folds">Controls the collapsed flag bits</param>
        public static void OnGUI(SerializedObject serialized, ref bool[] folds)
        {
            //first part
            folds[0] = EditorGUILayout.BeginFoldoutHeaderGroup(folds[0], "场景设置");

            if (folds[0])
            {
                EditorGUILayout.PropertyField(serialized.FindProperty("levelEditor"));
                EditorGUILayout.PropertyField(serialized.FindProperty("levelPlay"));
            }

            EditorGUILayout.EndFoldoutHeaderGroup();

            EditorGUILayout.Separator();

            folds[1] = EditorGUILayout.BeginFoldoutHeaderGroup(folds[1], "标签设置");

            if (folds[1])
            {
                EditorGUILayout.PropertyField(serialized.FindProperty("controlHandle"));
                EditorGUILayout.PropertyField(serialized.FindProperty("rigidbodyTag"));
                EditorGUILayout.PropertyField(serialized.FindProperty("canCopyTag"));
            }

            EditorGUILayout.EndFoldoutHeaderGroup();

            EditorGUILayout.Separator();

            folds[2] = EditorGUILayout.BeginFoldoutHeaderGroup(folds[2], "场景配置");

            if (folds[2]) EditorGUILayout.PropertyField(serialized.FindProperty("screenSizeStandard"));

            EditorGUILayout.EndFoldoutHeaderGroup();

            EditorGUILayout.Separator();

            folds[3] = EditorGUILayout.BeginFoldoutHeaderGroup(folds[3], "关键路径设置");

            if (folds[3]) EditorGUILayout.PropertyField(serialized.FindProperty("itemFilePath"));

            EditorGUILayout.EndFoldoutHeaderGroup();

            EditorGUILayout.Separator();

            folds[4] = EditorGUILayout.BeginFoldoutHeaderGroup(folds[4], "可持久化文件属性");

            if (folds[4])
            {
                EditorGUILayout.PropertyField(serialized.FindProperty("levelDataName"));
                EditorGUILayout.PropertyField(serialized.FindProperty("gamesDataName"));
                EditorGUILayout.PropertyField(serialized.FindProperty("imagesDataName"));
                EditorGUILayout.PropertyField(serialized.FindProperty("soundsDataName"));
            }

            EditorGUILayout.EndFoldoutHeaderGroup();
        }
    }
}