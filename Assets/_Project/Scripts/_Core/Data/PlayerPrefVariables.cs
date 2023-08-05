using System.Collections.Generic;
using UnityEngine;

namespace HOT.Data
{
    public static class PlayerPrefVariables
    {
        public class PrefVar
        {
            protected string Key;
            int? intContainer;
            bool? boolContainer;

            static PrefStringDictionary longToShortKeyMap = new PrefStringDictionary("shortKeyMap", null);

            #region Constructors

            public PrefVar(string k)
            {
                Key = NormalizeKey(k);
            }

            public PrefVar(string k, int value)
            {
                Key = NormalizeKey(k);
                Int = PlayerPrefs.HasKey(Key) ? PlayerPrefs.GetInt(Key) : value;
            }

            public PrefVar(string k, bool value)
            {
                Key = NormalizeKey(k);
                Bool = PlayerPrefs.HasKey(Key) ? PlayerPrefs.GetInt(Key) > 0 : value;
            }

            public PrefVar(string k, string value)
            {
                Key = NormalizeKey(k);
                String = PlayerPrefs.HasKey(Key) ? PlayerPrefs.GetString(Key) : value;
            }

            public PrefVar(string k, float value)
            {
                Key = NormalizeKey(k);
                if (!PlayerPrefs.HasKey(Key))
                    Float = value;
            }

            #endregion

            public int Int
            {
                get
                {
                    if (!intContainer.HasValue)
                        intContainer = PlayerPrefs.HasKey(Key) ? PlayerPrefs.GetInt(Key) : 0;
                    return intContainer.Value;
                }
                set
                {
                    intContainer = value;
                    PlayerPrefs.SetInt(Key, intContainer.Value);
                    PlayerPrefs.Save();
                }
            }

            public bool Bool
            {
                get
                {
                    if (!boolContainer.HasValue)
                        boolContainer = PlayerPrefs.HasKey(Key) && PlayerPrefs.GetInt(Key) > 0;
                    return boolContainer.Value;
                }
                set
                {
                    boolContainer = value;
                    PlayerPrefs.SetInt(Key, boolContainer.Value ? 1 : 0);
                    PlayerPrefs.Save();
                }
            }

            public string String
            {
                get { return PlayerPrefs.HasKey(Key) ? PlayerPrefs.GetString(Key) : string.Empty; }
                set
                {
                    PlayerPrefs.SetString(Key, value);
                    PlayerPrefs.Save();
                }
            }

            public float Float
            {
                get { return PlayerPrefs.HasKey(Key) ? PlayerPrefs.GetFloat(Key) : 0f; }
                set
                {
                    PlayerPrefs.SetFloat(Key, value);
                    PlayerPrefs.Save();
                }
            }

            string NormalizeKey(string key)
            {
                return key;
            }
        }

        public class PrefBoolsArr : PrefVar
        {
            public PrefBoolsArr(string key) : base(key, 0)
            {
            }

            public bool this[int index]
            {
                get
                {
                    if (index < 0 || index >= 8)
                        throw new System.Exception("Index out of range.");
                    return (Int & (1 << index)) != 0;
                }
                set
                {
                    if (index < 0 || index >= 8)
                        throw new System.Exception("Index out of range.");
                    if (value)
                        Int = Int | (1 << index);
                    else
                        Int = Int & ~(1 << index);
                }
            }
        }

        public class PrefIntDictionary : PrefDictionary<int>
        {
            public Dictionary<string, int> IntDictionary
            {
                get { return Dictionary; }
                set { Dictionary = value; }
            }

            public PrefIntDictionary(string key, Dictionary<string, int> dictionary) : base(key, dictionary)
            {
            }

            protected override int ParseValue(string value)
            {
                int.TryParse(value, out int result);
                return result;
            }
        }
        
        public class PrefDateTime : PrefVar
        {
            System.DateTime? container;

            public PrefDateTime(string key, System.DateTime value) : base(key)
            {
                DateTime = PlayerPrefs.HasKey(Key) ? ParseFromPlayerPrefs() : value;
            }

            public System.DateTime DateTime
            {
                get
                {
                    if (!container.HasValue)
                        container = ParseFromPlayerPrefs();
                    return container.Value;
                }
                set
                {
                    container = value;
                    PlayerPrefs.SetInt(Key, container.Value.ToUnixTimestamp());
                }
            }

            System.DateTime ParseFromPlayerPrefs()
            {
                if (PlayerPrefs.HasKey(Key))
                    return DateUtils.FromUnixTimestamp(PlayerPrefs.GetInt(Key));
                return System.DateTime.MinValue;
            }
        }

        public class PrefStringDictionary : PrefDictionary<string>
        {
            public PrefStringDictionary(string key, Dictionary<string, string> dictionary) : base(key, dictionary)
            {
            }

            protected override string ParseValue(string value)
            {
                return value;
            }
        }

        public abstract class PrefDictionary<TValue> : PrefVar
        {
            const char SEPARATOR = '|';
            const char KEY_VALUE_SEPARATOR = ';';

            bool setted;
            Dictionary<string, TValue> intDictionary;

            public PrefDictionary(string key, Dictionary<string, TValue> dictionary) : base(key)
            {
                Dictionary = ParseFromPlayerPrefs(dictionary);
            }

            public Dictionary<string, TValue> Dictionary
            {
                get
                {
                    if (!setted)
                    {
                        intDictionary = ParseFromPlayerPrefs();
                        setted = true;
                    }

                    return intDictionary;
                }
                set
                {
                    if (value != null)
                    {
                        intDictionary = value;
                        setted = true;
                        string[] keyValuePairs = new string[intDictionary.Count];
                        int index = 0;
                        foreach (var keyValuePair in intDictionary)
                        {
                            keyValuePairs[index] = keyValuePair.Key + KEY_VALUE_SEPARATOR + keyValuePair.Value;
                            index++;
                        }

                        string result = string.Join(SEPARATOR.ToString(), keyValuePairs);
                        PlayerPrefs.SetString(Key, result);
                    }
                }
            }

            public void AddKeyValuePair(string key, TValue value)
            {
                Dictionary<string, TValue> result = new Dictionary<string, TValue>();
                foreach (KeyValuePair<string, TValue> keyValuePair in Dictionary)
                    result.Add(keyValuePair.Key, keyValuePair.Value);

                if (result.ContainsKey(key))
                    result[key] = value;
                else
                    result.Add(key, value);
                Dictionary = result;
            }

            Dictionary<string, TValue> ParseFromPlayerPrefs(Dictionary<string, TValue> dictionary = null)
            {
                var result = new Dictionary<string, TValue>();
                if (!PlayerPrefs.HasKey(Key))
                    return dictionary ?? new Dictionary<string, TValue>();

                string str = PlayerPrefs.GetString(Key);
                if (!string.IsNullOrEmpty(str))
                {
                    var dictionaryStr = str.Split(SEPARATOR);
                    foreach (string keyPairValue in dictionaryStr)
                    {
                        string[] splitedKeyValue = keyPairValue.Split(KEY_VALUE_SEPARATOR);
                        string key = splitedKeyValue[0];
                        TValue value = ParseValue(splitedKeyValue[1]);
                        result.Add(key, value);
                    }
                }

                return result;
            }

            protected abstract TValue ParseValue(string value);
        }
    }
}