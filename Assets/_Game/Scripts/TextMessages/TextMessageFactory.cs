using System.Collections.Generic;
using _Game.Scripts.Ui.Base;
using Zenject;

namespace _Game.Scripts.TextMessages
{
    public class TextMessageFactory
    {
        [Inject] private LevelUI _levelUI;
        
        private readonly TextMessageUI.Pool _messagePool;
        private readonly List<TextMessageUI> _messages = new();
        
        public TextMessageFactory(TextMessageUI.Pool messagePool)
        {
            _messagePool = messagePool;
        }
        
        public void SpawnMessage(string text)
        {
            var i = _messages.Count;
            foreach (var message in _messages)
            {
                message.Move(120 * (i + 1));
                i--;
            }
            
            var newMessage = _messagePool.Spawn(_levelUI.transform, text);
            newMessage.OnFinished += RemoveMessage;
            _messages.Add(newMessage);
        }

        private void RemoveMessage(TextMessageUI messageUI)
        {
            messageUI.OnFinished -= RemoveMessage;
            _messagePool.Despawn(messageUI);
            _messages.Remove(messageUI);
        }
    }
}