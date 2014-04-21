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
        public const int eM = 8;
        private List<MSH> dataMSH = new List<MSH>();
        private List<OBR> dataOBR = new List<OBR>();
        private List<OBX> dataOBX = new List<OBX>();
        private List<Slovos> dataSlovos = new List<Slovos>();
        #endregion
        public Form1()
        {
            InitializeComponent();
            InitChartu(grafikVseho);
        }

        #region Eventsna kliknuti
        private void btnRead_Click(object sender, EventArgs e)
        {
            string seznamCtenychSouboru = ListFileTerminals();

            OpenFileDialog openFileDialog1 = new OpenFileDialog();
            cLB.Enabled = false;

            openFileDialog1.Filter = seznamCtenychSouboru;
            openFileDialog1.RestoreDirectory = true;

            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    DivideDataByType(ReadDataFromFile(openFileDialog1.FileName));

                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error: Pojebalo se to nekde pri nacitani Puvodni error: " + ex.Message);
                }
            }
            try
            {
                MakeDictonary();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: Pojebalo se to nekde pri delani slovniku Puvodni error: " + ex.Message);
            }
            List<string> seznamMerVel = dataSlovos.Select(slov => slov.key).ToList();
            FillCheckBoxList(cLB, seznamMerVel);
            if (seznamMerVel.Count() > 0)
            {
                cLB.Enabled = true;
                cLB.Width = (seznamMerVel.Max(vec => vec.ToString().Length)) * eM;
            }

            DateTime min = dataOBX.FindAll(ob => ob.DateTime_of_the_Observation > new DateTime(1, 1, 1, 0, 0, 0)).Min(ob => ob.DateTime_of_the_Observation);
            var litsek = dataMSH.FindAll(msh => msh.DateTime_of_Message > new DateTime(1, 1, 1, 0, 0, 0)).Min(msh => msh.DateTime_of_Message);
            min = dataOBR.FindAll(ob => ob.Observation_DateTime > new DateTime(1, 1, 1, 0, 0, 0)).Min(obr => obr.Observation_DateTime);
        }


        private void cLB_MouseUp(object sender, MouseEventArgs e)
        {
            PrDelSerieChartu(grafikVseho);
            InitChartu(grafikVseho);
            DateTime minTime = new DateTime(9999, 1, 1, 0, 0, 0), maxTime = new DateTime(1, 1, 1, 0, 0, 0);
            int delkaPole;
            List<Slovos> joudaList = new List<Slovos>();
            List<lineNameValues> keKresleni = new List<lineNameValues>();

            #region Nastaveni minima, maxima a vyber dat do grafu
            foreach (var itemCLB in cLB.CheckedItems)
            {
                string jouda = itemCLB.ToString();
                Slovos jedenPrvek = dataSlovos.Find(kohoVeme => kohoVeme.key == jouda);
                joudaList.Add(jedenPrvek);
                if (jedenPrvek.values.Min(hod => hod.Time) < minTime && jedenPrvek.values.Min(hod => hod.Time) != new DateTime(1, 1, 1, 0, 0, 0))
                {
                    minTime = jedenPrvek.values.Min(hod => hod.Time);
                }
                if (jedenPrvek.values.Max(hod => hod.Time) > maxTime)
                {
                    maxTime = jedenPrvek.values.Max(hod => hod.Time);
                }

            }
            delkaPole = (int)(maxTime - minTime).TotalMinutes + 1;
            #endregion
            #region priprava dat ke kresleni grafu
            foreach (var itemJL in joudaList)
            {
                #region init prochazeni
                lineNameValues lnv = new lineNameValues();
                lnv.hodnotas = new double[delkaPole];
                DateTime casOdPocatku, hodnotyCas;
                int iter = 0;
                lnv.nameOfSeries = itemJL.key/*.Split('^')[1]*/ + " - [" + itemJL.values[0].Unit + "]";
                #endregion
                foreach (var hodnota in itemJL.values)
                {
                    casOdPocatku = new DateTime(minTime.Year, minTime.Month, minTime.Day, minTime.Hour, minTime.Minute, 0) + TimeSpan.FromMinutes(iter);
                    hodnotyCas = new DateTime(hodnota.Time.Year, hodnota.Time.Month, hodnota.Time.Day, hodnota.Time.Hour, hodnota.Time.Minute, 0);
                    if (hodnotyCas == casOdPocatku)
                    {
                        lnv.hodnotas[iter] = hodnota.ObjectuvValue;
                    }
                    else
                    {
                        int backUpIter = iter;
                        do
                        {
                            iter++;
                            casOdPocatku = new DateTime(minTime.Year, minTime.Month, minTime.Day, minTime.Hour, minTime.Minute, 0) + TimeSpan.FromMinutes(iter);
                            if (iter > delkaPole) { iter = backUpIter; break; }

                        } while (hodnotyCas != casOdPocatku);
                        lnv.hodnotas[iter] = hodnota.ObjectuvValue;
                        iter++;
                        continue;
                    }
                    iter++;
                }
                keKresleni.Add(lnv);
            }
            #endregion
            #region Kresleni do grafu
            foreach (var rada in keKresleni)
            {
                RdawToGraphos(grafikVseho, rada.hodnotas, rada.nameOfSeries, SeriesChartType.FastLine);
            }
            #endregion
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
                Slovos existItema = dataSlovos.Find(sl => sl.key == obxItem.Observation_Identifier);
                if (existItema == null)
                {
                    Slovos slovoItemom = new Slovos();
                    slovoItemom.key = obxItem.Observation_Identifier;
                    Hodnota hodnotoso = new Hodnota();
                    hodnotoso.ObjectuvValue = obxItem.Observation_Value;
                    hodnotoso.Unit = obxItem.Units;
                    hodnotoso.Time = obxItem.DateTime_of_the_Observation;

                    slovoItemom.values = new List<Hodnota>();
                    slovoItemom.values.Add(hodnotoso);
                    dataSlovos.Add(slovoItemom);

                }
                else
                {
                    Hodnota hodnotoso = new Hodnota();
                    hodnotoso.ObjectuvValue = obxItem.Observation_Value;
                    hodnotoso.Time = obxItem.DateTime_of_the_Observation;
                    hodnotoso.Unit = obxItem.Units;
                    existItema.values.Add(hodnotoso);
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
            rData.Requested_Datetime = rData.GetTimeFromString(inputData[5 + 1]);
            rData.Observation_DateTime = rData.GetTimeFromString(inputData[6 + 1]);
            rData.Observation_End_DateTime = rData.GetTimeFromString(inputData[7 + 1]);
            rData.Collection_Volume = inputData[8 + 1];
            rData.Collector_Identifier = inputData[9 + 1];
            rData.Specimen_Action_Code = inputData[10 + 1];
            rData.Danger_Code = inputData[11 + 1];
            rData.Relevant_Clinical_Info = inputData[13 + 1];
            rData.Specimen_Received_DateTime = rData.GetTimeFromString(inputData[14 + 1]);
            rData.Specimen_Source = inputData[15 + 1];
            rData.Ordering_Provider = inputData[16 + 1];
            rData.Order_Callback_Phone_Number = inputData[17 + 1];
            rData.Placer_field_1 = inputData[18 + 1];
            rData.Placer_field_2 = inputData[19 + 1];
            rData.Filler_Field_1 = inputData[20 + 1];
            rData.Filler_Field_2 = inputData[21 + 1];
            rData.Results_RptStatus_Chng_DateTime = rData.GetTimeFromString(inputData[23 + 1]);
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
            rData.Observation_Value = rData.GetDoubleValueFromString(inputData[4 + 1]);
            rData.Units = inputData[5 + 1];
            rData.Reference_Range = inputData[6 + 1];
            rData.Abnormal_Flags = inputData[7 + 1];
            rData.Probability = inputData[8 + 1];
            rData.Nature_of_Abnormal_Test = inputData[9 + 1];
            rData.Observ_Result_Status = inputData[10 + 1];
            rData.Data_Last_Obs_Normal_Values = inputData[11 + 1];
            rData.User_Defined_Access_Checks = inputData[12 + 1];
            rData.DateTime_of_the_Observation = rData.GetTimeFromString(inputData[13 + 1]);
            rData.Producers_Id = inputData[14 + 1];
            rData.Responsible_Observer = inputData[15 + 1];
            rData.Observation_Method = inputData[16 + 1];
            return rData;
        }

        private MSH GetMSH(string[] inputData)
        {
            MSH rData = new MSH();
            inputData = ReplaceSeparator(inputData, ",", ".");
            rData.Field_Separator = inputData[1].Substring(0, 1);
            rData.Encoding_Characters = null;
            rData.Sending_Application = null;
            rData.Sending_Facility = inputData[3];
            rData.Receiving_Application = inputData[4];
            rData.Receiving_Facility = inputData[5];
            rData.DateTime_of_Message = rData.GetTimeFromString(inputData[6]);
            rData.Security = inputData[7];
            rData.Message_Type = inputData[8];
            rData.Message_Control_Id = inputData[9];
            rData.Processing_Id = inputData[10];
            rData.Version_Id = inputData[11];
            rData.Sequence_Number = inputData[12];
            rData.Continuation_Pointer = inputData[13];
            rData.Accept_Acknowledgement_Type = inputData[14];
            rData.Application_Acknowledgement_Type = inputData[15];
            rData.Country_Code = inputData[16];
            rData.Character_Set = inputData[17];
            rData.Principal_Language_of_Message = inputData[18];
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

        private void FillCheckBoxList(CheckedListBox nameCLB, List<string> listItems)
        {
            foreach (var item in listItems)
            {
                nameCLB.Items.Add(item);
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
            while (nameChartek.Series.Count > 0)
            { 
                nameChartek.Series.RemoveAt(0);
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

        #endregion

        #endregion
    }
}
