using _Game.Scripts.View.Base;
using UnityEngine;

namespace _Game.Scripts.View.Fx
{
    public class BaseFx : BaseView
    {
        [SerializeField] protected ParticleSystem _particle;
        
        private Vector3 _startScale = Vector3.zero;
       
        public void Show(Vector3 position)
        {
            if (_startScale != Vector3.zero) transform.localScale = _startScale;
            transform.position = position;
            _particle.Play();
        }
        
        public void Show(Transform parent)
        {
            if (_startScale != Vector3.zero) transform.localScale = _startScale;
            _particle.Play();

            var position = parent.position;
            var scale = transform.localScale;
            var rotate = transform.rotation;
            
            if (parent.TryGetComponent(out CustomFXAnchor anchor))
            {
                position = new Vector3(position.x + anchor.Anchor.Position.x, 
                    position.y + anchor.Anchor.Position.y, 
                    position.z + anchor.Anchor.Position.z);
                scale = anchor.Anchor.Scale;
            }
            
            transform.position = position;
            transform.localScale = scale;
            transform.rotation = rotate;
        }

        public void Play()
        {
            _particle.Play();
        }
    }
}
