using System.Linq;
using _Game.Scripts.Balance;
using _Game.Scripts.ScriptableObjects.Balance;
using Sirenix.OdinInspector.Editor;
using Sirenix.Utilities.Editor;
using UnityEditor;
using UnityEngine;

namespace _Game.Scripts.Editor
{
    public class LocalizationEditorWindow : OdinMenuEditorWindow
    {
        [MenuItem("_Game/My Window")]
        private static void OpenWindow()
        {
            GetWindow<LocalizationEditorWindow>().Show();
        }

        protected override OdinMenuTree BuildMenuTree()
        {
            var pathStr = "Assets/_Game/Resources/Balance/";
            var tree = new OdinMenuTree(supportsMultiSelect: true);
            tree.AddAssetAtPath("Main settings", $"{pathStr}GameBalanceConfigs.asset");
            
            //Добавляем все локализации
            tree.Add("Localizations", new GUIContent());
            var balance = GameBalanceConfigs.Instance;
            foreach (var table in balance.TableInfos.Where(t => t.Type == GameBalanceConfigs.TableType.Localization))
            {
                tree.AddAssetAtPath($"Localizations/Version{table.Version}", $"{pathStr}/Localizations/Localization{table.Version}.asset");
            }
           
            tree.Add("Balance", new GUIContent());
            foreach (var table in balance.TableInfos.Where(t => t.Type == GameBalanceConfigs.TableType.Balance))
            {
                tree.AddAssetAtPath($"Balance/Version{table.Version}", $"{pathStr}/Balances/Balance{table.Version}.asset");
            }
            
            return tree;
        }
    }
}