using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Windows.Forms.DataVisualization.Charting;

namespace HL7Message
{
    public partial class Form1 : Form
    {

        #region GlobalVariables

        private List<MSH> dataMSH = new List<MSH>();
        private List<OBR> dataOBR = new List<OBR>();
        private List<OBX> dataOBX = new List<OBX>();
        private List<Slovos> dataSlovos = new List<Slovos>();
        #endregion
        public Form1()
        {
            InitializeComponent();
        }

        #region Eventsna kliknuti
        private void btnRead_Click(object sender, EventArgs e)
        {
            string seznamCtenychSouboru = ListFileTerminals();

            OpenFileDialog openFileDialog1 = new OpenFileDialog();

            openFileDialog1.Filter = seznamCtenychSouboru;
            openFileDialog1.RestoreDirectory = true;

            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    DivideDataByType(ReadDataFromFile(openFileDialog1.FileName));
                    MakeDictonary();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error: Pojebalo se to nekde pri nacitani Puvodni error: " + ex.Message);
                }
            }


        }

        #endregion

        #region Splitovani a parsovani dat

        private void DivideDataByType(string[] inputDataFromFile)
        {
            for (int i = 0; i < inputDataFromFile.Length; i++)
            {
                string[] transferLevel = inputDataFromFile[i].Split('|');

                if (transferLevel[0] == "MSH")
                {
                    MSH mshItem = GetMSH(transferLevel);
                    dataMSH.Add(mshItem);
                }
                if (transferLevel[0] == "OBR")
                {
                    OBR obrItem = GetOBR(transferLevel);
                    dataOBR.Add(obrItem);
                }
                if (transferLevel[0] == "OBX")
                {
                    OBX oBXItem = GetOBX(transferLevel);
                    dataOBX.Add(oBXItem);
                }

            }
        }

        private void MakeDictonary()
        {

            foreach (var obxItem in dataOBX)
            {
                Slovos existItema = dataSlovos.Find(delegate(Slovos sl)
             {
                 return sl.key == obxItem.Observation_Identifier;
             }
             );
                if (existItema == null)
                {
                    Slovos slovoItemom = new Slovos();
                    slovoItemom.key = obxItem.Observation_Identifier;
                    Hodnota hodnotoso = new Hodnota();
                    hodnotoso.ObjectValue = obxItem.Observation_Value;
                    hodnotoso.Unit = obxItem.Units;
                    slovoItemom.values = new List<Hodnota>();
                    slovoItemom.values.Add(hodnotoso);
                    dataSlovos.Add(slovoItemom);

                }
                else
                {
                    foreach (var itemSlovos in dataSlovos)
                    {
                        if (itemSlovos.key == obxItem.Observation_Identifier)
                        {
                            Hodnota hodnotoso = new Hodnota();
                            hodnotoso.ObjectValue = obxItem.Observation_Value;
                            hodnotoso.Unit = obxItem.Units;
                            itemSlovos.values.Add(hodnotoso);
                            break;
                        }

                    }
                }
            }


        }
        #endregion

        #region GetData

        private OBR GetOBR(string[] inputData)
        {
            OBR rData = new OBR();
            inputData = ReplaceSeparator(inputData, ",", ".");
            rData.Set_ID_OBR = inputData[0 + 1];
            rData.Placer_Order_Number = inputData[1 + 1];
            rData.Filler_Order_Number = inputData[2 + 1];
            rData.Universal_Service_ID = inputData[3 + 1];
            rData.Priority = inputData[4 + 1];
            rData.Requested_Datetime = inputData[5 + 1];
            rData.Observation_DateTime = inputData[6 + 1];
            rData.Observation_End_DateTime = inputData[7 + 1];
            rData.Collection_Volume = inputData[8 + 1];
            rData.Collector_Identifier = inputData[9 + 1];
            rData.Specimen_Action_Code = inputData[10 + 1];
            rData.Danger_Code = inputData[11 + 1];
            rData.Relevant_Clinical_Info = inputData[13 + 1];
            rData.Specimen_Received_DateTime = inputData[14 + 1];
            rData.Specimen_Source = inputData[15 + 1];
            rData.Ordering_Provider = inputData[16 + 1];
            rData.Order_Callback_Phone_Number = inputData[17 + 1];
            rData.Placer_field_1 = inputData[18 + 1];
            rData.Placer_field_2 = inputData[19 + 1];
            rData.Filler_Field_1 = inputData[20 + 1];
            rData.Filler_Field_2 = inputData[21 + 1];
            rData.Results_RptStatus_Chng_DateTime = inputData[23 + 1];
            rData.Charge_to_Practice = inputData[24 + 1];
            //rData.Diagnostic_Serv_Sect_ID = inputData[25+1];
            //rData.Result_Status = inputData[26+1];
            //rData.Parent_Result = inputData[27+1];
            //rData.QuantityTiming = inputData[28+1];
            //rData.Result_Copies_To = inputData[29+1];
            //rData.Parent = inputData[30+1];
            //rData.Transportation_Mode = inputData[31+1];
            //rData.Reason_for_Study = inputData[32+1];
            //rData.Principal_Result_Interpreter = inputData[33+1];
            //rData.Assistant_Result_Interpreter = inputData[34+1];
            //rData.Technician = inputData[35+1];
            //rData.Transcriptionist = inputData[36+1];
            //rData.Scheduled_DateTime = inputData[37+1];
            //rData.Number_of_Sample_Containers = inputData[38+1];
            //rData.Transport_Logistics_of_Collected_Sample = inputData[39+1];
            //rData.Collectors_Comment = inputData[41+1];
            //rData.Transport_Arrangement_Responsibility = inputData[42+1];
            //rData.Transport_Arranged = inputData[43+1];
            //rData.Escort_Required = inputData[44+1];
            //rData.Planned_Patient_Transport_Comment = inputData[45+1];
            return rData;
        }

        private OBX GetOBX(string[] inputData)
        {
            OBX rData = new OBX();
            inputData = ReplaceSeparator(inputData, ",", ".");
            rData.Set_ID_Obx = inputData[0 + 1];
            rData.Value_Type = inputData[1 + 1];
            rData.Observation_Identifier = inputData[2 + 1];
            rData.Observation_SubId = inputData[3 + 1];
            rData.Observation_Value = inputData[4 + 1];
            rData.Units = inputData[5 + 1];
            rData.Reference_Range = inputData[6 + 1];
            rData.Abnormal_Flags = inputData[7 + 1];
            rData.Probability = inputData[8 + 1];
            rData.Nature_of_Abnormal_Test = inputData[9 + 1];
            rData.Observ_Result_Status = inputData[10 + 1];
            rData.Data_Last_Obs_Normal_Values = inputData[11 + 1];
            rData.User_Defined_Access_Checks = inputData[12 + 1];
            rData.DateTime_of_the_Observation = inputData[13 + 1];
            rData.Producers_Id = inputData[14 + 1];
            rData.Responsible_Observer = inputData[15 + 1];
            rData.Observation_Method = inputData[16 + 1];
            return rData;
        }

        private MSH GetMSH(string[] inputData)
        {
            MSH rData = new MSH();
            inputData = ReplaceSeparator(inputData, ",", ".");
            rData.Field_Separator = inputData[0 + 1];
            rData.Encoding_Characters = inputData[1 + 1];
            rData.Sending_Application = inputData[2 + 1];
            rData.Sending_Facility = inputData[3 + 1];
            rData.Receiving_Application = inputData[4 + 1];
            rData.Receiving_Facility = inputData[5 + 1];
            rData.DateTime_of_Message = inputData[6 + 1];
            rData.Security = inputData[7 + 1];
            rData.Message_Type = inputData[8 + 1];
            rData.Message_Control_Id = inputData[9 + 1];
            rData.Processing_Id = inputData[10 + 1];
            rData.Version_Id = inputData[11 + 1];
            rData.Sequence_Number = inputData[12 + 1];
            rData.Continuation_Pointer = inputData[13 + 1];
            rData.Accept_Acknowledgement_Type = inputData[14 + 1];
            rData.Application_Acknowledgement_Type = inputData[15 + 1];
            rData.Country_Code = inputData[16 + 1];
            rData.Character_Set = inputData[17 + 1];
            rData.Principal_Language_of_Message = inputData[18 + 1];
            return rData;
        }

        #endregion
        #region Bezne rutiny

        /// <summary>
        /// Funkce pro nacitani dat ze souboru
        /// </summary>
        /// <param name="route">Cesta k souboru</param>
        private string[] ReadDataFromFile(string route)
        {
            return File.ReadAllLines(route);
        }

        private string ListFileTerminals()
        {
            List<string> cteneTypySouboru = new List<string>();
            cteneTypySouboru.Add("Textak file (*.txt)|*.txt");
            cteneTypySouboru.Add("Textak file (*.TXT)|*.TXT");
            cteneTypySouboru.Add("All files (*.*)|*.*");

            string seznamCtenychSouboru = "";

            for (int i = 0; i < cteneTypySouboru.Count(); i++)
            {
                if (i < cteneTypySouboru.Count() - 1)
                { seznamCtenychSouboru += cteneTypySouboru[i] + "|"; }
                else
                { seznamCtenychSouboru += cteneTypySouboru[i]; }
            }

            return seznamCtenychSouboru;

        }

        private double[] Histogram(double[] data, int range)
        {
            Array.Sort(data);
            double diference = data.Max() - data.Min();
            int group = (int)(diference / range);
            double[] his = new double[group];


            for (int j = 0; j < data.Length; j++)
            {
                for (int i = 0; i < group; i++)
                {
                    if (data[j] > (data.Min() + (range * i)) && data[j] <= (data.Min() + (range * (i + 1))))
                    {
                        his[i]++;
                        break;
                    }
                    if (data[j] == data.Min())
                    {
                        his[i]++;
                        break;
                    }
                }
            }
            return his;
        }

        private static string GetChecksum(string sentence)
        {
            //Start with first Item
            int checksum = Convert.ToByte(sentence[sentence.IndexOf('$') + 1]);
            // Loop through all chars to get a checksum
            for (int i = sentence.IndexOf('$') + 2; i < sentence.IndexOf('*'); i++)
            {
                // No. XOR the checksum with this character's value
                checksum ^= Convert.ToByte(sentence[i]);
            }
            // Return the checksum formatted as a two-character hexadecimal
            return checksum.ToString("X2");
        }

        private double ConvetToDegLong(string nonDegValue, string logn)
        {
            if (logn == "E" || logn == "e")
            {
                return Convert.ToDouble(nonDegValue.Substring(0, 3)) + (Convert.ToDouble(nonDegValue.Substring(3, 5)) / (60));
            }
            if (logn == "W" || logn == "w")
            {
                return -(Convert.ToDouble(nonDegValue.Substring(0, 3)) + (Convert.ToDouble(nonDegValue.Substring(3, 5)) / (60)));
            }
            else
            {
                return 0;
            }
        }

        private double ConvetToDEgLat(string nonDegValue, string lat)
        {
            if (lat == "N" || lat == "n")
            {
                return Convert.ToDouble(nonDegValue.Substring(0, 2)) + (Convert.ToDouble(nonDegValue.Substring(2, 6)) / (60));
            }
            if (lat == "S" || lat == "s")
            {
                return -(Convert.ToDouble(nonDegValue.Substring(0, 2)) + (Convert.ToDouble(nonDegValue.Substring(2, 6)) / (60)));
            }
            else
            {
                return 0;
            }

        }

        #region rReplace metody

        private string[] ReplaceSeparator(string[] inputStringArray, string oldSeparator, string newSeparator)
        {
            string[] newArrayWithNewSeparator = new string[inputStringArray.Length];
            for (int i = 0; i < inputStringArray.Length; i++)
            {
                newArrayWithNewSeparator[i] = inputStringArray[i].Replace(oldSeparator, newSeparator);
            }
            return newArrayWithNewSeparator;
        }

        /// <summary>
        /// Funkce pro vykresleni dat
        /// </summary>
        /// <param name="inputDataArray">Nacita vstupni double pole</param>
        /// <param name="nameLine">Nazev rady</param>
        public string[] ReplaceNull(string[] inputStringArray)
        {

            for (int i = 0; i < inputStringArray.Length; i++)
            {
                if (String.IsNullOrWhiteSpace(inputStringArray[i]))
                {
                    inputStringArray[i] = "0";
                }
            }
            return inputStringArray;
        }

        #endregion

        #region Grafove metody

        private void InitChartu(System.Windows.Forms.DataVisualization.Charting.Chart nameChartek)
        {
            nameChartek.ChartAreas[0].AxisX.ScaleView.Zoomable = true;
            nameChartek.ChartAreas[0].AxisY.ScaleView.Zoomable = true;

            nameChartek.ChartAreas[0].CursorX.AutoScroll = true;
            nameChartek.ChartAreas[0].CursorY.AutoScroll = true;

            nameChartek.ChartAreas[0].CursorX.IsUserSelectionEnabled = true;
            nameChartek.ChartAreas[0].CursorY.IsUserSelectionEnabled = true;

        }

        private void PrDelSerieChartu(System.Windows.Forms.DataVisualization.Charting.Chart nameChartek)
        {
            for (int i = 0; i < nameChartek.Series.Count; i++)
            {
                nameChartek.Series.RemoveAt(i);
            }
        }

        private void PrepareSerie(System.Windows.Forms.DataVisualization.Charting.Chart nameChartek, string nameLine, SeriesChartType seriesChartType)
        {
            nameChartek.Series.Add(nameLine);
            nameChartek.Series[nameLine].ChartType = seriesChartType;
        }

        private void RdawLineDataToChart(System.Windows.Forms.DataVisualization.Charting.Chart nameChartek, double[] inputDataArray, string nameLine)
        {
            for (int i = 0; i < inputDataArray.Length; i++)
            {
                nameChartek.Series[nameLine].Points.AddXY(i, inputDataArray[i]);
            }
        }

        private void RdawLineDataToXY(System.Windows.Forms.DataVisualization.Charting.Chart nameChartek, double[,] inputDataArrayXY, string nameLine)
        {
            for (int i = 0; i < inputDataArrayXY.GetLength(1); i++)
            {
                nameChartek.Series[nameLine].Points.AddXY(inputDataArrayXY[0, i], inputDataArrayXY[1, i]);
            }
        }

        private void RdawToGraphos(System.Windows.Forms.DataVisualization.Charting.Chart nameChartek, double[] inputArray, string nameLine, SeriesChartType seriesChartType)
        {
            PrepareSerie(nameChartek, nameLine, seriesChartType);
            RdawLineDataToChart(nameChartek, inputArray, nameLine);

        }

        private void RdawToGraphos(System.Windows.Forms.DataVisualization.Charting.Chart nameChartek, double[,] inputArrayXY, string nameLine, SeriesChartType seriesChartType)
        {
            PrepareSerie(nameChartek, nameLine, seriesChartType);
            RdawLineDataToXY(nameChartek, inputArrayXY, nameLine);

        }

        private void RdawHistogram(System.Windows.Forms.DataVisualization.Charting.Chart nameChartek, double[] inputArray, string nameLine, SeriesChartType seriesChartType, int range)
        {
            if (range == null)
            {
                range = 100;
            }

            PrepareSerie(nameChartek, nameLine, seriesChartType);

            // double[] pokus = { 1, 2, 3, 4, 5, 6, 7, 7, 7, 7, 1,6,6, 2, 3, 4, 5, 6 };range = 2; // Rozložení histogramu {6,4,8}

            double[] inputDataHis = Histogram(inputArray, range);

            for (int i = 0; i < inputDataHis.Length; i++)
            {
                nameChartek.Series[nameLine].Points.AddXY(inputDataHis.Min() + (i * range), inputDataHis[i]);
            }
        }

        #endregion

        #endregion
    }
}
