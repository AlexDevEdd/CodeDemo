using _Game.Scripts.Systems;
using _Game.Scripts.Ui.Base;
using UnityEngine;
using Zenject;

namespace _Game.Scripts.Ui
{
    public class CheatsWindow : BaseWindow
    {
        [SerializeField] private BaseButton _addSoft;
        [SerializeField] private BaseButton _addHard;
        [SerializeField] private BaseButton _addTokens;
        [SerializeField] private BaseButton _removePurchases;

        [Inject] private CheatsSystem _cheats;
        
        public override void Init()
        {
             _addSoft.SetCallback(_cheats.AddSoft);
             _addHard.SetCallback(_cheats.AddHard);
             _addTokens.SetCallback(_cheats.AddTokens);
             _removePurchases.SetCallback(_cheats.RemoveAllPurchases);
            base.Init();
        }
    }
}