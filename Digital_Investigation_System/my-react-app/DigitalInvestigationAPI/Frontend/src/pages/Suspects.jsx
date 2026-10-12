import { useEffect, useState } from 'react'
import SuspectCard from '../Components/SuspectCard'

function Suspects() {
    const [suspects, setSuspects] = useState([])
    const [selectedSuspect, setSelectedSuspect] = useState(null)

    useEffect(() => {
        fetch('https://localhost:7031/api/Suspects')
            .then((response) => {
                if (!response.ok) {
                    throw new Error('Failed to retrieve suspects')
                }

                return response.json()
            })
            .then((data) => {
                setSuspects(data)
            })
            .catch((error) => {
                console.error('Suspects API error:', error)
            })
    }, [])

    const handleSelectSuspect = (suspect) => {
        setSelectedSuspect(suspect)
    }

    return (
        <div className="page">

            <section className="page-heading">
                <p className="eyebrow">INVESTIGATION</p>

                <h1>Suspects</h1>

                <p>
                    Review suspects connected to the investigation.
                </p>
            </section>

            <section className="content-section">

                <div className="cases-grid">

                    {suspects.map((suspect) => (
                        <SuspectCard
                            key={suspect.suspectID}
                            suspect={suspect}
                            onSelect={handleSelectSuspect}
                        />
                    ))}

                </div>

                {selectedSuspect && (
                    <div className="selected-suspect">
                        <h2>Selected Suspect</h2>

                        <p>
                            You selected: {selectedSuspect.suspectName}
                        </p>
                    </div>
                )}

            </section>

        </div>
    )
}

export default Suspects