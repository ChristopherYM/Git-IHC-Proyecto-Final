// Copyright (c) 2015 - 2019 Doozy Entertainment / Marlink Trading SRL. All Rights Reserved.
// This code can only be used under the standard Unity Asset Store End User License Agreement
// A Copy of the EULA APPENDIX 1 is available at http://unity3d.com/company/legal/as_terms

using Doozy.Editor.Nody.Settings;
using Doozy.Editor.Settings;
using Doozy.Editor.Utils;
using Doozy.Engine.Soundy;
using Doozy.Engine.Touchy;
using Doozy.Engine.UI.Animation;
using Doozy.Engine.UI.Settings;
using Doozy.Engine.Utils;
using UnityEditor;

namespace Doozy.Editor.Internal
{
    [InitializeOnLoad]
    public static class DoozyAssetsProcessor
    {
        static DoozyAssetsProcessor() { ExecuteProcessor(); }

        private static void ExecuteProcessor()
        {
            if (EditorApplication.isPlayingOrWillChangePlaymode) return;
            if (!ProcessorsSettings.Instance.RunDoozyAssetsProcessor) return;
            DelayedCall.Run(3f, Run);
        }

        public static void Run()
        {
            if (EditorApplication.isCompiling || EditorApplication.isUpdating)
            {
                DelayedCall.Run(2f, Run);
                return;
            }

            DoozyPath.CreateMissingFolders();

            CreateSettingsAssets(false);
            CreateDatabaseAssets(false);
            RegenerateDatabaseAssets(false);

            ProcessorsSettings.Instance.RunDoozyAssetsProcessor = false;
            ProcessorsSettings.Instance.SetDirty(false);

            DoozyUtils.DisplayProgressBar("Hold on...", "Saving Processor Settings...", 0.95f);
            DoozyUtils.SaveAssets();
            DoozyUtils.ClearProgressBar();

#if !dUI_MASTER
            if(AssetDatabase.IsValidFolder("Assets/DoozyInstaller"))
                AssetDatabase.MoveAssetToTrash("Assets/DoozyInstaller");
#endif
        }

        private static void CreateSettingsAssets(bool saveAssets = true)
        {
            DoozyUtils.ClearProgressBar();
            DoozyUtils.DisplayProgressBar("Hold on...", "Check Asset - DoozyWindowSettings", 0.1f);
            DoozyWindowSettings.Instance.SetDirty(false);
            DoozyUtils.DisplayProgressBar("Hold on...", "Check Asset - NodyWindowSettings", 0.2f);
            NodyWindowSettings.Instance.SetDirty(false);
            DoozyUtils.DisplayProgressBar("Hold on...", "Check Asset - SoundySettings", 0.3f);
            SoundySettings.Instance.SetDirty(false);
            DoozyUtils.DisplayProgressBar("Hold on...", "Check Asset - TouchySettings", 0.4f);
            TouchySettings.Instance.SetDirty(false);

            DoozyUtils.DisplayProgressBar("Hold on...", "Check Asset - UIButtonSettings", 0.5f);
            UIButtonSettings.Instance.SetDirty(false);
            DoozyUtils.DisplayProgressBar("Hold on...", "Check Asset - UICanvasSettings", 0.6f);
            UICanvasSettings.Instance.SetDirty(false);
            DoozyUtils.DisplayProgressBar("Hold on...", "Check Asset - UIDrawerSettings", 0.7f);
            UIDrawerSettings.Instance.SetDirty(false);
            DoozyUtils.DisplayProgressBar("Hold on...", "Check Asset - UIPopupSettings", 0.8f);
            UIPopupSettings.Instance.SetDirty(false);
            DoozyUtils.DisplayProgressBar("Hold on...", "Check Asset - UIToggleSettings", 0.85f);
            UIToggleSettings.Instance.SetDirty(false);
            DoozyUtils.DisplayProgressBar("Hold on...", "Check Asset - UIViewSettings", 0.9f);
            UIViewSettings.Instance.SetDirty(false);

            if (saveAssets)
            {
                DoozyUtils.DisplayProgressBar("Hold on...", "Saving Assets...", 0.95f);
                DoozyUtils.SaveAssets();
            }

            DoozyUtils.ClearProgressBar();
        }

