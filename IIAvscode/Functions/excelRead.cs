using System.Collections.Generic;
using ExcelDataReader;
using System.IO;

namespace IIABackend
{
    /// <summary>
    /// </summary> 


    public static class ExcelRead
    {
        public static List<dynamic> excelread(string path)
        {
            Dictionary<int, string> columnName = new Dictionary<int, string>();
            var genericDetails = new List<dynamic>();
            System.Text.Encoding.RegisterProvider(System.Text.CodePagesEncodingProvider.Instance);
            using (var stream = File.Open(path, FileMode.Open, FileAccess.Read))
            {
                using (var reader = ExcelReaderFactory.CreateReader(stream))
                {
                    if (reader.Read())
                    {
                        for (int column = 0; column < reader.FieldCount; column++)
                        {
                            if(reader.GetValue(column)!=null)
                                columnName.Add(column,reader.GetValue(column).ToString());
                        }
                    };
                
                    while (reader.Read()) //Each ROW
                    {
                        dynamic rowValues=new Dictionary<string,string>();
                        foreach (KeyValuePair<int, string> p in columnName){
                            if(reader.GetValue(p.Key)!=null){
                                rowValues.Add(p.Value,reader.GetValue(p.Key).ToString());
                            }
                            else
                                rowValues.Add(p.Value,"");
                        }
                        genericDetails.Add(rowValues);
                    }
                }
                
            }
            return genericDetails;
        }

    }
}