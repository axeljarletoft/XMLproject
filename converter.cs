using System.IO;
using System.Xml;

namespace converter {

    class newSystem {

        static void Main(string[] args) {
            
            //file
            string filename = args[0];

            // create & begin XML file
            var sts = new XmlWriterSettings() {
                Indent = true,
            };

            using var writer = XmlWriter.Create("testFile.xml", sts);

            writer.WriteStartDocument();
            writer.WriteStartElement("people");

            // declare needed arr&var 
            string[] tags;
            string prevoiusLetter = "X";

            // read file line by line
            foreach (string line in System.IO.File.ReadLines(filename)) {
                
                // first line -  ' P|Carl Gustaf|Bernadotte '
                string[] separatedLine = line.Split("|"); // array is now {P, Carl Gustaf, Bernadotte}

                // initialiaze array with x in case of error in txt file
                tags = new string[] {"x"};

                switch(separatedLine[0]) {

                    case "P":
                        if (prevoiusLetter != "P")
                            tags = new string[] {"person", "fistname", "lastname"};
                    break;
                    
                    case "T":
                        tags = new string[] {"phone", "mobile", "landline"};
                    break;

                    case "A":
                        tags = new string[] {"address", "street", "city", "zipcode"};
                    break;

                    case "F":
                        if(prevoiusLetter != "F" || prevoiusLetter != "P")
                            tags = new string[] {"family", "name", "born"};
                    break;
                }

                // write to xml file
                if(tags[0] != "x")
                    writer.WriteStartElement(tags[0]);
                    for(int i = 1; i < separatedLine.Length; i++) {
                        writer.WriteStartElement(tags[i]);
                        writer.WriteString(separatedLine[i]);
                        writer.WriteEndElement();
                    }
                    writer.WriteEndElement();
            }

            // close XML file
            writer.WriteEndDocument(); // end of people
            Console.WriteLine("XML document created");
        }
    }
}