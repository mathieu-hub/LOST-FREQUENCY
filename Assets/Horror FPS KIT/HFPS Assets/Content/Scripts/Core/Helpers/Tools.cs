﻿using System;
using System.Linq;
using System.Threading;
using System.Globalization;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using UnityEngine;
using ThunderWire.CrossPlatform.Input;
#if UNITY_EDITOR
using UnityEditor;
#endif

namespace ThunderWire.Utility
{
    [System.Serializable]
    public enum FilePath
    {
        GameDataPath,
        DocumentsPath
    }

    /// <summary>
    /// Basic Utility Tools for HFPS
    /// </summary>
    public static class Tools
    {
        /// <summary>
        /// Compare Layer with LayerMask
        /// </summary>
        public static bool CompareLayer(this LayerMask layermask, int layer)
        {
            return layermask == (layermask | (1 << layer));
        }

        /// <summary>
        /// Get MainCamera Instance
        /// </summary>
        public static UnityEngine.Camera MainCamera()
        {
            return UnityEngine.Object.FindObjectsOfType<UnityEngine.Camera>().Where(x => x.gameObject.tag == "MainCamera" && x.gameObject.activeSelf).FirstOrDefault();
        }

#if UNITY_EDITOR
        /// <summary>
        /// Find All Scene Type Objects (Editor Only)
        /// </summary>
        public static List<T> FindAllSceneObjects<T>() where T : UnityEngine.Object
        {
            return Resources.FindObjectsOfTypeAll<T>().Where(x => !EditorUtility.IsPersistent(x)).ToList();
        }
#endif

        /// <summary>
        /// Function to generate List of random integer numbers.
        /// </summary>
        public static List<int> RandomList(int min, int max, int count)
        {
            return Enumerable.Range(min, max).OrderBy(x => System.Guid.NewGuid()).Take(count).ToList();
        }

        /// <summary>
        /// Function to generate random integer number (No Duplicates).
        /// </summary>
        public static int RandomUnique(int min, int max, int last)
        {
            System.Random rnd = new System.Random();

            if (min + 1 < max)
            {
                return Enumerable.Range(min, max).OrderBy(x => rnd.Next()).Where(x => x != last).Take(1).Single();
            }
            else
            {
                return min;
            }
        }

        /// <summary>
        /// Check if value is in vector range.
        /// </summary>
        public static bool IsInRange(this Vector2 vector, float value)
        {
            return value > vector.x && value < vector.y;
        }

        /// <summary>
        /// Play OneShot Audio 2D
        /// </summary>
        public static AudioSource PlayOneShot2D(Vector3 position, AudioClip clip, float volume = 1f)
        {
            GameObject go = new GameObject("OneShotAudio");
            go.transform.position = position;
            AudioSource source = go.AddComponent<AudioSource>();
            source.spatialBlend = 0f;
            source.clip = clip;
            source.volume = volume;
            source.Play();
            UnityEngine.Object.Destroy(go, clip.length);
            return source;
        }

        /// <summary>
        /// Play OneShot Audio 2D
        /// </summary>
        public static AudioSource PlayOneShot3D(Vector3 position, AudioClip clip, float volume = 1f)
        {
            GameObject go = new GameObject("OneShotAudio");
            go.transform.position = position;
            AudioSource source = go.AddComponent<AudioSource>();
            source.spatialBlend = 1f;
            source.clip = clip;
            source.volume = volume;
            source.Play();
            UnityEngine.Object.Destroy(go, clip.length);
            return source;
        }

        /// <summary>
        /// Return full GameObject Inspector Path
        /// </summary>
        public static string GameObjectPath(this GameObject obj)
        {
            return string.Join("/", obj.GetComponentsInParent<Transform>().Select(t => t.name).Reverse().ToArray());
        }

        /// <summary>
        /// Get string between two chars
        /// </summary>
        public static string GetBetween(this string str, char start, char end)
        {
            string result = str.Split(new char[] { start, end })[1];
            return result;
        }

        /// <summary>
        /// Check if string contains character.
        /// </summary>
        public static bool ContainsChar(this string str, char ch)
        {
            return str.Any(x => char.IsLetter(x) && x.Equals(ch));
        }

        /// <summary>
        /// Get string between two chars (Regex)
        /// </summary>
        public static string RegexBetween(this string str, char start, char end)
        {
            Regex regex = new Regex($@"\{start}(.*?)\{end}");
            Match match = regex.Match(str);

            if (match.Success)
            {
                return match.Groups[1].Value;
            }

            return str;
        }

