using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HL7Message
{
    class MSH
    {
        //SEQ 	LEN 	DT 	OPT 	RP/# 	ELEMENT NAME
        //1 	1 	    ST 	R 	  	        Field Separator
        //2   	4 	    ST 	R 	  	        Encoding Characters
        //3 	180 	HD 	O 	  	        Sending Application
        //4 	180 	HD 	O 	  	        Sending Facility
        //5 	180 	HD 	O 	  	        Receiving Application
        //6 	180 	HD 	O 	  	        Receiving Facility
        //7 	26 	    TS 	O 	  	        Date/Time of Message
        //8 	40 	    ST 	O 	  	        Security
        //9 	7 	    CM_MSG 	R 	  	    Message Type
        //10 	20 	    ST 	R 	  	        Message Control Id
        //11 	3 	    PT 	R 	  	        Processing Id
        //12 	8 	    ID 	R 	  	        Version Id
        //13 	15 	    NM 	O 	  	        Sequence Number
        //14 	180 	ST 	O 	  	        Continuation Pointer
        //15 	2 	    ID 	O 	  	        Accept Acknowledgement Type
        //16 	2 	    ID 	O 	  	        Application Acknowledgement Type
        //17 	2 	    ID 	O 	  	        Country Code
        //18 	6 	    ID 	O 	  	        Character Set
        //19 	3 	    CE 	O 	  	        Principal Language of Message

        //CHARACTER 	NAME 	PURPOSE
        //| 	Field separator (pipe) 	Separates fields in a message
        //^ 	Component separator (hat) 	Separates components in a field
        //~ 	Field repeat separator 	Separates repeated fields in a segment
        //\ 	Escape character 	Used to signal special characters in a field of text (i.e. \H\ = start highlighting; \F\ = component separator)
        //& 	Sub-component separator 	Separates components within components (see Data Types)

        public string Field_Separator;
        public string Encoding_Characters;
        public string Sending_Application;
        public string Sending_Facility;
        public string Receiving_Application;
        public string Receiving_Facility;
        public string DateTime_of_Message;
        public string Security;
        public string Message_Type;
        public string Message_Control_Id;
        public string Processing_Id;
        public string Version_Id;
        public string Sequence_Number;
        public string Continuation_Pointer;
        public string Accept_Acknowledgement_Type;
        public string Application_Acknowledgement_Type;
        public string Country_Code;
        public string Character_Set;
        public string Principal_Language_of_Message;

    }
}
