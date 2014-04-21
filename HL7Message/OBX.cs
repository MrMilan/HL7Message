using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HL7Message
{
    class OBX
    {
        //SEQ	LEN	DT	OPT	RP/#	ELEMENT NAME
        //1	    4	SI	O		    Set ID – Obx
        //2 	2	ID	R		    Value Type
        //3 	590	CE	R		    Observation Identifier
        //4	    20	ST	O		    Observation Sub-Id
        //5	    65536	ST	O		Observation Value
        //6	    60	CE	O		    Units
        //7 	10	ST	O		    Reference Range
        //8	    5	ID	O	Y/5	    Abnormal Flags
        //9	    5	NM	O		    Probability
        //10	2	ID	O		    Nature of Abnormal Test
        //11	1	ID	R		    Observ Result Status
        //12	26	TS	O	    	Data Last Obs Normal Values
        //13	20	ST	O		    User Defined Access Checks
        //14	26	TS	O		    Date/Time of the Observation
        //15	60	CE	O		    Producer’s Id
        //16	80	XCN	O		    Responsible Observer
        //17	80	CE	O	Y   	Observation Method

        public string Set_ID_Obx;
        public string Value_Type;
        public string Observation_Identifier;
        public string Observation_SubId;
        public double Observation_Value;
        public string Units;
        public string Reference_Range;
        public string Abnormal_Flags;
        public string Probability;
        public string Nature_of_Abnormal_Test;
        public string Observ_Result_Status;
        public string Data_Last_Obs_Normal_Values;
        public string User_Defined_Access_Checks;
        public DateTime DateTime_of_the_Observation;
        public string Producers_Id;
        public string Responsible_Observer;
        public string Observation_Method;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="datetimeOriginal"></param>
        /// <returns>Datetime</returns>
        public DateTime GetTimeFromString(string datetimeOriginal)
        {
            if (String.IsNullOrWhiteSpace(datetimeOriginal))
            {
                return new DateTime(1, 1, 1, 0, 0, 0);
            }
            else
            {
                return new DateTime(Convert.ToInt32(datetimeOriginal.Substring(0, 4)),
                             Convert.ToInt32(datetimeOriginal.Substring(4, 2)),
                             Convert.ToInt32(datetimeOriginal.Substring(6, 2)),
                             Convert.ToInt32(datetimeOriginal.Substring(8, 2)),
                             Convert.ToInt32(datetimeOriginal.Substring(10, 2)),
                             Convert.ToInt32(datetimeOriginal.Substring(12, 2)));
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="valueOriginal"></param>
        /// <returns>Int</returns>
        public double GetIntValueFromString(string valueOriginal)
        {
            return String.IsNullOrWhiteSpace(valueOriginal) ? 0 : Convert.ToInt32(valueOriginal);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="valueOriginal"></param>
        /// <returns>Double</returns>
        public double GetDoubleValueFromString(string valueOriginal)
        {
            valueOriginal = valueOriginal.Replace(".", ",");
            return String.IsNullOrWhiteSpace(valueOriginal) ? 0 : Convert.ToDouble(valueOriginal);
        }

    }
}
