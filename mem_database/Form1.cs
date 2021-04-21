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
            CalcTotalPayments();
        }

        private void SearchButton_Click(object sender, EventArgs e)
        {
            SearchByMobNo();
        }

        private void ExitButton_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void CheckDatabaseExists()
        {
            if (File.Exists("database.xml") == true)
            {
                //SaveData();
            }
            else if (File.Exists("datbase.xml") == false)
            {
                CreateXMLandSaveData();
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
            if (YesRadioButton.Checked)
            {
                xWriter.WriteString("true");
            }
            if (NoRadioButton.Checked)
            {
                xWriter.WriteString("false");
            }
            xWriter.WriteEndElement();

            xWriter.WriteStartElement("amount");
            int age = DateTime.Now.Year - dateTimePicker1.Value.Year;
            if (age < 18 || age >= 65)
            {
                xWriter.WriteString("40");
            }
            else
            {
                xWriter.WriteString("50");
            }
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

        private void SaveData()
        {
            XmlDocument xDoc = new XmlDocument();
            xDoc.Load("database.xml");

            XmlNode member = xDoc.CreateElement("member");
            XmlNode firstname = xDoc.CreateElement("firstname");
            XmlNode surname = xDoc.CreateElement("surname");
            XmlNode address = xDoc.CreateElement("address");
            XmlNode mobNo = xDoc.CreateElement("mobNo");
            XmlNode gender = xDoc.CreateElement("dogenderb");
            XmlNode paid = xDoc.CreateElement("paid");
            XmlNode amount = xDoc.CreateElement("amount");

            firstname.InnerText = FirstNameTextbox.Text;
            member.AppendChild(firstname);
            surname.InnerText = SurnameTextBox.Text;
            member.AppendChild(surname);
            address.InnerText = AddressTextbox.Text;
            member.AppendChild(address);
            mobNo.InnerText = MobNoTextbox.Text;
            member.AppendChild(mobNo);
            gender.InnerText = GenderComboBox.Text;
            member.AppendChild(gender);
            paid.InnerText = GenderComboBox.Text;
            member.AppendChild(gender);

            int age = DateTime.Now.Year - dateTimePicker1.Value.Year;
            if (age < 18 || age >= 65)
            {
                amount.InnerText = "40";
            }
            else
            {
                amount.InnerText = "50";
            }
            member.AppendChild(amount);

            xDoc.DocumentElement.AppendChild(member);
            xDoc.Save("database.xml");

            MessageBox.Show("Member data saved to XML successfully");
        }

        private void SearchByMobNo()
        {
            bool found = false;

            XmlDocument xDoc = new XmlDocument();
            xDoc.Load("database.xml");

            string searchedMember = SearchMobNoTextbox.Text;

            foreach (XmlNode xNode in xDoc.SelectNodes("members/member"))
            {
                if (xNode.SelectSingleNode("mobNo").InnerText == searchedMember)
                {
                    found = true;
                    FoundFirstNameLabel.Text = xNode.SelectSingleNode("firstname").InnerText;
                    FoundSurnameLabel.Text = xNode.SelectSingleNode("surname").InnerText;
                    FoundAddressLabel.Text = xNode.SelectSingleNode("address").InnerText;
                    FoundMobNoLabel.Text = xNode.SelectSingleNode("mobNo").InnerText;
                    FoundGenderLabel.Text = xNode.SelectSingleNode("gender").InnerText;
                    FoundPaidLabel.Text = xNode.SelectSingleNode("paid").InnerText;
                }
            }

            if (!found)
            {
                Console.WriteLine();
                MessageBox.Show("No member found with that mobile number.");
            }
        }

        private void CalcTotalPayments()
        {
            XmlDocument xDoc = new XmlDocument();
            xDoc.Load("database.xml");
            int total = 0;

            foreach (XmlNode xNode in xDoc.SelectNodes("members/member"))
            {
                total = total + int.Parse(xNode.SelectSingleNode("amount").InnerText);
            }

            TotalPaymentLabel.Text = $"${total}";
        }
    }
}
