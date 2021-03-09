using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.Xml;

namespace mem_database
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void SaveButton_Click(object sender, EventArgs e)
        {
            CheckDatabaseExists();
        }

        private void CheckDatabaseExists()
        {
            if (File.Exists("database.xml") == false)

            {
                CreateXMLandSaveData();
            }
            else if (File.Exists("datbase.xml") == true)
            {
                //SaveData();
            }
        }

        private void CreateXMLandSaveData()
        {
            XmlTextWriter xWriter = new XmlTextWriter("database.xml", Encoding.UTF8);
            xWriter.Formatting = Formatting.Indented;

            xWriter.WriteStartElement("members");
            xWriter.WriteStartElement("member");
            xWriter.WriteStartElement("firstname");
            xWriter.WriteString(FirstNameTextbox.Text);
            xWriter.WriteEndElement();

            xWriter.WriteStartElement("surname");
            xWriter.WriteString(SurnameTextBox.Text);
            xWriter.WriteEndElement();

            xWriter.WriteStartElement("dob");
            xWriter.WriteString(dateTimePicker1.Value.ToShortDateString());
            xWriter.WriteEndElement();

            xWriter.WriteStartElement("address");
            xWriter.WriteString(AddressTextbox.Text);
            xWriter.WriteEndElement();

            xWriter.WriteStartElement("mobNo");
            xWriter.WriteString(MobNoTextbox.Text);
            xWriter.WriteEndElement();

            xWriter.WriteStartElement("gender");
            xWriter.WriteString(GenderComboBox.Text);
            xWriter.WriteEndElement();

            xWriter.WriteStartElement("paid");
            xWriter.WriteString("yes");
            xWriter.WriteEndElement();

            xWriter.WriteStartElement("amount");
            xWriter.WriteString("50");
            xWriter.WriteEndElement();

            xWriter.WriteEndElement();
            xWriter.WriteEndElement();

            xWriter.Close();

            XmlDocument xDoc = new XmlDocument();
            xDoc.Load("database.xml");

            XmlDeclaration xmldecl;
            xmldecl = xDoc.CreateXmlDeclaration("1.0", "UTF-8", null);

            XmlElement root = xDoc.DocumentElement;
            xDoc.InsertBefore(xmldecl, root);
            xDoc.Save("database.xml");
        }

        private void ExitButton_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
