using System.IO;

using Unity.VisualScripting;

using UnityEngine;


namespace TheAshBot
{
    public struct SaveSystem
    {

        /// <summary>
        /// This is the root of the save/load path. NOTE: If the file location for the Editor is not the same as the file location for the Build then any file under the editor folder will not go to the location of the Build folder, this does not happen to "Resources".
        /// </summary>
        public enum RootPath
        {
            /// <summary>
            /// This will save/load data at Editor. "(YOUR_PROJECT_LOCALTION)/Assets/Resources/"    Build. "(YOUR_BUILD_LOCALTION)/(YOUR_BUILD_NAME)_Data/Resources/"
            /// </summary>
            Resources,
            /// <summary>
            /// This will save/load data at Editor. "(YOUR_PROJECT_LOCALTION)/Assets/"    Build. "(YOUR_BUILD_LOCALTION)/(YOUR_BUILD_NAME)_Data/"
            /// </summary>
            DataPath,
            /// <summary>
            /// This will save/load data at "C:Users/(YOUR_USERNAME)/AppData/LocalLow/(YOUR_COMPANY_NAME)/(YOUR_PROJECT_NAME)/"
            /// </summary>
            PersistentDataPath,
            /// <summary>
            /// This will save/load data at Editor. "C:Users/(YOUR_USERNAME)/AppData/Local/Unity/Editor/Editor.log/"    Build. "C:Users/(YOUR_USERNAME)/AppData/LocalLow/(YOUR_COMPANY_NAME)/(YOUR_PROJECT_NAME)/Player.log/"
            /// </summary>
            StreamingAssetsPath,
            /// <summary>
            /// This will save/load data at "C:Users/(YOUR_USERNAME)/AppData/Local/Temp/(YOUR_COMPANY_NAME)/(YOUR_PROJECT_NAME)/"
            /// </summary>
            TemporaryCachePath,
            /// <summary>
            /// This will not use the root path.
            /// </summary>
            Costum,
        }

        /// <summary>
        /// This is a enum with all the suported file tpyes.
        /// </summary>
        public enum FileType
        {
            /// <summary>
            /// This is a text file. (.txt)
            /// </summary>
            Txt,
            /// <summary>
            /// This is a JSON file. (.json)
            /// </summary>
            Json,
            /// <summary>
            /// This is a C-Sharp file (.cs)
            /// </summary>
            Cs,
            /// <summary>
            /// This allows you to chose what file type you want the file to be in the name of the file.
            /// </summary>
            Costum,
        }



        /// <summary>
        /// This will save a string as a file.
        /// </summary>
        /// <param name="text">This is the string that it will save.</param>
        /// <param name="rootSavePath">This is the root of the path the file will be saved to</param>
        /// <param name="path">This is the path that the file will be saved to. This goes after the "rootSavePath"</param>
        /// <param name="name">This is the name of the file being saved.</param>
        /// <param name="fileType">This is the type of file being saved.</param>
        /// <param name="canOveride">If true then this will overide any data with the same name, and at the same path, and with the same filetpye as this file.</param>
        public static void SaveString(string text, RootPath rootSavePath, string path, string name, FileType fileType, bool canOveride)
        {
            string saveFolder = GetPathRoot(rootSavePath) + path;
            string wholePath = saveFolder + "/" + name + GetFileType(fileType);

            if (File.Exists(saveFolder))
            {
                // If the path exists...
                // Than save the file.
                SaveFile();
            }
            else
            {
                // If the path does not exiset..
                // Than create the directory
                Directory.CreateDirectory(saveFolder);

                // and save the file
                SaveFile();
            }

            void SaveFile()
            {
                if (File.Exists(wholePath))
                {
                    // the file olready exist
                    if (!canOveride)
                    {
                        // Can not overide the older file
                        // adding a number to the end
                        int saveNumber = 0;
                        wholePath = saveFolder + "/" + name + "_" + saveNumber + GetFileType(fileType);
                        while (File.Exists(wholePath))
                        {
                            saveNumber++;
                            wholePath = saveFolder + "/" + name + "_" + saveNumber + GetFileType(fileType);
                        }
                    }

                    File.WriteAllText(wholePath, text);
                    return;
                }

                File.WriteAllText(wholePath, text);
            }
        }

