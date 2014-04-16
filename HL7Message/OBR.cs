using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HL7Message
{
    class OBR
    {
        //SEQ 	LEN 	DT 	OPT 	RP/# 	ELEMENT NAME
        //1    	4	    SI	C		        Set ID - OBR
        //2	    75	    EI	C		        Placer Order Number
        //3	    75	    EI	C		        Filler Order Number
        //4 	200	    CE	R		        Universal Service ID
        //5	    2   	ID	B	        	Priority
        //6	    26  	TS	B	        	Requested Date/time
        //7	    26	    TS	C	        	Observation Date/Time
        //8	    26	    TS	O	        	Observation End Date/Time
        //9 	20	    CQ	O	        	Collection Volume
        //10	60	    XCN	O	    Y   	Collector Identifier
        //11	1	    ID	O	        	Specimen Action Code
        //12	60	    CE	O	        	Danger Code
        //13	300	    ST	O	        	Relevant Clinical Info.
        //14	26	    TS	C	        	Specimen Received Date/Time
        //15	300	    CM	O	        	Specimen Source
        //16	80  	XCN	O	    Y   	Ordering Provider
        //17	40  	XTN	O	    Y/2 	Order Callback Phone Number
        //18	60  	ST	O		        Placer field 1
        //19	60	    ST	O		        Placer field 2
        //20	60  	ST	O		        Filler Field 1
        //21	60  	ST	O		        Filler Field 2
        //22	26	    TS	C	        	Results Rpt/Status Chng - Date/Time
        //23	40	    CM	O	        	Charge to Practice
        //24	10	    ID	O	        	Diagnostic Serv Sect ID
        //25	1	    ID	C	        	Result Status
        //26	400	     CM	O	           	Parent Result
        //27	200 	TQ	O	    Y   	Quantity/Timing
        //28	150 	XCN	O	    Y/5 	Result Copies To
        //29	150 	CM	O		        Parent
        //30	20	    ID	O		        Transportation Mode
        //31	300 	CE	O	    Y   	Reason for Study
        //32	200 	CM	O	        	Principal Result Interpreter
        //33	200 	CM	O	    Y   	Assistant Result Interpreter
        //34	200	    CM	O	    Y   	Technician
        //35	200 	CM	O	    Y   	Transcriptionist
        //36	26  	TS	O	        	Scheduled Date/Time
        //37	4   	NM	O	        	Number of Sample Containers
        //38	60  	CE	O	    Y   	Transport Logistics of Collected Sample
        //39	200 	CE	O	    Y   	Collector’s Comment
        //40	60  	CE	O		        Transport Arrangement Responsibility
        //41	30  	ID	O	        	Transport Arranged
        //42	1	    ID	O		        Escort Required
        //43	200 	CE	O	    Y   	Planned Patient Transport Comment

        public string Set_ID_OBR;
public string Placer_Order_Number;
public string Filler_Order_Number;
public string Universal_Service_ID;
public string Priority;
public string Requested_Datetime;
public string Observation_DateTime;
public string Observation_End_DateTime;
public string Collection_Volume;
public string Collector_Identifier;
public string Specimen_Action_Code;
public string Danger_Code;
public string Relevant_Clinical_Info;
public string Specimen_Received_DateTime;
public string Specimen_Source;
public string Ordering_Provider;
public string Order_Callback_Phone_Number;
public string Placer_field_1;
public string Placer_field_2;
public string Filler_Field_1;
public string Filler_Field_2;
public string Results_RptStatus_Chng_DateTime;
public string Charge_to_Practice;
public string Diagnostic_Serv_Sect_ID;
public string Result_Status;
public string Parent_Result;
public string QuantityTiming;
public string Result_Copies_To;
public string Parent;
public string Transportation_Mode;
public string Reason_for_Study;
public string Principal_Result_Interpreter;
public string Assistant_Result_Interpreter;
public string Technician;
public string Transcriptionist;
public string Scheduled_DateTime;
public string Number_of_Sample_Containers;
public string Transport_Logistics_of_Collected_Sample;
public string Collectors_Comment;
public string Transport_Arrangement_Responsibility;
public string Transport_Arranged;
public string Escort_Required;
public string Planned_Patient_Transport_Comment;


    }
}
