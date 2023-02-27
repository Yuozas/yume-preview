﻿using UnityEngine.SceneManagement;
using System.Collections.Generic;
using System.Linq;
using System.Collections;

public class Transitioner : Singleton<Transitioner>
{
    readonly List<TransitionDestination> _destinations = new();
    IEnumerator _coroutine;

    public void Transition(Scriptable_TransitionDestination to)
    {
        var scene = SceneManager.GetActiveScene();
        if (scene.name == to.Scene)
        {
            TransitionToDestination(to);
            return;
        }

        Stop();

        _coroutine = Co_LoadScene(to);
        StartCoroutine(_coroutine);
    }

    void TransitionToDestination(Scriptable_TransitionDestination to)
    {
        var transitionable = FindObjectOfType<Transitionable>();
        Get(to).Set(transitionable);
    }

    void Stop()
    {
        if (_coroutine == null) return;

        StopCoroutine(_coroutine);
        _coroutine = null;
    }

    public void Add(TransitionDestination destination) => _destinations.Add(destination);
    public void Remove(TransitionDestination destination) => _destinations.Remove(destination);
    TransitionDestination Get(Scriptable_TransitionDestination to)
        => _destinations.First(destination => destination.This == to);

    IEnumerator Co_LoadScene(Scriptable_TransitionDestination to)
    {
        var asyncLoad = SceneManager.LoadSceneAsync(to.Scene);

        while (!asyncLoad.isDone)
            yield return null;

        TransitionToDestination(to);
    }
}
