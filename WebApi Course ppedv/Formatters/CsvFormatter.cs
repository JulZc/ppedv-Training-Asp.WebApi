using Trainings.Core;
using System;
using System.Collections.Generic;
using System.Net.Http.Formatting;
using System.Net.Http.Headers;
using System.IO;
using System.Net.Http;

namespace WebApi_Course_ppedv.Controllers
{
    public class CsvFormatter : BufferedMediaTypeFormatter
    {
        public CsvFormatter()
        {
            SupportedMediaTypes.Add(new MediaTypeHeaderValue("text/csv"));
        }

        public override bool CanReadType(Type type)
        {
            return false;
        }

        public override bool CanWriteType(Type type)
        {

            if (type == typeof(Training)) return true;
            else
            {
                Type enumeralbeType = typeof(IEnumerable<Training>);
                return enumeralbeType.IsAssignableFrom(type);
            }

            
        }

        public override void WriteToStream(Type type, object value, Stream writeStream, HttpContent content)
        {
            using (var writer = new StreamWriter(writeStream))
            {
                var trainings = value as IEnumerable<Training>;

                if(trainings != null)
                {
                    foreach (var product in trainings)
                    {
                        WriteItem(product, writer);
                    }
                }
                else
                {
                    var singleTraining = value as Training;
                    if (singleTraining == null) throw new InvalidOperationException("Type could not be serialized");

                    WriteItem(singleTraining, writer);
                }
            }
        }

        private void WriteItem(Training product, StreamWriter writer)
        {
            writer.WriteLine("{0},{1},{2},{3}", product.Id, product.Name, product.Description, product.Category.Name);
        }
    }
}