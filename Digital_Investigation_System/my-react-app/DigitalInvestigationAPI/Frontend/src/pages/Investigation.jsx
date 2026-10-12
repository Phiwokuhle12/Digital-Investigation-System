import { useEffect, useState } from 'react'
import InvestigationForm from '../Components/InvestigationForm'

function Investigation() {
    const [suspects, setSuspects] = useState([])
    const [cases, setCases] = useState([])
    const [selectedSuspect, setSelectedSuspect] = useState('')
    const [conclusion, setConclusion] = useState('')
    const [message, setMessage] = useState('')

    // Get suspects from the API
    useEffect(() => {
        fetch('https://localhost:7031/api/Suspects')
            .then((response) => {
                if (!response.ok) {
                    throw new Error('Unable to load suspects.')
                }

                return response.json()
            })
            .then((data) => {
                setSuspects(data)
            })
            .catch((error) => {
                console.error(error)
                setMessage('Unable to load suspects.')
            })
    }, [])

    // Get cases from the API
    useEffect(() => {
        fetch('https://localhost:7031/api/Cases')
            .then((response) => {
                if (!response.ok) {
                    throw new Error('Unable to load cases.')
                }

                return response.json()
            })
            .then((data) => {
                setCases(data)
            })
            .catch((error) => {
                console.error(error)
                setMessage('Unable to load cases.')
            })
    }, [])

    // Submit investigation
    const handleSubmit = async () => {
        if (!selectedSuspect || !conclusion.trim()) {
            setMessage(
                'Please select a suspect and enter a conclusion.'
            )
            return
        }

        if (cases.length === 0) {
            setMessage(
                'No case is available for this investigation.'
            )
            return
        }

        // Use the current case
        const caseID = cases[0].caseID

        const investigation = {
            caseID: caseID,
            suspectID: parseInt(selectedSuspect),
            conclusion: conclusion
        }

        try {
            const response = await fetch(
                'https://localhost:7031/api/Investigation',
                {
                    method: 'POST',
                    headers: {
                        'Content-Type': 'application/json'
                    },
                    body: JSON.stringify(investigation)
                }
            )

            if (response.ok) {

                // =================================================
                // RECEIVE THE GENERATED PDF
                // =================================================

                const pdfBlob = await response.blob()

                // =================================================
                // CREATE A TEMPORARY DOWNLOAD URL
                // =================================================

                const downloadUrl =
                    window.URL.createObjectURL(pdfBlob)

                // =================================================
                // CREATE DOWNLOAD LINK
                // =================================================

                const link =
                    document.createElement('a')

                link.href = downloadUrl

                // Use the filename sent by the API if available
                const contentDisposition =
                    response.headers.get('Content-Disposition')

                let fileName =
                    'TracePoint-Investigation-Docket.pdf'

                if (contentDisposition) {
                    const fileNameMatch =
                        contentDisposition.match(
                            /filename="?([^"]+)"?/
                        )

                    if (fileNameMatch) {
                        fileName = fileNameMatch[1]
                    }
                }

                link.download = fileName

                document.body.appendChild(link)

                link.click()

                // =================================================
                // CLEAN UP
                // =================================================

                link.remove()

                window.URL.revokeObjectURL(downloadUrl)

                // =================================================
                // RESET FORM
                // =================================================

                setMessage(
                    'Investigation submitted successfully. Docket generated.'
                )

                setSelectedSuspect('')
                setConclusion('')
            } else {
                const error = await response.text()

                setMessage(
                    error ||
                    'Failed to submit investigation.'
                )
            }
        } catch (error) {
            console.error(error)

            setMessage(
                'Error connecting to the investigation API.'
            )
        }
    }

    return (
        <div className="page">

            <div className="page-heading">

                <p className="eyebrow">
                    INVESTIGATION
                </p>

                <h1>
                    Complete Investigation
                </h1>

                <p>
                    Select a suspect and enter your
                    investigation conclusion. A TracePoint
                    investigation docket will be generated
                    automatically when you submit.
                </p>

            </div>

            <InvestigationForm
                suspects={suspects}
                selectedSuspect={selectedSuspect}
                conclusion={conclusion}
                onSuspectChange={setSelectedSuspect}
                onConclusionChange={setConclusion}
                onSubmit={handleSubmit}
            />

            {message && (
                <p className="investigation-message">
                    {message}
                </p>
            )}

        </div>
    )
}

export default Investigation