        /// <summary>
        /// This will save a json object as a json file.
        /// </summary>
        /// <param name="jsonObject">This is the json object being saved.</param>
        /// <param name="rootSavePath">This is the root of the path the file will be saved to</param>
        /// <param name="path">This is the path that the file will be saved to. This goes after the "rootSavePath"</param>
        /// <param name="name">This is the name of the file being saved.</param>
        /// <param name="canOveride">If true then this will overide any data with the same name, and at the same path, and with the same filetpye as this file.</param>
        public static void SaveJson(object jsonObject, RootPath rootSavePath, string path, string name, bool canOveride)
        {
            // Converting the json object to a string
            string text = JsonUtility.ToJson(jsonObject);

            // saveing the string as a json file
            SaveString(text, rootSavePath, path, name, FileType.Json, canOveride);
        }

        /// <summary>
        /// This will load a file as a string.
        /// </summary>
        /// <param name="savePathRoot">This is the root of the path that the file is at.</param>
        /// <param name="path">This is the rest of the path fallowing the root path.</param>
        /// <param name="name">This is the name of the file being loaded.</param>
        /// <param name="fileType">This is the type of file being loaded.</param>
        /// <returns>The the string data from the loaded file.</returns>
        public static string LoadString(RootPath savePathRoot, string path, string name, FileType fileType)
        {
            // Getting the path.
            string saveFolder = GetPathRoot(savePathRoot) + path;
            string wholePath = saveFolder + "/" + name + GetFileType(fileType);

            if (!File.Exists(wholePath))
            {
                // There is no file at the location.
                Debug.Log("Can not find " + name + GetFileType(fileType) + " at " + saveFolder);
                return default;
            }

            return File.ReadAllText(wholePath);
        }

        /// <summary>
        /// This loads a JSON file and convers it to a TSaveObject.
        /// </summary>
        /// <typeparam name="TSaveObject">This is save object that the JSON is being loaded as.</typeparam>
        /// <param name="savePathRoot">This is the root of the path that the file is at.</param>
        /// <param name="path">This is the rest of the path fallowing the root path.</param>
        /// <param name="name">This is the name of the file being loaded.</param>
        /// <returns>The JSON as a Save Object</returns>
        public static TSaveObject LoadJson<TSaveObject>(RootPath savePathRoot, string path, string name)
        {
            // loading the JSON as a string
            string json = LoadString(savePathRoot, path, name, FileType.Json);

            // Converting the JSON to a save object
            return JsonUtility.FromJson<TSaveObject>(json);
        }



        /// <summary>
        /// This uses the enum to determain where the root path is.
        /// </summary>
        /// <param name="savePathRoot">This is the RootPath enum.</param>
        /// <returns>The root path as a string</returns>
        private static string GetPathRoot(RootPath savePathRoot)
        {
            switch (savePathRoot)
            {
                case RootPath.Resources:
                    return Application.dataPath + "/Resources/";
                case RootPath.DataPath:
                    return Application.dataPath + "/";
                case RootPath.PersistentDataPath:
                    return Application.persistentDataPath + "/";
                case RootPath.StreamingAssetsPath:
                    return Application.streamingAssetsPath + "/";
                case RootPath.TemporaryCachePath:
                    return Application.temporaryCachePath + "/";
                case RootPath.Costum:
                    return "";
                default:
                    return Application.persistentDataPath + "/";
            }
        }

        /// <summary>
        /// This uses the enum to determain the file type.
        /// </summary>
        /// <param name="fileType">This is the FileType enum</param>
        /// <returns>the file type F.E. ".txt"</returns>
        private static string GetFileType(FileType fileType)
        {
            switch (fileType)
            {
                case FileType.Txt:
                    return ".txt";
                case FileType.Json:
                    return ".json";
                case FileType.Cs:
                    return ".cs";
                case FileType.Costum:
                    return "";
                default:
                    return ".txt";
            }
        }

    }
}
