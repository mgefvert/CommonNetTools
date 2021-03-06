﻿using System;
using System.Collections.Generic;

// ReSharper disable UnusedMember.Global

namespace DotNetCommons.Logging.LogMethods
{
    public class BufferLogger : ILogMethod
    {
        private readonly object _lock = new object();
        private readonly List<LogEntry> _buffer = new List<LogEntry>();
        private readonly int _count;
        private readonly TimeSpan _time;
        private DateTime? _start;

        public BufferLogger() : this(1000, TimeSpan.FromMinutes(1))
        {
        }

        public BufferLogger(int? maxCount, TimeSpan? maxTime)
        {
            _count = maxCount ?? int.MaxValue;
            _time = maxTime ?? TimeSpan.MaxValue;
        }

        private bool BufferExceeded => _buffer.Count >= _count || DateTime.Now - (_start ?? DateTime.Now) > _time;

        public IReadOnlyList<LogEntry> Handle(IReadOnlyList<LogEntry> entries, bool flush)
        {
            if (_start == null)
                _start = DateTime.Now;

            lock (_lock)
            {
                _buffer.AddRange(entries);

                var result = new List<LogEntry>();
                if (flush || BufferExceeded)
                {
                    result.AddRange(_buffer);
                    _buffer.Clear();
                    _start = null;
                }

                return result;
            }
        }
    }
}
