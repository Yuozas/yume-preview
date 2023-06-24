using UnityEngine;

public class ChoiceUserInterface : MonoBehaviour
{
    public Choice Choice { get; private set; }

    public void Initialize(Choice choice)
    {
        Choice = choice;
    }
}
