using System;
using System.Collections.Generic;
using System.Text;

namespace XObjectSerializer.Tests
{
    public class CompressExample
    {
        public List<string> ListWithValues { get; set; }


        public CompressExample()
        {
            ListWithValues = new List<string>();
            ListWithValues.Add("Lorem Ipsum");
            ListWithValues.Add("Where can I get some?");
            ListWithValues.Add("Why do we use it?");
            ListWithValues.Add("Lorem Ipsum");
            ListWithValues.Add("Start with 'Lorem");
            ListWithValues.Add("Where does it come from?");
            ListWithValues.Add("Why do we use it?");
            ListWithValues.Add("Where can I get some?");
            ListWithValues.Add("from");
            ListWithValues.Add("Start with 'Lorem");
            ListWithValues.Add("Where does it come from?");
            ListWithValues.Add("from");
            ListWithValues.Add("Dzmitry Dym");
            ListWithValues.Add("Lorem Ipsum is simply dummy text of the printing and typesetting industry. Lorem Ipsum has been the industry's standard dummy text ever since the 1500s, when an unknown printer took a galley of type and scrambled it to make a type specimen book. It has survived not only five centuries, but also the leap into electronic typesetting, remaining essentially unchanged. It was popularised in the 1960s with the release of Letraset sheets containing Lorem Ipsum passages, and more recently with desktop publishing software like Aldus PageMaker including versions of Lorem Ipsum.");
            ListWithValues.Add("Dzmitry Dym");
            ListWithValues.Add("Lorem Ipsum is simply dummy text of the printing and typesetting industry. Lorem Ipsum has been the industry's standard dummy text ever since the 1500s, when an unknown printer took a galley of type and scrambled it to make a type specimen book. It has survived not only five centuries, but also the leap into electronic typesetting, remaining essentially unchanged. It was popularised in the 1960s with the release of Letraset sheets containing Lorem Ipsum passages, and more recently with desktop publishing software like Aldus PageMaker including versions of Lorem Ipsum.");
        }
    }
}
