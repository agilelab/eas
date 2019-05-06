using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.Serialization;
using System.Collections;

namespace EAS.SilverlightClient
{
    [Serializable()]
    class DictionaryEx<TKey, TValue> : Dictionary<TKey, TValue>
    {
        /// <summary>
        /// 初始化 DictionaryEx 类的新实例。
        /// </summary>
        public DictionaryEx():base()
        {

        }

        /// <summary>
        /// 初始化 DictionaryEx 类的新实例，该实例包含从指定的 IDictionary。
        /// </summary>
        /// <param name="dictionary">IDictionary，它的元素被复制到新的 DictionaryEx。</param>
        ///<exception cref="System.ArgumentNullException">dictionary 为 null。</exception>
        public DictionaryEx(IDictionary<TKey, TValue> dictionary):base(dictionary)
        {

        }

        /// <summary>
        /// 初始化 DictionaryEx类的新实例，并使用指定的IEqualityComparer。
        /// </summary>
        /// <param name="comparer">比较键时要使用的 IEqualityComparer 实现，或者为 null，以便为键类型使用默认的IEqualityComparer。</param>
        public DictionaryEx(IEqualityComparer<TKey> comparer):base(comparer)
        {

        }

        /// <summary>
        /// 初始化 DictionaryEx 类的新实例，该实例为空且具有指定的初始容量。
        /// </summary>
        /// <param name="capacity">DictionaryEx 可包含的初始元素数。</param>
        public DictionaryEx(int capacity):base(capacity)
        {

        }

        /// <summary>
        /// 初始化 DictionaryEx 类的新实例，该实例包含从指定的 IDictionary中复制的元素并使用指定的 IEqualityComparer。
        /// </summary>
        /// <param name="dictionary">IDictionary,它的元素被复制到新的 DictionaryEx。</param>
        /// <param name="comparer">比较键时要使用的 IEqualityComparer 实现。</param>
        ///<exception cref="System.ArgumentNullException">dictionary 为 null。</exception> 
        public DictionaryEx(IDictionary<TKey, TValue> dictionary, IEqualityComparer<TKey> comparer):base(dictionary,comparer)
        {

        }

        /// <summary>
        /// 初始化 DictionaryEx 类的新实例，该实例为空且具有指定的初始容量，并使用指定的 IEqualityComparer。
        /// </summary>
        /// <param name="capacity">DictionaryEx可包含的初始元素数。</param>
        /// <param name="comparer">比较键时要使用的 IEqualityComparer实现。</param>
        ///<exception cref="System.ArgumentNullException">dictionary 为 null。</exception> 
        public DictionaryEx(int capacity, IEqualityComparer<TKey> comparer):base(capacity,comparer)
        {

        }

        /// <summary>
        /// 从 DictionaryEx 中移除所指定索引的值。
        /// </summary>
        /// <param name="index">索引位置。</param>
        /// <returns>如果成功找到并移除该元素，则为 true；否则为 false。 如果在 Dictionary中没有找到 key/Value，此方法则返回 false。</returns>
        public bool Remove(int index)
        {
            if ((index >= this.Keys.Count) | (index < 0))
                throw new ArgumentOutOfRangeException("index");

            return this.Remove(this.GetKey(index));
        }

        /// <summary>
        /// 获取或设置指定索引处的元素。
        /// </summary>
        /// <param name="index">要获得或设置的元素从零开始的索引。</param>
        /// <exception cref="System.ArgumentOutOfRangeException">index 小于 0。- 或 -index 等于或大于索引长度。</exception> 
        /// <returns>指定索引处的元素。</returns>
        public TValue this[int index]
        {
            get
            {
                if ((index >= this.Keys.Count) |(index <0))
                    throw new ArgumentOutOfRangeException("index");
 
                return base[this.GetKey(index)];
            }
            set
            {
                if ((index >= this.Keys.Count) |(index <0))
                    throw new ArgumentOutOfRangeException("index");

                base[this.GetKey(index)] = value;
            }
        }

        /// <summary>
        /// 根据索引访问值。
        /// </summary>
        /// <param name="index">索引位置。</param>
        /// <returns>值。</returns>
        public TKey GetKey(int index)
        {
            if ((index >= this.Keys.Count) |(index <0))
                throw new ArgumentOutOfRangeException("index");

            IEnumerator<TKey> enumerator = this.Keys.GetEnumerator();
            enumerator.Reset();
            int i= 0;
            while (enumerator.MoveNext())
            {
                if(i==index)
                    return enumerator.Current;

                i++;
            }

            return default(TKey);
        }
    }
}
