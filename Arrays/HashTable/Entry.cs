﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Structures.HashTable
{
    internal class Entry
    {
        public string Key {  get; set; }
        public int Value { get; set; }

        public Entry(string key, int value) 
        {
            Key = key;
            Value = value;
        }
    }
}
