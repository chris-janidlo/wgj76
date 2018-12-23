using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using UnityEngine;
using crass;

public class Registers : Singleton<Registers>
{
    public int Size;

    public ReadOnlyCollection<IAtomicAction> Contents {
        get {
            return new List<IAtomicAction>(idleQueue).AsReadOnly();
        }
    }

    Queue<IAtomicAction> idleQueue; // for things the user sees, waiting to be flushed
    Queue<IAtomicAction> runQueue; // queue of all actions being executed

    bool running;

    void Awake()
    {
        SingletonSetInstance(this, true);
    }

    void Start()
    {
        Clock.Instance.OnTick += flushAndRun;
        idleQueue = new Queue<IAtomicAction>();
        runQueue = new Queue<IAtomicAction>();
    }

    public void AddAction (IAtomicAction action)
    {
        idleQueue.Enqueue(action);
    }

    public bool IsFull ()
    {
        return idleQueue.Count >= Size;
    }

    void flushAndRun ()
    {
        while (idleQueue.Count != 0)
        {
            runQueue.Enqueue(idleQueue.Dequeue());
        }

        if (!running) StartCoroutine(flushAndRunRoutine());
    }

    IEnumerator flushAndRunRoutine ()
    {
        running = true;
        while (runQueue.Count != 0)
        {
            yield return runQueue.Dequeue().Execute();
        }
        running = false;
    }
}
