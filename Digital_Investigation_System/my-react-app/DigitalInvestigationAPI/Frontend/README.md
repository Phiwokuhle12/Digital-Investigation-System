# Tracepoint Investigations

## Student Information

**Student Name:** [Your Name]

**Student Number:** [Your Student Number]


## Application Description

Tracepoint Investigations is a digital investigation system developed using
ASP.NET Core Web API and React.

The system allows an investigator to:

- Review investigation cases
- View suspects
- Select a suspect
- View digital evidence
- Examine evidence
- Enter an investigation conclusion
- Submit an investigation
- Store investigation information in the database
- View investigation results using Dapper

The application follows a structured digital forensic investigation workflow.

---

## Technologies Used

### Backend

- ASP.NET Core Web API
- C#
- Entity Framework Core
- Dapper
- SQL Server
- Swagger

### Frontend

- React
- Vite
- JavaScript
- React Router
- CSS

### Testing

- Unit Testing
- Integration Testing

---

## Project Structure

```text
TracePointAPI/
│
├── Controllers/
├── Data/
├── Models/
├── Repositories/
└── Program.cs

TracePointClient/
│
├── src/
│   ├── Components/
│   │   ├── CaseCard.jsx
│   │   ├── SuspectCard.jsx
│   │   ├── EvidenceCard.jsx
│   │   └── InvestigationForm.jsx
│   │
│   ├── pages/
│   │   ├── Case.jsx
│   │   ├── Suspects.jsx
│   │   ├── Evidence.jsx
│   │   └── Investigation.jsx
│   │
│   ├── App.jsx
│   └── App.css
│
└── package.json