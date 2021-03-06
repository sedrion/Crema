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

using System.ComponentModel;
using System.Runtime.Serialization;
using System.Xml;
using System.Xml.Serialization;
using Ntreev.Crema.Data.Xml;
using System.Collections.Generic;
using System;
using Ntreev.Library;
using System.Xml.Schema;
using Ntreev.Library.Serialization;
using System.Text;
using System.IO;

namespace Ntreev.Crema.ServiceModel
{
    [DataContract(Namespace = SchemaUtility.Namespace)]
    [XmlInclude(typeof(DBNull))]
    [XmlInclude(typeof(TimeSpan))]
    public struct DomainLocationInfo
    {
        private object[] keys;

        [XmlElement]
        public string TableName { get; set; }

        [XmlElement]
        public string ColumnName { get; set; }

        [XmlIgnore]
        public object[] Keys
        {
            get { return this.keys; }
            set { this.keys = value; }
        }

        public static readonly DomainLocationInfo Empty = new DomainLocationInfo() { Keys = new object[] { } };

        public static bool operator ==(DomainLocationInfo left, DomainLocationInfo right)
        {
            return left.Equals(right);
        }

        public static bool operator !=(DomainLocationInfo left, DomainLocationInfo right)
        {
            return !(left == right);
        }

        public override bool Equals(object obj)
        {
            if (!(obj is DomainLocationInfo))
            {
                return false;
            }
            var location = (DomainLocationInfo)obj;
            return HashUtility.Equals(location.Keys, this.Keys) &&
                   location.TableName == this.TableName &&
                   location.ColumnName == this.ColumnName;
        }

        public override int GetHashCode()
        {
            return HashUtility.GetHashCode(this.Keys, this.ColumnName, this.TableName);
        }

        #region DataMember

        [XmlElement]
        [EditorBrowsable(EditorBrowsableState.Never)]
        public string ItemXml
        {
            get
            {
                var settings = new XmlWriterSettings()
                {
                    ConformanceLevel = ConformanceLevel.Fragment,
                    Encoding = Encoding.UTF8,
                    Indent = true,
                };

                using (var sw = new Ntreev.Library.IO.Utf8StringWriter())
                using (var writer = XmlWriter.Create(sw, settings))
                {
                    DomainRowInfo.WriteFields(writer, nameof(Keys), this.keys);

                    writer.Close();
                    return sw.ToString();
                }
            }
            set
            {
                var settings = new XmlReaderSettings()
                {
                    ConformanceLevel = ConformanceLevel.Fragment,
                };

                using (var sr = new StringReader(value))
                using (var reader = XmlReader.Create(sr, settings))
                {
                    this.keys = DomainRowInfo.ReadFields(reader, nameof(Keys));
                }
            }
        }

        [DataMember]
        [XmlIgnore]
        private string Xml
        {
            get { return XmlSerializerUtility.GetString(this); }
            set { this = XmlSerializerUtility.ReadString(this, value); }
        }

        #endregion
    }
}
