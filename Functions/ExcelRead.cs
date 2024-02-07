using System;
using System.Collections.Generic;
using ExcelDataReader;

namespace IIABackend
{
    /// <summary>
    /// Read every excel
    /// </summary>
    public static class ExcelRead
    {
        /// <summary>
        /// Generic Excel Reader
        /// </summary>
        /// <param name="data">Binary Data</param>
        /// <returns>List of dynamic objects</returns>
        public static List<dynamic> GenericExcelReader(BinaryData data)
        {
            Dictionary<int, string> columnName = new Dictionary<int, string>();
            var genericDetails = new List<dynamic>();
            System.Text.Encoding.RegisterProvider(System.Text.CodePagesEncodingProvider.Instance);
            using (var reader = ExcelReaderFactory.CreateReader(data.ToStream()))
                {
                    if (reader.Read())
                    {
                        for (int column = 0; column < reader.FieldCount; column++)
                        {
                            if (reader.GetValue(column) != null)
                            {
                                columnName.Add(column, reader.GetValue(column).ToString());
                            }
                        }
                    }

                    while (reader.Read())
                    {
                        dynamic rowValues = new Dictionary<string, string>();
                        foreach (KeyValuePair<int, string> p in columnName)
                        {
                            if (reader.GetValue(p.Key) != null)
                            {
                                rowValues.Add(p.Value, reader.GetValue(p.Key).ToString());
                            }
                            else
                            {
                                rowValues.Add(p.Value, string.Empty);
                            }
                        }

                        genericDetails.Add(rowValues);
                    }
                }

            return genericDetails;
        }
    }
}