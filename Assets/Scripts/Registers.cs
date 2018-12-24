using System.Linq;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using UnityEngine;
using crass;

public class Registers : Singleton<Registers>
{
    public int Size;

    public ReadOnlyCollection<AtomicAction> Contents {
        get {
            return new List<AtomicAction>(idleQueue).AsReadOnly();
        }
    }

    Queue<AtomicAction> idleQueue; // for things the user sees, waiting to be flushed
    Queue<AtomicAction> runQueue; // queue of all actions being executed

    bool running;

    void Awake()
    {
        SingletonSetInstance(this, true);
    }

    void Start()
    {
        Clock.Instance.OnTick += flushAndRun;
        idleQueue = new Queue<AtomicAction>();
        runQueue = new Queue<AtomicAction>();
    }

    public void AddAction (AtomicAction action)
    {
        if (idleQueue.Count < Size)
        {
            idleQueue.Enqueue(action);
        }
    }

    public void AddActions (ICollection<AtomicAction> actions)
    {
        foreach (var a in actions)
        {
            AddAction(a);
        }
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
