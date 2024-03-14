using MyPlatform.Model.Data.Properties;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MyPlatform.Model.Data.Properties
{
    public abstract class PrefsPersistentProperty<TPropertyType> : PersistentProperty<TPropertyType>
    {
        protected string Key;
        protected PrefsPersistentProperty(TPropertyType defaultValue, string key) : base(defaultValue) 
        {
            Key = key;
        }
    }
}

