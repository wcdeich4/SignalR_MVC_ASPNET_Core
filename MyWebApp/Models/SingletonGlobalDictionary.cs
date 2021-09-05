using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MyWebApp.Models
{
    public sealed class SingletonGlobalDictionary : Dictionary<string, object>
    {
        private static SingletonGlobalDictionary instance = null;
        private static readonly object syncRoot = new object();
        private SingletonGlobalDictionary()
        {

        }

        public static SingletonGlobalDictionary Instance
        {
            get
            {
                lock(syncRoot)
                {
                    if (instance == null)
                    {
                        instance = new SingletonGlobalDictionary();
                    }
                    return instance;
                }
            }
        }

        public void AddOrUpdate(string key, object newValue)
        {
            if (this.ContainsKey(key))
            {
                this[key] = newValue;
            }
            else
            {
                this.Add(key, newValue);
            }
        }

    }
}