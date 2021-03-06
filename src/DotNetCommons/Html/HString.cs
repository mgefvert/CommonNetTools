﻿using System;
using System.Collections.Generic;
using System.Net;

// ReSharper disable UnusedMember.Global

namespace DotNetCommons.Html
{
    public class HString : HElement, IEqualityComparer<HString>
    {
        private string _html;

        protected HString()
        {
        }

        /// <summary>
        /// Return a new string from raw HTML text without escaping.
        /// </summary>
        /// <param name="html"></param>
        /// <returns></returns>
        public static HString Raw(string html)
        {
            return new HString { _html = html };
        }

        /// <summary>
        /// Return a new string from text, escaping it to HTML sequences.
        /// </summary>
        /// <param name="html"></param>
        /// <returns></returns>
        public static HString Encode(string html)
        {
            return new HString { _html = WebUtility.HtmlEncode(html) };
        }

        public HString Copy()
        {
            return new HString { _html = _html };
        }

        public override string Render()
        {
            return _html;
        }

        public override string ToString()
        {
            return _html;
        }

        public static implicit operator HString(string text)
        {
            return Encode(text);
        }

        public static HString operator +(HString h1, HString h2)
        {
            return new HString { _html = h1._html + h2._html };
        }

        public bool Equals(HString x, HString y)
        {
            return string.Equals(x?._html, y?._html);
        }

        public int GetHashCode(HString obj)
        {
            return obj._html.GetHashCode();
        }
    }
}
