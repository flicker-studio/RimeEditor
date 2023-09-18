using UnityEngine;
using UnityEditor;

public static class HierarchyWindowExtension
{
    [MenuItem("GameObject/Project-Moon Object/Cube", false, 0)]
    public static void CreateCube(MenuCommand menuCommand)
    {
        GameObject cube = GameObject.CreatePrimitive(PrimitiveType.Cube);
        GameObject.DestroyImmediate(cube.GetComponent<Collider>());
        cube.AddComponent<BoxCollider2D>();
        Selection.activeObject = cube;
        GameObjectUtility.SetParentAndAlign(cube, menuCommand.context as GameObject);
        Undo.RegisterCreatedObjectUndo(cube, "Create " + cube.name);
    }
    
    [MenuItem("GameObject/Project-Moon Object/Sphere", false, 1)]
    public static void CreateSphere(MenuCommand menuCommand)
    {
        GameObject shere = GameObject.CreatePrimitive(PrimitiveType.Sphere);
        GameObject.DestroyImmediate(shere.GetComponent<Collider>());
        shere.AddComponent<CircleCollider2D>();
        Selection.activeObject = shere;
        GameObjectUtility.SetParentAndAlign(shere, menuCommand.context as GameObject);
        Undo.RegisterCreatedObjectUndo(shere, "Create " + shere.name);
    }
    
    [MenuItem("GameObject/Project-Moon Object/Capsule", false, 2)]
    public static void CreateCapsule(MenuCommand menuCommand)
    {
        GameObject capsule = GameObject.CreatePrimitive(PrimitiveType.Capsule);
        GameObject.DestroyImmediate(capsule.GetComponent<Collider>());
        capsule.AddComponent<CapsuleCollider2D>();
        Selection.activeObject = capsule;
        GameObjectUtility.SetParentAndAlign(capsule, menuCommand.context as GameObject);
        Undo.RegisterCreatedObjectUndo(capsule, "Create " + capsule.name);
    }
}