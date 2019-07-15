﻿using System.Collections.Generic;
using Perjure.PurgeRules;

namespace Perjure
{
    public class Configuration
    {
        public bool IncludeRecycleBin { get; set; }
        public List<FilePurgeRule> PurgeRules { get; set; }

        public IEnumerable<IPurgeRule> GetAllPurgeRules()
        {
            if (IncludeRecycleBin)
            {
                yield return new RecycleBinPurgeRule();
            }

            if (PurgeRules != null)
            {
                foreach (var purgeRule in PurgeRules)
                {
                    yield return purgeRule;
                }
            }
        }
    }
}
