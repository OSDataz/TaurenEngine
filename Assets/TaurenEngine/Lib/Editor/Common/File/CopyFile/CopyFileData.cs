/*┌────────────────────────┐
 *│　Engine  ：TaurenEngine
 *│　Author  ：Osdataz
 *│　Version ：v0.3.0
 *│　Time    ：2021/10/8 20:43:20
 *└────────────────────────┘*/

using System;
using System.Collections.Generic;
using UnityEngine;

namespace TaurenEditor.Lib.Common
{
    public sealed class CopyFileData : ScriptableObject
    {
        public List<CopyFileGroup> groups;
    }

    [Serializable]
    public sealed class CopyFileGroup
    {
        public bool showDetails;
        public string name;
        public string updateTime;
        public List<CopyFileItem> items;
    }

    [Serializable]
    public sealed class CopyFileItem
    {
        public string copyPath;
        public string pastePath;
    }
}