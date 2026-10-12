using DigitalInvestigationAPI.Data;
using DigitalInvestigationAPI.Models;
using Microsoft.EntityFrameworkCore;
using QuestPDF.Fluent;
using QuestPDF.Helpers;
using QuestPDF.Infrastructure;

namespace DigitalInvestigationAPI.Services
{
    public class InvestigationDocketService
    {
        private readonly DigitalInvestigationContext _context;

        public InvestigationDocketService(
            DigitalInvestigationContext context)
        {
            _context = context;
        }

        public async Task<byte[]> GenerateDocketAsync(
            int investigationID)
        {
            // =========================================================
            // GET INVESTIGATION
            // =========================================================

            var investigation = await _context.Investigations
                .FirstOrDefaultAsync(
                    i => i.InvestigationID == investigationID
                );

            if (investigation == null)
            {
                throw new Exception("Investigation not found.");
            }

            // =========================================================
            // GET CASE
            // =========================================================

            var caseInformation = await _context.Cases
                .FirstOrDefaultAsync(
                    c => c.CaseID == investigation.CaseID
                );

            // =========================================================
            // GET SUSPECT
            // =========================================================

            var suspect = await _context.Suspects
                .FirstOrDefaultAsync(
                    s => s.SuspectID == investigation.SuspectID
                );

            // =========================================================
            // GET EVIDENCE
            // =========================================================

            var evidence = await _context.Evidence
                .ToListAsync();

            // =========================================================
            // GENERATE DOCKET NUMBER
            // =========================================================

            string docketNumber =
                $"TP-{investigation.DateStarted.Year}-{investigation.InvestigationID:D4}";

            // =========================================================
            // GENERATE DIGITAL SIGNATURE
            // =========================================================

            string digitalSignature =
                $"TP-SIGNED-{investigation.DateStarted.Year}-{investigation.InvestigationID:D4}";

            // =========================================================
            // GENERATE PDF
            // =========================================================

            var document = Document.Create(container =>
            {
                container.Page(page =>
                {
                    page.Size(PageSizes.A4);

                    page.Margin(40);

                    // QuestPDF has Lato registered.
                    page.DefaultTextStyle(x =>
                        x.FontFamily("Lato")
                         .FontSize(10)
                         .FontColor("#1f2937")
                    );

                    // =================================================
                    // HEADER
                    // =================================================

                    page.Header()
                        .Element(header =>
                        {
                            ComposeHeader(
                                header,
                                docketNumber
                            );
                        });

                    // =================================================
                    // CONTENT
                    // =================================================

                    page.Content()
                        .Element(content =>
                        {
                            ComposeContent(
                                content,
                                investigation,
                                caseInformation,
                                suspect,
                                evidence,
                                digitalSignature
                            );
                        });

                    // =================================================
                    // FOOTER
                    // =================================================

                    page.Footer()
                        .AlignCenter()
                        .Text(text =>
                        {
                            text.Span(
                                "TRACEPOINT • Digital Investigation System"
                            )
                            .FontSize(8)
                            .FontColor("#64748b");

                            text.Span("   |   ")
                                .FontSize(8)
                                .FontColor("#94a3b8");

                            text.Span(docketNumber)
                                .FontSize(8)
                                .Bold()
                                .FontColor("#2563eb");
                        });
                });
            });

            return document.GeneratePdf();
        }

        // =========================================================
        // HEADER
        // =========================================================

        private void ComposeHeader(
            IContainer container,
            string docketNumber)
        {
            container.Column(column =>
            {
                column.Item()
                    .BorderBottom(2)
                    .BorderColor("#2563eb")
                    .PaddingBottom(12)
                    .Row(row =>
                    {
                        // =================================================
                        // TRACEPOINT BADGE
                        // =================================================

                        row.ConstantItem(75)
                            .Height(75)
                            .Background("#061522")
                            .Border(2)
                            .BorderColor("#2563eb")
                            .AlignMiddle()
                            .AlignCenter()
                            .Column(badge =>
                            {
                                badge.Item()
                                    .Text("TP")
                                    .FontSize(26)
                                    .Bold()
                                    .FontColor("#20b4ff");

                                badge.Item()
                                    .Text("TRACEPOINT")
                                    .FontSize(7)
                                    .Bold()
                                    .FontColor("#ffffff");
                            });

                        row.ConstantItem(20);

                        // =================================================
                        // TITLE
                        // =================================================

                        row.RelativeItem()
                            .Column(text =>
                            {
                                text.Item()
                                    .Text("TRACEPOINT")
                                    .FontSize(24)
                                    .Bold()
                                    .FontColor("#061522");

                                text.Item()
                                    .Text(
                                        "DIGITAL INVESTIGATION SYSTEM"
                                    )
                                    .FontSize(9)
                                    .Bold()
                                    .FontColor("#2563eb");

                                text.Item()
                                    .PaddingTop(8)
                                    .Text("INVESTIGATION DOCKET")
                                    .FontSize(14)
                                    .Bold()
                                    .FontColor("#334155");
                            });

                        // =================================================
                        // DOCKET NUMBER
                        // =================================================

                        row.ConstantItem(150)
                            .AlignMiddle()
                            .AlignRight()
                            .Column(info =>
                            {
                                info.Item()
                                    .Text("DOCKET NUMBER")
                                    .FontSize(7)
                                    .Bold()
                                    .FontColor("#64748b");

                                info.Item()
                                    .Text(docketNumber)
                                    .FontSize(12)
                                    .Bold()
                                    .FontColor("#2563eb");
                            });
                    });
            });
        }

