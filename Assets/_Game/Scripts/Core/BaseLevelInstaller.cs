using System.Collections.Generic;
using _Game.Scripts.Interfaces;
using _Game.Scripts.ResourceBubbles;
using _Game.Scripts.ScriptableObjects;
using _Game.Scripts.Systems.Base;
using _Game.Scripts.TextMessages;
using _Game.Scripts.Tools;
using _Game.Scripts.Ui.Base;
using UnityEngine;
using Zenject;

namespace _Game.Scripts.Core
{
    public class BaseLevelInstaller : MonoInstaller, IInitializable, IInitGameLogic, IInitSceneObjects
    {
        [SerializeField] protected GameObject _root;
        
        [Inject] protected WindowsSystem Windows;

        private bool _init;
        private List<ITickableSystem> _tickSystems;

        public override void InstallBindings()
        {
            Container.BindInterfacesAndSelfTo<IInitializable>().FromInstance(this).AsSingle().NonLazy();
        }

        public virtual void Initialize()
        {
        }

        public virtual void InitGameLogic()
        {
            _tickSystems = Container.ResolveAll<ITickableSystem>();

            var systems = Container.ResolveAll<IInitGameLogic>();
            foreach (var system in systems)
            {
                system.InitGameLogic();
            }
            _init = true;
        }

        public virtual void InitNewLevelLogic()
        {
            var systems = Container.ResolveAll<IInitNewLevelLogic>();
            foreach (var system in systems)
            {
                system.InitNewLevelLogic();
            }
        }

        public virtual void InitLevelUI()
        {
            var windows = _root.GetComponentsInChildren<BaseWindow>(true);
            foreach (var window in windows)
            {
                Windows.AddWindow(window);
            }
            Windows.InitWindows();
        }

        public virtual void InitSceneObjects()
        {
            var systems = Container.ResolveAll<IInitSceneObjects>();
            foreach (var system in systems)
            {
                system.InitSceneObjects();
            }
        }

        public virtual void ResetGameLogic()
        {
            var systems = Container.ResolveAll<IResetGameLogic>();
            foreach (var system in systems)
            {
                system.ResetGameLogic();
            }
            
            var windows = _root.GetComponentsInChildren<BaseWindow>(true);
            foreach (var window in windows)
            {
                window.ResetGameLogic();
                Windows.RemoveWindow(window);
            }
            Windows.ResetGameLogic();
        }
        
        public virtual void LoadSave(bool clearData)
        {
        }
        
        private void Update()
        {
            if (!_init) return;
            var deltaTime = Time.deltaTime;
            foreach (var element in _tickSystems)
            {
                element.Tick(deltaTime);
            }
        }
    }
}