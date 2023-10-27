using System;

using UnityEngine;
using UnityEngine.SceneManagement;

namespace TheAshBot.SceenManagment
{
    public class SceneLoader
    {

        public enum Scenes
        {
            Loading = 0,
        }


        public static event SceneLoaded OnSceneLoaded;
        public delegate void SceneLoaded(Scenes scene);


        public static Scenes Scene
        {
            get;
            private set;
        }


        private static Scenes sceneBeingLoaded;
        private static Action OnLoadFromLoadingScene;
        private static AsyncOperation loadingAsyncOperation;


        public static void LoadScene(Scenes scene)
        {
            // Subscribing to the event
            OnLoadFromLoadingScene += () =>
            {
                LoadSceneAsync(scene);
            };

            // Load the loading scene
            AsyncOperation loading = SceneManager.LoadSceneAsync(Scenes.Loading.ToString());
            loading.completed += Loading_completed;

            Scene = Scenes.Loading;
            OnSceneLoaded?.Invoke(scene);
        }

        private static void Loading_completed(AsyncOperation obj)
        {
            OnLoadFromLoadingScene?.Invoke();
            OnLoadFromLoadingScene = null;
        }

        private static void LoadSceneAsync(Scenes scene)
        {
            loadingAsyncOperation = SceneManager.LoadSceneAsync(scene.ToString());
            loadingAsyncOperation.completed += LoadingAsyncOperation_completed;
            sceneBeingLoaded = scene;
        }

        private static void LoadingAsyncOperation_completed(AsyncOperation obj)
        {
            Scene = sceneBeingLoaded;
            OnSceneLoaded?.Invoke(Scene);
        }
    }
}