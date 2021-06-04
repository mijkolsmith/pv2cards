using UnityEditor;
[CustomEditor(typeof(Card))]

public class CardEditor : Editor
{
    public override void OnInspectorGUI()
    {
        Card card = (Card)target;

        DrawDefaultInspector();

        if (card.effect != CardEffect.NONE)
        {
            card.modifier = EditorGUILayout.IntField("Modifier", card.modifier);
            card.left = EditorGUILayout.Toggle("Left", card.left);
        }

        // Debugging
        EditorGUILayout.IntField("Attack(Debug)", card.attack);
        EditorGUILayout.IntField("Energy(Debug)", card.energy);
        EditorGUILayout.IntField("siblingIndex(Debug)", card.siblingIndex);
    }
}