        /// <summary>
        /// Get strings between two chars (Regex)
        /// </summary>
        public static string[] RegexBetweens(this string str, char start, char end)
        {
            Regex regex = new Regex($@"\{start}(.*?)\{end}");
            MatchCollection collection = regex.Matches(str);
            
            if(collection.Count < 1)
            {
                return new string[0];
            }

            IEnumerable<string> match = collection.Cast<Match>().Select(x => x.Groups[1].Value);
            return match.ToArray();
        }

        /// <summary>
        /// Check if string between char match tag (Regex)
        /// </summary>
        public static bool RegexMatch(this string str, char start, char end, string tag)
        {
            Regex regex = new Regex($@"\{start}({tag})\{end}");
            Match result = regex.Match(str);
            return result.Success;
        }

        /// <summary>
        /// Replace string part inside two chars
        /// </summary>
        public static string ReplacePart(this string str, char start, char end, string replace)
        {
            string old = str.Substring(start, end - start + 1);
            return str.Replace(old, replace);
        }

        /// <summary>
        /// Replace string part inside two chars (Regex)
        /// </summary>
        public static string RegexReplace(this string str, char start, char end, string replace)
        {
            Regex regex = new Regex($@"\{start}([^\{end}]+)\{end}");
            string result = regex.Replace(str, replace);

            return result;
        }

        /// <summary>
        /// Replace tag inside two chars (Regex)
        /// </summary>
        public static string RegexReplaceTag(this string str, char start, char end, string tag, string replace)
        {
            Regex regex = new Regex($@"\{start}({tag})\{end}");
            string result = regex.Replace(str, replace);

            return result;
        }

        /// <summary>
        /// Check if string match specified word
        /// </summary>
        public static bool RegexMatchWord(this string str, string word)
        {
            Regex regex = new Regex($@"\b{word}\b", RegexOptions.Singleline | RegexOptions.IgnoreCase);
            return regex.IsMatch(str);
        }

        /// <summary>
        /// Get string with specified input between two chars
        /// </summary>
        public static string GetStringWithInput(this string str, char start, char end)
        {
            string result = string.Empty;

            if (CrossPlatformInput.IsInitialized)
            {
                char[] stringCh = str.ToCharArray();

                if (stringCh.Contains(start) && stringCh.Contains(end))
                {
                    string key = CrossPlatformInput.Instance.ControlOf(str.RegexBetween(start, end)).Control;
                    result = str.RegexReplace(start, end, key);
                }
            }

            return result;
        }

        /// <summary>
        /// Get string with specified input between two chars, with before and after separators
        /// </summary>
        public static string GetStringWithInput(this string str, char start, char end, char before, char after)
        {
            string result = string.Empty;

            if (CrossPlatformInput.IsInitialized)
            {
                char[] stringCh = str.ToCharArray();

                if (stringCh.Contains(start) && stringCh.Contains(end))
                {
                    string key = CrossPlatformInput.Instance.ControlOf(str.RegexBetween(start, end)).Control;
                    result = str.RegexReplace(start, end, before + key + after);
                }
            }

            return result;
        }

        /// <summary>
        /// Check if string contains character with any case.
        /// </summary>
        public static bool ContainsAnyCase(this string haystack, char needle)
        {
            return haystack.IndexOf(needle, (int)System.StringComparison.CurrentCultureIgnoreCase) != -1;
        }

        /// <summary>
        /// Get Titled Case text
        /// </summary>
        public static string TitleCase(this string str)
        {
            CultureInfo cultureInfo = Thread.CurrentThread.CurrentCulture;
            TextInfo textInfo = cultureInfo.TextInfo;
            return textInfo.ToTitleCase(str);
        }

        /// <summary>
        /// Clamp Vector3
        /// </summary>
        public static Vector3 Clamp(this Vector3 vector, float min, float max)
        {
            return new Vector3(Mathf.Clamp(vector.x, min, max), Mathf.Clamp(vector.y, min, max), Mathf.Clamp(vector.z, min, max));
        }

        /// <summary>
        /// Get Random Value from Min/Max Vector.
        /// </summary>
        public static float Random(this Vector2 vector)
        {
            return UnityEngine.Random.Range(vector.x, vector.y);
        }

        /// <summary>
        /// Get Signed Angle between two Targets [-180, 180]
        /// </summary>
        public static int RealSignedAngle(this Transform tr, Vector3 other)
        {
            Vector3 relative = tr.InverseTransformPoint(other);
            return Mathf.RoundToInt(Mathf.Atan2(relative.x, relative.z) * Mathf.Rad2Deg);
        }

