﻿using System;
using System.Data;
using System.IO;
using System.Windows.Forms;
using XML_GUI.Properties;

namespace XML_GUI
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            openXmlFile();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            openXmlFile();
        }
        private void saveXml_Click(object sender, EventArgs e)
        {
            saveXmlFile();
        }
        
        private void saveCurrent_Click(object sender, EventArgs e)
        {
            XML2DB.XML.XMLParser.exportXmlData((DataTable) xmlDataGrid.DataSource, currentOpenXmlPath);
            MessageBox.Show(Resources.XMLGUI_saveCurrent_success + currentOpenXmlPath, Resources.success);
        }
        
        private void saveXmlFile()
        {
            using (SaveFileDialog fileDialog = new SaveFileDialog()){
                //openFileDialog.InitialDirectory = @"%HOMEPATH%";
                fileDialog.Filter = Resources.XMLGUI_xmlfile_dialogextention;
                fileDialog.FilterIndex = 1;
                fileDialog.RestoreDirectory = true;

                if (fileDialog.ShowDialog() == DialogResult.OK)
                {
                    //Save the contents of the xmlDataGrid into the chosen file
                    XML2DB.XML.XMLParser.exportXmlData((DataTable) xmlDataGrid.DataSource, fileDialog.FileName);
                }
            }
        }
        
        private void loadXmlFile(Stream xmlDoc)
        {
            xmlDataGrid.DataSource = XML2DB.XML.XMLParser.getXmlData(xmlDoc).Tables[0];
        }
        private void openXmlFile(){
            using (OpenFileDialog openFileDialog = new OpenFileDialog()){
            //openFileDialog.InitialDirectory = @"%HOMEPATH%";
            openFileDialog.Filter = Resources.XMLGUI_xmlfile_dialogextention;
            openFileDialog.FilterIndex = 1;
            openFileDialog.RestoreDirectory = true;

            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                //Load the contents of the file into xmlDataGrid
                Stream xmlDoc = openFileDialog.OpenFile();
                loadXmlFile(xmlDoc);
                currentOpenXmlPath = openFileDialog.FileName; //Set the static variable to be used later for saving
                xmlDoc.Close();
            }
        }
    }

        private static String currentOpenXmlPath = String.Empty;

    }
}