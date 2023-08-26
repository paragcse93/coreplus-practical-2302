# CorePlus Practical 2302

## Pre-requisites

coreplus-sample-api require DotNet 7 to run

## Installing Npm Packages

To install the npm packages run -

```bash
npm i
```

To Run UI Project run following command -

```bash
npm run dev
```

To Run API Project run follwoing command -

```bash
dotnet run
```

_Note: Emphasis has been placed on prioritizing functionality over the visual design within the UI project._

# Bussiness Logic:-

## API Used for Fetching Supervisor and Other Practitioners

### For Supervisor

**GET /practitioners/supervisorsPractitioners**

Supervisor List is displayed in the Top Left Portion of the UI.

### For Other Practitioners

**GET /practitioners/otherpractitioners**

Other Practitioners List is displayed in the Bottom Left Portion of the UI.

## 1) Reporting

To generate a comprehensive Practitioners report:

1. Select an individual from the Supervisor List or Other Practitioners List.
2. Choose a specific Start Date and End Date.
3. Click the "Generate Report" button.

This will generate the **Cost and Revenue Summary Report** on the bottom left side of the UI.

**API Details:**

- Endpoint: **POST /appointments/summarybydate**
- Parameters: `practitionerId` (Required), `startDate` (Required), `endDate` (Required)
- Result: Monthly total revenue and cost for the selected practitioner within the given date range.

## 2) Breakdown of Practitioner's Appointments

To view a detailed breakdown of a specific practitioner's appointments:

1. Click on the corresponding practitioner's name in the **Cost and Revenue Summary Report** table within the summary report.
2. This will trigger another API call.

**API Details:**

- Endpoint: **POST /appointments/practitionerappointmentlist**
- Parameters: `practitionerId` (Required), `startDate` (Required), `endDate` (Required)
- Result: Comprehensive overview of the practitioner's appointment breakdown on the right side of the UI.

## 3) Appointment Details

Upon selecting an appointment from the practitioner's appointment breakdown:

1. The system will present the intricate details of the chosen appointment within the corresponding row.

**API Details:**

- Endpoint: **GET /appointments/appointmentdetails/**
- Parameter: `appointmentId` (Required)

## Other APIs

An additional API has been implemented to retrieve a list of practitioners apart from those categorized as OWNER and ADMIN.

**API Details:**

- Endpoint: **GET /practitioners/others/**

---

```

```