        private static void CreateDatabaseAssets(bool saveAssets = true)
        {
            DoozyUtils.ClearProgressBar();
            DoozyUtils.DisplayProgressBar("Hold on...", "Check Asset - UIAnimations", 0.1f);
            UIAnimations.Instance.SetDirty(false);
            DoozyUtils.DisplayProgressBar("Hold on...", "Check Asset - SoundySettings", 0.2f);
            SoundySettings.Database.SetDirty(false);
            DoozyUtils.DisplayProgressBar("Hold on...", "Check Asset - UIButtonSettings", 0.3f);
            UIButtonSettings.Database.SetDirty(false);
            DoozyUtils.DisplayProgressBar("Hold on...", "Check Asset - UICanvasSettings", 0.5f);
            UICanvasSettings.Database.SetDirty(false);
            DoozyUtils.DisplayProgressBar("Hold on...", "Check Asset - UIDrawerSettings", 0.7f);
            UIDrawerSettings.Database.SetDirty(false);
            DoozyUtils.DisplayProgressBar("Hold on...", "Check Asset - UIPopupSettings", 0.8f);
            UIPopupSettings.Database.SetDirty(false);
            DoozyUtils.DisplayProgressBar("Hold on...", "Check Asset - UIViewSettings", 0.9f);
            UIViewSettings.Database.SetDirty(false);

            if (saveAssets)
            {
                DoozyUtils.DisplayProgressBar("Hold on...", "Saving Assets...", 0.95f);
                DoozyUtils.SaveAssets();
            }

            DoozyUtils.ClearProgressBar();
        }

        private static void RegenerateDatabaseAssets(bool saveAssets = true)
        {
            DoozyUtils.ClearProgressBar();

            //SOUNDY
            DoozyUtils.DisplayProgressBar("Hold on...", "Soundy - Search For Unregistered Databases", 0.1f);
            SoundySettings.Database.SearchForUnregisteredDatabases(false);
            DoozyUtils.DisplayProgressBar("Hold on...", "Soundy - Refresh", 0.2f);
            SoundySettings.Database.RefreshDatabase(false, false);

            //UIAnimations
            DoozyUtils.DisplayProgressBar("Hold on...", "UIAnimations - Search For Unregistered Databases", 0.3f);
            UIAnimations.Instance.SearchForUnregisteredDatabases(false);

            DoozyUtils.DisplayProgressBar("Hold on...", "Buttons - Search For Unregistered Databases", 0.4f);
            UIButtonSettings.Database.SearchForUnregisteredDatabases(false);
            DoozyUtils.DisplayProgressBar("Hold on...", "Canvases - Search For Unregistered Databases", 0.6f);
            UICanvasSettings.Database.SearchForUnregisteredDatabases(false);
            DoozyUtils.DisplayProgressBar("Hold on...", "Drawers - Search For Unregistered Databases", 0.7f);
            UIDrawerSettings.Database.SearchForUnregisteredDatabases(false);
            DoozyUtils.DisplayProgressBar("Hold on...", "Popups - Search For Unregistered Databases", 0.8f);
            UIPopupSettings.Database.SearchForUnregisteredLinks(false);
            DoozyUtils.DisplayProgressBar("Hold on...", "Views - Search For Unregistered Databases", 0.9f);
            UIViewSettings.Database.SearchForUnregisteredDatabases(false);

            if (saveAssets)
            {
                DoozyUtils.DisplayProgressBar("Hold on...", "Saving Assets...", 0.95f);
                DoozyUtils.SaveAssets();
            }

            DoozyUtils.ClearProgressBar();
        }
    }
}