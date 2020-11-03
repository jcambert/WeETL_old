using System;
using System.Collections.Generic;
using System.Text;

namespace WeETL.ConsoleApp
{
    public class TestSchema1
    {
        private static string PadRight(string value, int length = 10) => value?.Trim().PadRight(length) ?? "NULL".PadRight(length);

        [Key]public Guid UniqueId { get; set; }
        public string TextColumn1 { get; set; }
        public string TextColumn2 { get; set; }

        public string TextColumn3 { get; set; }

        public override string ToString() => $" {UniqueId} | {PadRight( TextColumn1)}| {PadRight( TextColumn2)}| {PadRight(TextColumn3)}";
    }
    public class TestSchema2
    {
        private static string PadRight(string value, int length = 10) => value?.Trim().PadRight(length) ?? "NULL".PadRight(length);

        [Key]
        public Guid UniqueId { get; set; }

        public string TextColumn4 { get; set; }
        public override string ToString() => $" {UniqueId} | {PadRight(TextColumn4)}";

    }

    public class TestSchema3
    {
        private static string PadRight(string value, int length = 10) => value?.Trim().PadRight(length) ?? "NULL".PadRight(length);

        [Key]
        public Guid UniqueId { get; set; }

        public string TextColumn5 { get; set; }
        public override string ToString() => $" {UniqueId} | {PadRight(TextColumn5)}";

    }
}
