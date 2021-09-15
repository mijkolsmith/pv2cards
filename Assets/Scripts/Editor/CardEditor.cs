using UnityEditor;

[CustomEditor(typeof(Card))]
public class CardEditor : Editor
{
    public override void OnInspectorGUI()
    {
        Card card = (Card)target;

        DrawDefaultInspector();
        var modifier = serializedObject.FindProperty("modifier");
        var left = serializedObject.FindProperty("left");
        var multiply = serializedObject.FindProperty("multiply");

        if (card.effect != CardEffect.NONE)
        {
            EditorGUILayout.PropertyField(modifier);
            EditorGUILayout.PropertyField(left);
            EditorGUILayout.PropertyField(multiply);

            serializedObject.ApplyModifiedProperties();
        }

        // Debugging
        EditorGUILayout.IntField("Attack(Debug)", card.attack);
        EditorGUILayout.IntField("Energy(Debug)", card.energy);
        EditorGUILayout.IntField("siblingIndex(Debug)", card.siblingIndex);
        EditorGUILayout.IntField("isLerping(Debug)", card.isLerping ? 1 : 0);
        if (EditorApplication.isPlaying)
        {
            EditorGUILayout.TextField("State(Debug)", card.GetState().ToString());
        }
    }
}