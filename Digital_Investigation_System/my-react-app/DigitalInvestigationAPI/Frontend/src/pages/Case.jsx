import { useEffect, useState } from 'react'
import CaseCard from '../Components/CaseCard'

function Cases() {
    const [cases, setCases] = useState([])
    const [loading, setLoading] = useState(true)
    const [error, setError] = useState('')

    useEffect(() => {
        fetch('https://localhost:7031/api/Cases')
            .then((response) => {
                if (!response.ok) {
                    throw new Error(
                        `API returned status ${response.status}`
                    )
                }

                return response.json()
            })
            .then((data) => {
                setCases(data)
                setLoading(false)
            })
            .catch((err) => {
                console.error('Cases API error:', err)
                setError(err.message)
                setLoading(false)
            })
    }, [])

    return (
        <div className="page">
            <section className="page-heading">
                <p className="eyebrow">INVESTIGATIONS</p>

                <h1>Cases</h1>

                <p>
                    Review and manage digital investigations.
                </p>
            </section>

            <section className="content-section">

                {loading && (
                    <div className="empty-card">
                        <h2>Loading Cases...</h2>

                        <p>
                            Please wait while the cases are loaded
                            from the investigation system.
                        </p>
                    </div>
                )}

                {error && (
                    <div className="empty-card">
                        <h2>Unable to Load Cases</h2>

                        <p>{error}</p>
                    </div>
                )}

                {!loading && !error && cases.length === 0 && (
                    <div className="empty-card">
                        <h2>No Cases Found</h2>

                        <p>
                            There are currently no cases in the database.
                        </p>
                    </div>
                )}

                {!loading && !error && cases.length > 0 && (
                    <div className="cases-grid">
                        {cases.map((caseItem) => (
                            <CaseCard
                                key={caseItem.caseID}
                                caseData={caseItem}
                            />
                        ))}
                    </div>
                )}

            </section>
        </div>
    )
}

export default Cases