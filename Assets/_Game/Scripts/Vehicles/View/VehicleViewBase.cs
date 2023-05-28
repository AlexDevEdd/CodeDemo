using _Game.Scripts.Enums;
using _Game.Scripts.Systems.Base;
using _Game.Scripts.Tools;
using _Game.Scripts.View;
using _Game.Scripts.View.Base;
using DG.Tweening;
using System;
using UnityEngine;
using UnityEngine.AI;

namespace _Game.Scripts.Vehicles.View
{
    public class VehicleViewBase : BaseView
    {
        [SerializeField] private ParticleSystem _smokePS;
        [SerializeField] private ParticleSystem _lightingFXX;
        
        private float _startAgentSpeed;
        
        private VisualView[] _visuals;
        
        public int Id { get; private set; }
        public NavMeshAgent NavMeshAgent { get; private set; }
        public Transform ModelTransform => transform.GetChild(0);
        public VehicleType Type { get; private set; }
        
        public override void Init()
        {
            //MOCK
        }

        protected void SetId(int id)
        {
            Id = id;
        }
        
        public void SetPosition(Vector3 position)
        {
            transform.position = position;
        }

        public void LightingParticleAction(bool state)
        {
            if (_lightingFXX != null) _lightingFXX.SetActive(state);
        }
        
        public void ChangePosition(Vector3 offset)
        {
            transform.position += offset;
        }
        
        public void SetSpeed(float value)
        {
            NavMeshAgent.speed = value;
        }
        
        public void SetSpeed()
        {
            NavMeshAgent.speed = _startAgentSpeed;
        }
        
        public void SetRotation(Vector3 rotation, bool toDefaultRotation)
        {
            transform.eulerAngles = rotation;
            if (toDefaultRotation)
            {
                if (_smokePS)
                {
                    _smokePS.Deactivate();
                    InvokeSystem.StartInvoke(EnablePS, 0.1f);
                }
            }
        }
        
        public void SmoothAppear(float time = 1f)
        {
            //MOCK
        }
        
        public void SmoothHide(float time = 1f)
        {
            //MOCK
        }

        private void EnablePS()
        {
            if (_smokePS != null ) _smokePS.Activate();
        }
        
        public void MoveToRoadLine(float speed = 0f)
        {
            if (speed == 0f)
            {
                var childTransform = transform.GetChild(0);
                var localPosition = childTransform.localPosition;
                transform.GetChild(0).localPosition = new Vector3(0.5f, localPosition.y, localPosition.z);
                return;
            }
            transform.GetChild(0).DOLocalMoveX(0.5f, 1f / speed);
        }
        
        public void MoveToCenter(float speed = 0f)
        {
            if (speed == 0f)
            {
                var childTransform = transform.GetChild(0);
                var localPosition = childTransform.localPosition;
                transform.GetChild(0).localPosition = new Vector3(0f, localPosition.y, localPosition.z);
                return;
            }
            transform.GetChild(0).DOLocalMoveX(0f, 1f / speed);
        }
        
        public void SetNavMeshAgentFlag(bool flag)
        {
            NavMeshAgent.enabled = flag;
        }
        
        public void SetNavMeshDestination(Vector3 point)
        {
            NavMeshAgent.SetDestination(point);
        }
        
        public void Show()
        {
            this.Activate();
        }

        public void Hide()
        {
            this.Deactivate();
        }
        
        public void ShowSkin(VehicleType type)
        {
            Type = type;
            if (_visuals == null)
            {
                //MOCK
            }
            
            SetupParticleAnchor(type, _visuals[0]);
        }

        private void SetupParticleAnchor(VehicleType type, VisualView visual)
        {
            //MOCK
        }

        private void ShowSkin(VehicleType type, int num)
        {
            //MOCK
        }
    }
}