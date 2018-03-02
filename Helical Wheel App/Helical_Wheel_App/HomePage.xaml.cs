﻿using System;
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
            PreviousEntriesItem.Clicked += delegate
            {
                Navigation.PushAsync(new PreviousEntries());
            };
            if (!string.IsNullOrEmpty(AminoSeq))
            {
                ProteinEntry.Text = ProteinName;
                AminoAcidEditor.Text = AminoSeq;
                DrawHelicalWheel();
            }
        }
        void Button_Click(object sender, EventArgs e)
        {
            DrawHelicalWheel();
            if (!Errors.IsVisible)
                AddEntry();
        }
        void RunAnalysisItem_Click(object sender, EventArgs e)
        {
            if (HelicalView.Content != null && HelicalView.IsVisible && !RunAnalysisItem.Text.Equals("Back to Entry"))
            {
                AminoEntry.IsVisible = false;
                var content = (WheelCanvas)HelicalView.Content;
                RunAminoAnalysis(content.HelicalStructure);
                RunAnalysisItem.Text = "Back to Entry";
            }
            else if (RunAnalysisItem.Text.Equals("Back to Entry"))
            {
                StructureAnalysis.IsVisible = false;
                ScrollBar.IsVisible = false;
                AminoEntry.IsVisible = true;
                RunAnalysisItem.Text = "Evaluate Structure";

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
                    contentView.HeightRequest = .55 * Application.Current.MainPage.Height;
                    var id = contentView.Id;
                    HelicalView.Content = contentView;
                    HelicalView.IsVisible = true;
                    Errors.IsVisible = false;
                }
                else
                {
                    Errors.Text = "The amino acid sequence is not in a correct format. Please only use letters A-Z, and, for three letter appreviation, use no more than three letters seperated by commas";
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
                foreach (var item2 in closeAminos)
                {
                    var countItem2 = item2.Key.ToCharArray().ToList().Where(x => char.IsDigit(x));
                    if (aminoClass.ContainsBulkyAminos(item.Key.Substring(0,item.Key.Length - countItem), item2.Key.Substring(0,item2.Key.Length - countItem2.Count())))
                        structErrors.Add($"-{item.Key.Substring(0,item.Key.Length - countItem)} & {item2.Key.Substring(0, item2.Key.Length - countItem2.Count())} are bulky amino acids and shouldn't be very close to each other");
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
            ScrollBar.IsVisible = true;
            StructureAnalysis.HeightRequest = .40 * Application.Current.MainPage.Height;

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
