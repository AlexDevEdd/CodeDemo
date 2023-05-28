namespace _Game.Scripts.Interfaces
{
    public interface ILoadSystem
    {
        public void Load(bool clearData);

        public SaveData GetData();
    }
}