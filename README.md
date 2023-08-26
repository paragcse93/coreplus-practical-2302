# coreplus-practical-2302

Note: Emphasis has been placed on prioritizing functionality over the visual design within the UI project.

API used for Fetching Supervisor and Other Practitioners
For Supervisor
**/practitioners/supervisorsPractionars**

In UI the Supervisor List is on Top Left Portion

For Other Practitioners
**/practitioners/otherpractionars**
In UI the Supervisor List is on Bottom Left Portion

**1) Reporting**
To generate a comprehensive Practitioners report, Select an individual From Supervisor List or Other Practitioners List. Choose a specific Start Date and End Date then click the "Generate Report" button it will generate _Cost and Revenue Summary Report_ on the bottom left side

This API involves three parameters:
`practitionerId` (Required) , `startDate` (Required) ,`endDate` (Required)

The API provides monthly total revenue and cost for selected practitioner with in given date range.

**/appointments/summarybydate**

**2) Breakdown of Practitioner's Appointments**
To delve into a detailed breakdown of a specific practitioner's appointments, simply click on the corresponding Practioner name _Cost and Revenue Summary Report_ table within the summary report generated in step 1.
It will call another api

**/appointments/practitionerappointmentlist**

This API requires three parameters:

`practitionerId` (Required) , `startDate` (Required) ,`endDate` (Required)

The API furnishes a comprehensive overview of the practitioner's appointment breakdown in right side.

**3) Appointment Details**
Upon selecting an appointment from practitioner's appointment breakdown , the system will present the intricate details of the chosen appointment inside the row

This API necessitates a single parameter:

1. `appointmentId` (Required)

**/appointments/appointmentdetails/**

**Other APIs**
An additional API has been implemented to retrieve a list of practitioners apart from those categorized as OWNER and ADMIN.

**/practitioners/others/**
