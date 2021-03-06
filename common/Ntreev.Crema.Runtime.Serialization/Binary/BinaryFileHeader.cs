﻿//Released under the MIT License.
//
//Copyright (c) 2018 Ntreev Soft co., Ltd.
//
//Permission is hereby granted, free of charge, to any person obtaining a copy of this software and associated 
//documentation files (the "Software"), to deal in the Software without restriction, including without limitation the 
//rights to use, copy, modify, merge, publish, distribute, sublicense, and/or sell copies of the Software, and to permit 
//persons to whom the Software is furnished to do so, subject to the following conditions:
//
//The above copyright notice and this permission notice shall be included in all copies or substantial portions of the 
//Software.
//
//THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE 
//WARRANTIES OF MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR 
//COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR 
//OTHERWISE, ARISING FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE SOFTWARE.

using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace Ntreev.Crema.Runtime.Serialization.Binary
{
    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    public struct BinaryFileHeader
    {
        public const uint DefaultMagicValueObsolete = 0x8d31269e;
        public const uint DefaultMagicValue = 0x03050000;

        public uint MagicValue { get; set; }

        public int Revision { get; set; }

        public int TypesHashValue { get; set; }

        public int TablesHashValue { get; set; }

        public int Tags { get; set; }

        public int Reserved { get; set; }

        public int TableCount { get; set; }

        public int Name { get; set; }

        public long IndexOffset { get; set; }

        public long TablesOffset { get; set; }

        public long StringResourcesOffset { get; set; }
    }
}
