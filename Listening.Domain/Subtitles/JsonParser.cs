﻿using Listening.Domain.ValueObjets;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Listening.Domain.Subtitles
{
    class JsonParser : ISubtitleParser
    {
        public bool Accept(string typeName)
        {
            return typeName.Equals("json", StringComparison.OrdinalIgnoreCase);
        }

        public IEnumerable<Sentence> Parse(string subtitle)
        {
            return JsonSerializer.Deserialize<IEnumerable<Sentence>>(subtitle);
        }
    }
}
