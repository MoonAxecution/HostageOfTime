using System;
using UnityEngine;

public static class DelegateExtensions
{
    public static void Fire(this Action action)
    {
        if (action == null)
            return;
        Delegate[] delegates = action.GetInvocationList();
        for (int index = 0; index < delegates.Length; ++index)
        {
            Action delegateAction = (Action)delegates[index];
            if (IsSafeDelegate(delegateAction))
                delegateAction();
        }
    }

    public static void Fire<T>(this Action<T> action, T param)
    {
        if (action == null)
            return;
        Delegate[] delegates = action.GetInvocationList();
        for (int index = 0; index < delegates.Length; ++index)
        {
            Action<T> delegateAction = (Action<T>)delegates[index];
            if (IsSafeDelegate(delegateAction))
                delegateAction(param);
        }
    }

    public static void Fire<T1, T2>(this Action<T1, T2> action, T1 param1, T2 param2)
    {
        if (action == null)
            return;
        Delegate[] delegates = action.GetInvocationList();
        for (int index = 0; index < delegates.Length; ++index)
        {
            Action<T1, T2> delegateAction = (Action<T1, T2>)delegates[index];
            if (IsSafeDelegate(delegateAction))
                delegateAction(param1, param2);
        }
    }

    public static void Fire<T1, T2, T3>(this Action<T1, T2, T3> action, T1 param1, T2 param2, T3 param3)
    {
        if (action == null)
            return;
        Delegate[] delegates = action.GetInvocationList();
        for (int index = 0; index < delegates.Length; ++index)
        {
            Action<T1, T2, T3> delegateAction = (Action<T1, T2, T3>)delegates[index];
            if (IsSafeDelegate(delegateAction))
                delegateAction(param1, param2, param3);
        }
    }

    public static void Fire<T1, T2, T3, T4>(this Action<T1, T2, T3, T4> action, T1 param1, T2 param2, T3 param3, T4 param4)
    {
        if (action == null)
            return;
        Delegate[] delegates = action.GetInvocationList();
        for (int index = 0; index < delegates.Length; ++index)
        {
            Action<T1, T2, T3, T4> delegateAction = (Action<T1, T2, T3, T4>)delegates[index];
            if (IsSafeDelegate(delegateAction))
                delegateAction(param1, param2, param3, param4);
        }
    }

    public static TResult Fire<TResult>(this Func<TResult> func)
	{
        TResult result = default(TResult);
		if (func == null)
			return result;
		Delegate[] delegates = func.GetInvocationList();
		for (int index = 0; index < delegates.Length; ++index)
		{
			Func<TResult> delegateFunc = (Func<TResult>)delegates[index];
			if (IsSafeDelegate(delegateFunc))
				result = delegateFunc();
		}
        return result;
	}

	public static TResult Fire<TParam, TResult>(this Func<TParam, TResult> func, TParam param)
	{
        TResult result = default(TResult);
		if (func == null)
			return result;
		Delegate[] delegates = func.GetInvocationList();
		for (int index = 0; index < delegates.Length; ++index)
		{
			Func<TParam, TResult> delegateFunc = (Func<TParam, TResult>)delegates[index];
			if (IsSafeDelegate(delegateFunc))
				result = delegateFunc(param);
		}
        return result;
	}

	static bool IsSafeDelegate(Delegate del)
	{
		object target = del.Target;

        if (target == null)
        {
            if (!del.Method.IsStatic)
            {
                Debug.LogWarning(string.Format("Delegate target is null along with not static method: {0} {1}", del.Method.Name, del.Method.DeclaringType));
                return false;
            }
        }
		else if ((target is UnityEngine.Object) && target.Equals(null))
        {
            Debug.LogWarning(string.Format("Delegate target was destroyed in native side but exist managed object: {0} {1}\n{2}", del.Method.Name, del.Method.DeclaringType, StackTraceUtility.ExtractStackTrace()));
            return false;
        }

        return true;
	}

    public static void CallIfValid(this Action action)
    {
        if (action == null) return;

        action();
    }

    public static void CallIfValid<T>(this Action<T> action, T arg)
    {
        if (action == null) return;

        action(arg);  
    }
    
    public static void CallIfValid<T1, T2>(this Action<T1, T2> action, T1 arg1, T2 arg2)
    {
        if (action == null) return;
        
        action(arg1, arg2);
    }

    // when the reference to an action can be changed during the action call you need to call the copy of a reference
    // so when the call is finished you don't null newly assigned reference
    // eg. Planimator.Play("example state", () => Planimator.Play("example state 2"));
    public static void ClearAndCallCopy(ref Action a)
    {
        if (a == null) return;

        Action copy = a;
        a = null;

        copy();
    }
}