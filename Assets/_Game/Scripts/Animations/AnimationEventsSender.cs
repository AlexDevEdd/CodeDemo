using _Game.Scripts.Interfaces;
using _Game.Scripts.View.Base;

namespace _Game.Scripts.Animations
{
    public class AnimationEventsSender : BaseView, IAnimationEventsSender
    {
        private IAnimationEventsListener _listener;
        private int _id;
        
        public void AssignListener(IAnimationEventsListener listener, int id)
        {
            _listener = listener;
            _id = id;
        }

        public void AddEvent()
        {
        }

        public void SendEvent()
        {
            _listener?.ExecuteEvent(_id);
        }
    }
}