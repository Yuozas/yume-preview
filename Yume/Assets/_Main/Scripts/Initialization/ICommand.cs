using System;

public interface ICommand
{
    void Execute(Action action = null);
}