        // =========================================================
        // CONTENT
        // =========================================================

        private void ComposeContent(
            IContainer container,
            InvestigationClass investigation,
            CaseClass? caseInformation,
            SuspectClass? suspect,
            List<EvidenceClass> evidence,
            string digitalSignature)
        {
            container.Column(column =>
            {
                column.Spacing(18);

                // =================================================
                // INVESTIGATION INFORMATION
                // =================================================

                column.Item()
                    .Column(section =>
                    {
                        section.Item()
                            .Element(title =>
                            {
                                ComposeSectionTitle(
                                    title,
                                    "INVESTIGATION INFORMATION"
                                );
                            });

                        section.Item()
                            .PaddingTop(8)
                            .Column(info =>
                            {
                                info.Spacing(6);

                                AddInformationRow(
                                    info,
                                    "Investigation ID",
                                    investigation.InvestigationID.ToString()
                                );

                                AddInformationRow(
                                    info,
                                    "Date Started",
                                    investigation.DateStarted.ToString(
                                        "dd MMMM yyyy HH:mm"
                                    )
                                );

                                AddInformationRow(
                                    info,
                                    "Case",
                                    caseInformation?.CaseName
                                        ?? "Unknown"
                                );

                                AddInformationRow(
                                    info,
                                    "Case Status",
                                    caseInformation?.Status
                                        ?? "Unknown"
                                );
                            });
                    });

                // =================================================
                // CASE INFORMATION
                // =================================================

                column.Item()
                    .Column(section =>
                    {
                        section.Item()
                            .Element(title =>
                            {
                                ComposeSectionTitle(
                                    title,
                                    "CASE INFORMATION"
                                );
                            });

                        section.Item()
                            .PaddingTop(8)
                            .Column(info =>
                            {
                                info.Spacing(6);

                                AddInformationRow(
                                    info,
                                    "Case Name",
                                    caseInformation?.CaseName
                                        ?? "Unknown"
                                );

                                AddInformationRow(
                                    info,
                                    "Description",
                                    caseInformation?.Description
                                        ?? "No description available."
                                );

                                AddInformationRow(
                                    info,
                                    "Status",
                                    caseInformation?.Status
                                        ?? "Unknown"
                                );
                            });
                    });

                // =================================================
                // SUSPECT INFORMATION
                // =================================================

                column.Item()
                    .Column(section =>
                    {
                        section.Item()
                            .Element(title =>
                            {
                                ComposeSectionTitle(
                                    title,
                                    "SUSPECT INFORMATION"
                                );
                            });

                        section.Item()
                            .PaddingTop(8)
                            .Column(info =>
                            {
                                info.Spacing(6);

                                AddInformationRow(
                                    info,
                                    "Name",
                                    suspect?.SuspectName
                                        ?? "Unknown"
                                );

                                AddInformationRow(
                                    info,
                                    "Occupation",
                                    suspect?.Occupation
                                        ?? "Unknown"
                                );

                                AddInformationRow(
                                    info,
                                    "Description",
                                    suspect?.Description
                                        ?? "No description available."
                                );
                            });
                    });

                // =================================================
                // EVIDENCE REVIEWED
                // =================================================

                column.Item()
                    .Column(section =>
                    {
                        section.Item()
                            .Element(title =>
                            {
                                ComposeSectionTitle(
                                    title,
                                    "EVIDENCE REVIEWED"
                                );
                            });

                        section.Item()
                            .PaddingTop(8)
                            .Column(evidenceColumn =>
                            {
                                evidenceColumn.Spacing(8);

                                if (evidence.Count == 0)
                                {
                                    evidenceColumn.Item()
                                        .Text(
                                            "No evidence recorded."
                                        )
                                        .FontColor("#64748b");
                                }
                                else
                                {
                                    for (
                                        int i = 0;
                                        i < evidence.Count;
                                        i++
                                    )
                                    {
                                        var item = evidence[i];

                                        evidenceColumn.Item()
                                            .Border(1)
                                            .BorderColor("#dbeafe")
                                            .Background("#f8fafc")
                                            .Padding(10)
                                            .Column(evidenceItem =>
                                            {
                                                evidenceItem.Item()
                                                    .Text(
                                                        $"{i + 1}. {item.Title}"
                                                    )
                                                    .Bold()
                                                    .FontSize(11)
                                                    .FontColor(
                                                        "#1e40af"
                                                    );

                                                evidenceItem.Item()
                                                    .PaddingTop(4)
                                                    .Text(
                                                        $"Location: {item.Location}"
                                                    )
                                                    .FontSize(9)
                                                    .FontColor(
                                                        "#475569"
                                                    );

                                                evidenceItem.Item()
                                                    .PaddingTop(4)
                                                    .Text(
                                                        item.Description
                                                    )
                                                    .FontSize(9)
                                                    .FontColor(
                                                        "#334155"
                                                    );
                                            });
                                    }
                                }
                            });
                    });

                // =================================================
                // INVESTIGATION CONCLUSION
                // =================================================

                column.Item()
                    .Column(section =>
                    {
                        section.Item()
                            .Element(title =>
                            {
                                ComposeSectionTitle(
                                    title,
                                    "INVESTIGATION CONCLUSION"
                                );
                            });

                        section.Item()
                            .PaddingTop(8)
                            .Background("#f8fafc")
                            .Border(1)
                            .BorderColor("#dbeafe")
                            .Padding(15)
                            .Text(investigation.Conclusion)
                            .FontSize(10)
                            .FontColor("#1f2937");
                    });

                // =================================================
                // DIGITAL SIGNATURE
                // =================================================

                column.Item()
                    .Column(section =>
                    {
                        section.Item()
                            .Element(title =>
                            {
                                ComposeSectionTitle(
                                    title,
                                    "INVESTIGATOR DIGITAL SIGNATURE"
                                );
                            });

                        section.Item()
                            .PaddingTop(10)
                            .Border(1)
                            .BorderColor("#bfdbfe")
                            .Background("#f8fafc")
                            .Padding(15)
                            .Column(signature =>
                            {
                                signature.Spacing(6);

                                signature.Item()
                                    .Text("DIGITALLY SIGNED")
                                    .FontSize(12)
                                    .Bold()
                                    .FontColor("#2563eb");

                                signature.Item()
                                    .Text(
                                        "TRACEPOINT INVESTIGATOR"
                                    )
                                    .FontSize(10)
                                    .Bold()
                                    .FontColor("#1f2937");

                                signature.Item()
                                    .PaddingTop(5)
                                    .Text("Digital Signature")
                                    .FontSize(8)
                                    .Bold()
                                    .FontColor("#64748b");

                                signature.Item()
                                    .Text(digitalSignature)
                                    .FontSize(10)
                                    .Bold()
                                    .FontColor("#1e40af");

                                signature.Item()
                                    .PaddingTop(5)
                                    .Text(
                                        $"Signed: {investigation.DateStarted:dd MMMM yyyy HH:mm}"
                                    )
                                    .FontSize(8)
                                    .FontColor("#64748b");

                                signature.Item()
                                    .PaddingTop(5)
                                    .Text(
                                        "This digital signature was generated " +
                                        "by the TracePoint Digital Investigation System."
                                    )
                                    .FontSize(8)
                                    .Italic()
                                    .FontColor("#64748b");
                            });
                    });

                // =================================================
                // DOCUMENT NOTICE
                // =================================================

                column.Item()
                    .PaddingTop(10)
                    .BorderTop(1)
                    .BorderColor("#cbd5e1")
                    .PaddingTop(10)
                    .Text(
                        "This document was generated by the TracePoint " +
                        "Digital Investigation System. It is a " +
                        "system-generated investigation record."
                    )
                    .FontSize(8)
                    .Italic()
                    .FontColor("#64748b");
            });
        }

        // =========================================================
        // SECTION TITLE
        // =========================================================

        private void ComposeSectionTitle(
            IContainer container,
            string title)
        {
            container.Row(row =>
            {
                row.ConstantItem(5)
                    .Height(18)
                    .Background("#2563eb");

                row.ConstantItem(10);

                row.RelativeItem()
                    .Text(title)
                    .FontSize(11)
                    .Bold()
                    .FontColor("#1e3a5f");
            });
        }

        // =========================================================
        // INFORMATION ROW
        // =========================================================

        private void AddInformationRow(
            ColumnDescriptor column,
            string label,
            string value)
        {
            column.Item()
                .Row(row =>
                {
                    row.ConstantItem(120)
                        .Text(label)
                        .Bold()
                        .FontSize(9)
                        .FontColor("#64748b");

                    row.RelativeItem()
                        .Text(value)
                        .FontSize(9)
                        .FontColor("#1f2937");
                });
        }
    }
}