        /// <summary>
        /// Correct the Angle
        /// </summary>
        public static float FixAngle(float angle)
        {
            if (angle < -360F)
                angle += 360F;
            if (angle > 360F)
                angle -= 360F;

            return angle;
        }

        /// <summary>
        /// Get game data folder.
        /// </summary>
        public static string GetFolderPath(FilePath filepath, bool root = false)
        {
            if (root)
            {
                if (filepath == FilePath.GameDataPath)
                {
                    return Application.dataPath;
                }
                else
                {
                    return Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
                }
            }
            else
            {
                if (filepath == FilePath.GameDataPath)
                {
                    return Application.dataPath + "/Data/";
                }
                else
                {
                    return Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + "/" + Application.productName + "/";
                }
            }
        }

        /// <summary>
        /// Function to encode bytes to a short version of Base64.
        /// </summary>
        public static string EncodeBase64Short(byte[] bytes)
        {
            string encoded = Convert.ToBase64String(bytes);
            encoded = encoded.Replace("/", "_").Replace("+", "-").Replace("=", "");
            return encoded;
        }

        /// <summary>
        /// Function to decode short version of Base64 to a string.
        /// </summary>
        public static string DecodeBase64Short(string value)
        {
            value = value.Replace("_", "/").Replace("-", "+");

            switch (value.Length % 4)
            {
                case 2:
                    value += "==";
                    break;
                case 3:
                    value += "=";
                    break;
            }

            byte[] buffer = Convert.FromBase64String(value);
            return System.Text.Encoding.UTF8.GetString(buffer);
        }

        /// <summary>
        /// Get all saveable components from GameObject
        /// </summary>
        public static SaveableDataPair[] GetSaveables(GameObject obj)
        {
            MonoBehaviour[] scripts = obj.GetComponents<MonoBehaviour>();

            var saveablesQuery = from Instance in scripts
                                 where typeof(ISaveable).IsAssignableFrom(Instance.GetType()) && !Instance.GetType().IsInterface && !Instance.GetType().IsAbstract
                                 let key = string.Format("{0}_{1}", Instance.GetType().Name, System.Guid.NewGuid().ToString("N"))
                                 select new SaveableDataPair(SaveableDataPair.DataBlockType.ISaveable, key, Instance, new string[0]);

            var attributesQuery = from Instance in scripts
                                  let attr = Instance.GetType().GetFields().Where(field => field.GetCustomAttributes(typeof(SaveableField), false).Count() > 0 && !field.IsLiteral && field.IsPublic).Select(fls => fls.Name).ToArray()
                                  let key = string.Format("{0}_{1}", Instance.GetType().Name, System.Guid.NewGuid().ToString("N"))
                                  where attr.Count() > 0
                                  select new SaveableDataPair(SaveableDataPair.DataBlockType.Attribute, key, Instance, attr);

            if (attributesQuery.Count() > 0)
            {
                var pairs = saveablesQuery.Union(attributesQuery);

                if (pairs.Count() > 0)
                {
                    return pairs.ToArray();
                }
            }
            else if(saveablesQuery.Count() > 0)
            {
                return saveablesQuery.ToArray();
            }

            Debug.LogError("There are no saveables in specified object!");

            return null;
        }

        /// <summary>
        /// Get terrain texture from position
        /// </summary>
        public static Texture2D TerrainPosToTex(Terrain terrain, Vector3 worldPos)
        {
            float[] mix = TerrainTextureMix(terrain, worldPos);
            float maxMix = 0;
            int maxIndex = 0;

            for (int n = 0; n < mix.Length; n++)
            {
                if (mix[n] > maxMix)
                {
                    maxIndex = n;
                    maxMix = mix[n];
                }
            }

            return terrain.terrainData.terrainLayers[maxIndex].diffuseTexture;
        }

        static float[] TerrainTextureMix(Terrain terrain, Vector3 worldPos)
        {
            TerrainData terrainData = terrain.terrainData;
            Vector3 terrainPos = terrain.transform.position;

            int mapX = (int)(((worldPos.x - terrainPos.x) / terrainData.size.x) * terrainData.alphamapWidth);
            int mapZ = (int)(((worldPos.z - terrainPos.z) / terrainData.size.z) * terrainData.alphamapHeight);

            float[,,] splatmapData = terrainData.GetAlphamaps(mapX, mapZ, 1, 1);
            float[] cellMix = new float[splatmapData.GetUpperBound(2) + 1];

            for (int n = 0; n < cellMix.Length; n++)
            {
                cellMix[n] = splatmapData[0, 0, n];
            }

            return cellMix;
        }
    }
}
