using UnityEngine;

[CreateAssetMenu(menuName = "Interaction")]
public class Interaction : ScriptableObject
{
    private INode _node;

    public void Set()
    {
        if (_node == null)
            Debug.Log("Null");
        else
        {
            Debug.Log("Not Null");
            var converted = (LeafNode)_node;
            if(converted._executable == null)
            {
                Debug.Log("Executable Null");
            }
            else
            {
                Debug.Log("Executable Not Null");
            }
        }


        var command = new SetDialogueNameSettingsCommand(Dialogue.CONVERSATION, NameSettings.DEFAULT);
        _node = new LeafNode(command);
    }
}