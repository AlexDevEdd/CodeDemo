using System.Collections.Generic;
using System.Linq;

namespace _Game.Scripts.Systems.Base
{
    public static class InvokeSystem
    {
        private static InvokeClass _externalInvokeClass;
        private static readonly List<InvokeClass> _invokeClasses = new();
        private static readonly List<InvokeClass> _removeList = new();
        public delegate void InvokeCallback();

        public static void ForceTick()
        {
            if(_externalInvokeClass == null) return;
            _externalInvokeClass.Frames --;
            if (_externalInvokeClass.Frames <= 0)
            {
                var method = _externalInvokeClass.Callback;
                _externalInvokeClass = null;
                method?.Invoke();
            }
        }

        public static void Tick(float deltaTime)
        {
            if (_invokeClasses == null) return;
           
            _removeList.Clear();

            for (var i = 0; i < _invokeClasses.Count; i++)
            {
                var method = _invokeClasses[i];
                method.Tick(deltaTime);
                if (method.CanInvoke())
                {
                    _removeList.Add(method);
                    method.Callback?.Invoke();
                }
            }

            foreach (var removeMethod in _removeList)
            {
                _invokeClasses.Remove(removeMethod);
            }
        }
        
        public static void StartInvoke(InvokeCallback method, float time, bool checkExisting = false)
        {
            if (checkExisting)
            {
                if (_invokeClasses.FirstOrDefault(c => c.Callback.Target == method.Target) != null)
                {
                    return;
                }
            }

            AddInvoke(method, time, 0, false);
        }
        
        public static void StartInvoke(InvokeCallback method, int frames, bool checkExisting = false)
        {
            if (checkExisting && IsExisting(method)) return;

            AddInvoke(method, 0f, frames, true);
        }

        private static bool IsExisting(InvokeCallback method)
        {
            if (_invokeClasses.FirstOrDefault(c => c.Callback.Target == method.Target) != null)
            {
                return true;
            }

            return false;
        }

        private static void AddInvoke(InvokeCallback method, float time, int frames, bool tickable)
        {
            var newInvoke = new InvokeClass (tickable)
            {
                Callback = method,
                Time = + time,
                Frames = frames
            };
            _invokeClasses.Add(newInvoke);
        }

        public static void StartExternalInvoke(InvokeCallback method, int frames)
        {
            _externalInvokeClass = new InvokeClass(false)
            {
                Callback = method,
                Frames = +frames
            };
        }

        public static void CancelInvoke(InvokeCallback method, bool all = false)
        {
            if (_invokeClasses == null) return;
            if (_invokeClasses.Count == 0) return;
            var targetInvokes = _invokeClasses.Where(invoke =>
                invoke.Callback.Target == method.Target && invoke.Callback.Equals(method)).ToList();
            if(targetInvokes.Count == 0) return;
            foreach (var invoke in targetInvokes)
            {
                _invokeClasses.Remove(invoke);
                if(!all) break;
            }
        }
        
        public static void Clear()
        {
            foreach (var item in _invokeClasses)
            {
                item.Callback = null;
            }
            _invokeClasses.Clear();
        }
    }
    
    public class InvokeClass
    {
        public InvokeSystem.InvokeCallback Callback;
        public float Time;
        public int Frames;
        private bool _tickable;

        public InvokeClass(bool tickable)
        {
            _tickable = tickable;
        }

        public void Tick(float deltaTime)
        {
            if (_tickable)
            {
                Frames--;
            }
            else
            {
                Time -= deltaTime;
            }
        }

        public bool CanInvoke()
        {
            if (_tickable)
            {
                if (Frames <= 0) return true;
            }
            else
            {
                if (Time <= 0) return true;
            }

            return false;
        }
    }
}
