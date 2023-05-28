namespace _Game.Scripts.Interfaces
{
	public interface IAnimationEventsSender
	{
		void AssignListener(IAnimationEventsListener listener, int id);
		void AddEvent();
	}
}