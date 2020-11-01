using System;
using System.Collections.Generic;
using System.Text;

namespace WeETL.ConsoleApp
{
    public class TestSchema
    {
        private static string PadRight(string value, int length = 10) => value?.Trim().PadRight(length) ?? "NULL".PadRight(length);

        public Guid UniqueId { get; set; }
        public string TextColumn1 { get; set; }
        public string TextColumn2 { get; set; }

        public string TextColumn3 { get; set; }

        public override string ToString() => $" {UniqueId} | {PadRight( TextColumn1)}| {PadRight( TextColumn2)}| {PadRight(TextColumn3)}";
    }
}
