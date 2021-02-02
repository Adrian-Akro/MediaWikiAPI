using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MediaWikiApi.Requests {
    public class QueryString : IEnumerable<KeyValuePair<string, (ParamType, string)>> {
        Dictionary<string, LinkedList<string>> listValuesDict;
        Dictionary<string, string> singleValuesDict;

        public QueryString() {
            listValuesDict = new Dictionary<string, LinkedList<string>>();
            singleValuesDict = new Dictionary<string, string>();
        }

        public bool TryGetList(string key, out string[] values) {
            bool result = listValuesDict.TryGetValue(
                key,
                out LinkedList<string> lList
                );
            if (lList != null) {
                values = new string[lList.Count];
                lList.CopyTo(values, 0);
            } else {
                values = null;
            }
            return result;
        }

        public bool TryGet(string key, out string value) {
            bool result = singleValuesDict.TryGetValue(
                key,
                out value
                );
            return result;
        }

        public void Add(string key, string value) {
            if (listValuesDict.ContainsKey(key)) throw new InvalidOperationException($"A key with this value already exists for the type {ParamType.List}");
            if (singleValuesDict.ContainsKey(key))
                Update(key, value);
            else
                singleValuesDict.Add(key, value);
        }

        private void Update(string key, string value) {
            singleValuesDict[key] = value;
        }

        public void AddList(string key, string value) {
            if (singleValuesDict.ContainsKey(key)) throw new InvalidOperationException($"A key with this value already exists for the type {ParamType.Single}");
            if (listValuesDict.ContainsKey(key))
                UpdateList(key, value);
            else {
                LinkedList<string> lList = new LinkedList<string>();
                lList.AddLast(value);
                listValuesDict.Add(key, lList);
            }
        }

        private void UpdateList(string key, string value) {
            listValuesDict[key].AddLast(value);
        }

        public bool RemoveKey(string key) {
            if (!listValuesDict.Remove(key))
                return singleValuesDict.Remove(key);
            return true;
        }
        public bool ContainsKey(string key) {
            if (!listValuesDict.ContainsKey(key))
                return singleValuesDict.ContainsKey(key);
            return true;
        }

        public override string ToString() {
            StringBuilder strBuilder = new StringBuilder("?");

            if (singleValuesDict.Count > 0) {
                KeyValuePair<string, string> firstSingleKvp = singleValuesDict.First();
                strBuilder.Append($"{firstSingleKvp.Key}={firstSingleKvp.Value}");
                foreach (KeyValuePair<string, string> kvp in singleValuesDict.Skip(1)) {
                    strBuilder.Append($"&{kvp.Key}={kvp.Value}");
                }
            }

            if (listValuesDict.Count > 0) {
                KeyValuePair<string, LinkedList<string>> firstListKvp = listValuesDict.First();
                if (strBuilder.Length > 1) {
                    strBuilder.Append("&");
                }
                strBuilder.Append($"{firstListKvp.Key}={firstListKvp.Value.First.Value}");
                foreach (string str in firstListKvp.Value.Skip(1)) {
                    strBuilder.Append($"&{firstListKvp.Key}={str}");
                }
                foreach (KeyValuePair<string, LinkedList<string>> kvp in listValuesDict.Skip(1)) {
                    foreach (string str in kvp.Value) {
                        strBuilder.Append($"&{kvp.Key}={str}");
                    }
                }
            }

            return strBuilder.Length > 1 ? strBuilder.ToString() : "";
        }

        public IEnumerator<KeyValuePair<string, (ParamType, string)>> GetEnumerator() {
            return new Enumerator(this);
        }

        IEnumerator IEnumerable.GetEnumerator() {
            return GetEnumerator();
        }

        class Enumerator : IEnumerator<KeyValuePair<string, (ParamType, string)>> {
            private QueryString qs;
            private Dictionary<string, string>.Enumerator singleEnumerator;
            private Dictionary<string, LinkedList<string>>.Enumerator listEnumerator;
            private LinkedList<string>.Enumerator innerListEnumerator;

            public Enumerator(QueryString queryString) {
                qs = queryString;
                listEnumerator = qs.listValuesDict.GetEnumerator();
                singleEnumerator = qs.singleValuesDict.GetEnumerator();
                innerListEnumerator = new LinkedList<string>().GetEnumerator();
            }

            public KeyValuePair<string, (ParamType, string)> Current { get; private set; }

            object IEnumerator.Current => Current;

            public void Dispose() {
                listEnumerator.Dispose();
                singleEnumerator.Dispose();
                innerListEnumerator.Dispose();
            }

            public bool MoveNext() {
                if (singleEnumerator.MoveNext()) {
                    Current = new KeyValuePair<string, (ParamType, string)>(
                        singleEnumerator.Current.Key,
                        (ParamType.Single, singleEnumerator.Current.Value)
                        );
                } else {
                    if (!innerListEnumerator.MoveNext()) {
                        if (listEnumerator.MoveNext()) {
                            innerListEnumerator = listEnumerator.Current.Value.GetEnumerator();
                            if (!innerListEnumerator.MoveNext()) return false;
                        } else {
                            return false;
                        }
                    }
                    Current = new KeyValuePair<string, (ParamType, string)>(
                        listEnumerator.Current.Key,
                        (ParamType.List, innerListEnumerator.Current)
                        );
                }
                return true;
            }

            public void Reset() {
                listEnumerator = qs.listValuesDict.GetEnumerator();
                singleEnumerator = qs.singleValuesDict.GetEnumerator();
                innerListEnumerator = new LinkedList<string>().GetEnumerator();
            }
        }
    }



    public enum ParamType {
        List,
        Single
    }
}
