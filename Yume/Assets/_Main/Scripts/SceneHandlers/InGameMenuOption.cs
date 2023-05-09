using System;
using UnityEngine;

public class InGameMenuOptionHolder : MonoBehaviour
{
    InGameMenuOption? _option;

    public InGameMenuOption Option 
    { 
        get 
        {
            return _option.GetValueOrDefault();
        } 
        set 
        {
            if (_option.HasValue)
                throw new Exception("Init only.");
            _option = value;
        } 
    }
}

public enum InGameMenuOption
{
    Unset,
    Backpack,
    Settings,
}
