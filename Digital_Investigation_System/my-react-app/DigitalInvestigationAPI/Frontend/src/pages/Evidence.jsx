
import { useEffect, useState } from 'react'
import EvidenceCard from '../Components/EvidenceCard'
import { apiUrl } from '../api'

function Evidence() {
    const [evidence, setEvidence] = useState([])
    const [selectedEvidence, setSelectedEvidence] = useState(null)
    const [loading, setLoading] = useState(true)
    const [error, setError] = useState('')

    useEffect(() => {
        fetch(apiUrl('/api/Evidence'))
            .then((response) => {
                if (!response.ok) {
                    throw new Error('Failed to retrieve evidence')
                }

                return response.json()
            })
            .then((data) => {
                setEvidence(data)
                setLoading(false)
            })
            .catch((error) => {
                console.error('Evidence API error:', error)
                setError(error.message)
                setLoading(false)
            })
    }, [])

    return (
        <div className="page">
            <section className="page-heading">
                <p className="eyebrow">INVESTIGATION</p>
                <h1>Evidence</h1>
                <p>Review digital evidence connected to cases.</p>
            </section>

            <section className="content-section">

                {loading && (
                    <div className="empty-card">
                        <h2>Loading Evidence...</h2>
                        <p>
                            Please wait while the evidence is loaded from
                            the investigation system.
                        </p>
                    </div>
                )}

                {error && (
                    <div className="empty-card">
                        <h2>Unable to Load Evidence</h2>
                        <p>{error}</p>
                    </div>
                )}

                {!loading && !error && evidence.length === 0 && (
                    <div className="empty-card">
                        <h2>No Evidence Found</h2>
                        <p>
                            There is currently no evidence in the database.
                        </p>
                    </div>
                )}

                {!loading && !error && evidence.length > 0 && (
                    <div className="evidence-grid">
                        {evidence.map((item) => (
                            <EvidenceCard
                                key={item.evidenceID}
                                evidence={item}
                                onExamine={setSelectedEvidence}
                            />
                        ))}
                    </div>
                )}

                {selectedEvidence && (
                    <div className="selected-evidence">
                        <h2>{selectedEvidence.title}</h2>

                        <p>
                            <strong>Location:</strong>{' '}
                            {selectedEvidence.location}
                        </p>

                        <p>
                            {selectedEvidence.description}
                        </p>
                    </div>
                )}

            </section>
        </div>
    )
}

export default Evidence
