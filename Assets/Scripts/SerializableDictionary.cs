using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 시리얼라이즈 가능한 제네릭 딕셔너리 클래스
[System.Serializable]
public class SerializableDictionary<TKey, TValue> : Dictionary<TKey, TValue>, ISerializationCallbackReceiver
{
    [SerializeField]
    private List<TKey> keys = new List<TKey>(); // 딕셔너리 키를 저장하는 리스트

    [SerializeField]
    private List<TValue> values = new List<TValue>(); // 딕셔너리 값들을 저장하는 리스트

    // 딕셔너리를 리스트로 변환하여 직렬화 직전에 호출됨
    public void OnBeforeSerialize()
    {
        keys.Clear();
        values.Clear();
        foreach (KeyValuePair<TKey, TValue> pair in this)
        {
            keys.Add(pair.Key);
            values.Add(pair.Value);
        }
    }

    // 리스트로부터 딕셔너리를 복원하여 역직렬화 이후에 호출됨
    public void OnAfterDeserialize()
    {
        this.Clear();

        // 키와 값의 개수가 일치하지 않으면 예외 발생
        if (keys.Count != values.Count)
            throw new Exception("There are " + keys.Count + " keys and " + values.Count + " values after deserialization. Make sure that both key and value types are serializable.");

        // 리스트에서 딕셔너리로 데이터 복원
        for (int i = 0; i < keys.Count; i++)
            this.Add(keys[i], values[i]);
    }
}
