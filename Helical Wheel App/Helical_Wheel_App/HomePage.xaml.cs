using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Reflection;
using Xamarin.Forms;
using System.Xml.Serialization;


namespace Helical_Wheel_App
{
    public partial class HomePage : ContentPage
    {
        private AminoAcids aminoClass;
        public HomePage(string ProteinName = "", string AminoSeq = "")
        {
            aminoClass = new AminoAcids();
            InitializeComponent();
            //creating tool bar item
            var toolbar = new ToolbarItem();
            toolbar.AutomationId = "RunAnalysisItem";
            toolbar.Text = "Evaluate Structure";
            toolbar.Priority = 1;
            toolbar.Clicked += new EventHandler(RunAnalysisItem_Click);
            toolbar.Order = ToolbarItemOrder.Primary;
            // creating a second toolbar item
            var toolbar2 = new ToolbarItem();
            toolbar2.AutomationId = "PreviousEntriesItem";
            toolbar2.Text = "Previous Entries";
            toolbar2.Priority = 1;
            // navigate to new page when clicked
            toolbar2.Clicked += delegate
            {
                Navigation.PushAsync(new PreviousEntries());
            };
            toolbar2.Order = ToolbarItemOrder.Primary;
            // add tool bar items
            this.ToolbarItems.Add(toolbar);
            this.ToolbarItems.Add(toolbar2);
            // if the contructor isn't empty, create a helical wheel
            if (!string.IsNullOrEmpty(AminoSeq))
            {
                ProteinEntry.Text = ProteinName;
                AminoAcidEditor.Text = AminoSeq;
                DrawHelicalWheel();
            }
        }
        void Button_Click(object sender, EventArgs e)
        {
            // if the create wheel button is clicked
            DrawHelicalWheel();
            if (!Errors.IsVisible)
                AddEntry();
        }
        void RunAnalysisItem_Click(object sender, EventArgs e)
        {
            //get the toolbar item named RunAnalysisItem
            var toolbar = this.ToolbarItems.FirstOrDefault(x => x.AutomationId.Equals("RunAnalysisItem"));
            // if the wheel is drawn and is visible
            if (HelicalView.Content != null && HelicalView.IsVisible && !toolbar.Text.Equals("Back to Entry"))
            {
                // hide the stacklayout
                AminoEntry.IsVisible = false;
                // get the content
                var content = (WheelCanvas)HelicalView.Content;
                // run the analysis
                RunAminoAnalysis(content.HelicalStructure);
                toolbar.Text = "Back to Entry";
            }
            else if (toolbar.Text.Equals("Back to Entry"))
            {
                StructureAnalysis.IsVisible = false;
                AminoEntry.IsVisible = true;
                toolbar.Text = "Evaluate Structure";

            }
            else
            {
                Errors.Text = "You must create a wheel before you run the analysis";
                Errors.IsVisible = true;
            }
        }
        public void DrawHelicalWheel()
        {
            string aminoAcid = AminoAcidEditor.Text;
            if (!string.IsNullOrWhiteSpace(aminoAcid))
            {
                bool ValidSequence = true;
                if (aminoAcid.Contains(","))
                    ValidSequence = aminoClass.IsValidThreeLetterAppreviation(aminoAcid);
                else
                    ValidSequence = aminoClass.IsValidOneLetterAppreviation(aminoAcid);
                if (ValidSequence)
                {
                    var contentView = new WheelCanvas(aminoAcid);
                    contentView.HeightRequest = .55 * App.ScreenHeight;
                    var id = contentView.Id;
                    HelicalView.Content = contentView;
                    HelicalView.IsVisible = true;
                    Errors.IsVisible = false;
                }
                else
                {
                    Errors.Text = "The amino acid sequence is not in a correct format. Please only use letters A-Z, and, for three letter abbreviation, use no more than three letters separated by commas";
                    Errors.IsVisible = true;
                }
            }
            else
            {
                Errors.Text = "The amino acid sequence cannot be empty";
                Errors.IsVisible = true;
                HelicalView.IsVisible = false;
            }
        }
        private void RunAminoAnalysis(List<KeyValuePair<string,Point>> aminoList)
        {
            var items = aminoList;
            bool prolineFound = false;
            bool invalidAminoAcid = false;
            var structErrors = new List<string>();
            StructureAnalysis.Text = "The following issues were found: \n";
            StructureAnalysis.TextColor = Color.Red;
            foreach (var item in aminoList)
            {
                var countItem = item.Key.ToCharArray().ToList().Where(x => char.IsDigit(x)).Count();
                if ((item.Key.Substring(0,1).ToLower().Equals("p") || item.Key.Substring(0,item.Key.Length - countItem).ToLower().Equals("pro")) && !prolineFound)
                {
                    prolineFound = true;
                    structErrors.Add("-Found one or more prolines present on the wheel");
                    continue;
                }
                if ((item.Key.Length - countItem) > 2 && !invalidAminoAcid)
                {
                    if (!aminoClass.IsAminoAcid(item.Key.Substring(0,item.Key.Length - countItem)))
                    {
                        structErrors.Add("-Your helical structure contains one or more invalid amino acid(s).");
                        invalidAminoAcid = true;
                        continue;
                    }
                        
                }
                if((item.Key.Length - countItem) == 1 && !invalidAminoAcid)
                {
                    if (!aminoClass.IsAminoAcid(null,item.Key.ToCharArray()[0]))
                    {
                        structErrors.Add("-Your helical structure contains one or more invalid amino acid(s).");
                        invalidAminoAcid = true;
                        continue;
                    }
                }
                List<KeyValuePair<string, Point>> templist = aminoList.Where(x => !x.Key.Equals(item.Key)).ToList();
                var closeAminos = templist.Where(x => Math.Sqrt(Math.Pow((x.Value.X - item.Value.X), 4) + Math.Pow((x.Value.Y - item.Value.Y), 2)) <= 4);
                List<KeyValuePair<string,string>> foundAminos = new List<KeyValuePair<string, string>>();
                foreach (var item2 in closeAminos)
                {
                    var countItem2 = item2.Key.ToCharArray().ToList().Where(x => char.IsDigit(x));
                    if (aminoClass.ContainsBulkyAminos(item.Key.Substring(0,item.Key.Length - countItem), item2.Key.Substring(0,item2.Key.Length - countItem2.Count())))
                    {
                        var text = $"-{item.Key} & {item2.Key} are bulky amino acids and shouldn't be very close to each other.";
                        KeyValuePair<string, string> keyPair1 = new KeyValuePair<string, string>(item.Key, item2.Key);
                        KeyValuePair<string, string> keyPair2 = new KeyValuePair<string, string>(item2.Key, item.Key);
                        if (!foundAminos.Contains(keyPair1))
                            structErrors.Add(text);
                        foundAminos.Add(keyPair1);
                        foundAminos.Add(keyPair2);
                    }

                }
            }
            if (structErrors.Any())
            {
                foreach (var error in structErrors)
                {
                    StructureAnalysis.Text += error + "\n";
                }

            }
            else
            {
                StructureAnalysis.Text = "Your helical structure looks good !";
                StructureAnalysis.TextColor = Color.ForestGreen;
            }
            StructureAnalysis.IsVisible = true;
            Content = new ScrollView { Content = MainView };
            //StructureAnalysis.HeightRequest = .40 * App.ScreenHeight;
        }
        private void AddEntry()
        {
            var docmentPath = Path.Combine(FileStream.PathApp, FileStream.FileName);
            Stream stream = File.Open(docmentPath, FileMode.Open);
            List<ProteinEntry> proteins = new List<ProteinEntry>();
            using (var reader = new System.IO.StreamReader(stream))
            {
                var serializer = new XmlSerializer(typeof(List<ProteinEntry>), new XmlRootAttribute(FileStream.RootName));
                var xmlReader = System.Xml.XmlReader.Create(reader);
                if(serializer.CanDeserialize(xmlReader))
                    proteins = (List<ProteinEntry>)serializer.Deserialize(xmlReader);
            }
            proteins.Add(new ProteinEntry(ProteinEntry.Text, AminoAcidEditor.Text));
            Stream writeStream = File.Open(docmentPath, FileMode.Open);
            using (var writer = new StreamWriter(writeStream))
            {
                XmlSerializer ser = new XmlSerializer(typeof(List<ProteinEntry>), new XmlRootAttribute(FileStream.RootName));
                ser.Serialize(writer, proteins);
            }
        }
    }
}
