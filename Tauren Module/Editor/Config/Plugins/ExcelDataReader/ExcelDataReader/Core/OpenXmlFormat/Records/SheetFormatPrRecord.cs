﻿namespace ExcelDataReader.Core.OpenXmlFormat.Records
{
    internal sealed class SheetFormatPrRecord : Record
    {
        public SheetFormatPrRecord(double? defaultRowHeight)
        {
            DefaultRowHeight = defaultRowHeight;
        }

        public double? DefaultRowHeight { get; }
    }
}
