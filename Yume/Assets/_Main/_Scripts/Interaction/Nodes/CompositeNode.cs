using System;
using UnityEngine;

[Serializable]
public class CompositeNode : BaseNode, INode
{
    [SerializeField] private bool _wait;
    [field: SerializeReference] public ICommand Executable { get; protected set; }

    public CompositeNode(string type, bool wait, ICommand executable = null) : base(type)
    {
        _wait = wait;
        Executable = executable;
    }

    public void Execute()
    {
        if(Executable is null)
        {
            ExecuteAllConnections();
            return;
        }

        if (_wait)
        {
            Executable.Execute(ExecuteAllConnections);
            return;
        }

        Executable.Execute();
        ExecuteAllConnections();
    }

    private void ExecuteAllConnections()
    {
        foreach (var connection in Connections)
            connection.Execute();
    }
}